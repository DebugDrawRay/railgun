using UnityEngine;
using System.Collections;

public class playerMech : MonoBehaviour 
{
	//movement vars
	public Vector3 forwardVel;
	public float baseSpeed;
	private float strafeVel;
	public float recenterVel;

	//dashing vars
	private float dashFuel;
	private float maxDashFuel;
	private float dashRefuelRate;
	public float fuelUseRate; 

	//positioning vars
	private int currentPosition;
	public Vector3[] availablePositions;

	private bool rightReady;
	private bool leftReady;

	//aiming vars
	public float zAimOffset;

	//weapon vars
	public LayerMask rangedRayMask;
	public int totalDef;
	public bool meleeCooldown;

	//inventory vars
	public GameObject rangedWeapon;
	public GameObject meleeWeapon;
	public GameObject dashEngine;
	public GameObject shield;

	//bomb vars
	public GameObject bomb;
	private GameObject newBomb;
	public int bombCount;
	public int bombCountMax;
	private bool bombSent;

	//stat vars
	public int lives;
	public int armor;
	public int defense;
	public int attack;
	public float speed;

	//status vars
	private int currentArmor;

	//external tags
	public string enemyDamageTag;
	public string enemyCollisionTag;
	public string enviromentCollisionTag;

	//input vars
	//"Horizontal"
	//"Vertical"
	//"UseRangedWeapon"
	//"UseMeleeWeapon"
	//"UseBomb"
	//"RaiseShield"
	//"ChangePositionLeft"
	//"ChangePositionRight"
	//"Dash"

	private float horAxis;
	private float verAxis;
	private bool moveRight;
	private bool moveLeft;
	private bool dash;
	private bool fireRanged;
	private bool useMelee;
	private bool useBomb;
	private Vector3 mousePos;
	private bool raiseShield;

	//
	//initialization
	//

	void Start () 
	{
		railMovement();
		playerRepositioning(0);
		varInit();
	}
	void varInit()
	{
		//positioning
		rightReady = true;
		leftReady = true;

		//movement
		strafeVel = speed + baseSpeed;

		//weapons/shields
		totalDef = defense;
		meleeCooldown = true;
		bombSent = false;

		//dashing
		dashRefuelRate = dashEngine.GetComponent<equipmentProperties>().refuelRate;
		maxDashFuel = dashEngine.GetComponent<equipmentProperties>().maxFuel;
		dashFuel = maxDashFuel;

		//status
		currentArmor = armor;
	}
	

	//
	//main loop
	//

	void Update () 
	{
		playerMovement();
		playerRecentering();
		playerAiming();
		weaponControl();
		inputListener();

		checkStatus();
	}

	//check status, runs functions on status change
	void checkStatus()
	{
		if(currentArmor <= 0)
		{
			deathEvent();
		}
	}

	void deathEvent()
	{
		
	}

	//input control
	void inputListener()
	{
		horAxis = Input.GetAxisRaw("Horizontal");
		verAxis = Input.GetAxisRaw("Vertical");
		moveRight = Input.GetButtonDown("ChangePositionRight");
		moveLeft = Input.GetButtonDown("ChangePositionLeft");
		dash = Input.GetButton("Dash");
		fireRanged = Input.GetButtonDown("UseRangedWeapon");
		mousePos = Input.mousePosition;
		mousePos.z = zAimOffset;
		raiseShield = Input.GetButton("RaiseShield");
		useMelee = Input.GetButtonDown("UseMeleeWeapon");
		useBomb = Input.GetButtonDown("UseBomb");
	}

	//movement control
	void railMovement()
	{
		constantForce.force = forwardVel;
	}

	void playerMovement()
	{
		//strafing
		rigidbody.AddForce(horAxis * Camera.main.transform.right * strafeVel);
		rigidbody.AddForce(verAxis * Camera.main.transform.up * strafeVel);

		//positioning
		if (moveRight && rightReady)
		{
			playerRepositioning(1);
			rightReady = false;
		}
		if (!moveRight)
		{
			rightReady = true;;
		}
		if (moveLeft && leftReady)
		{
			playerRepositioning(-1);
			leftReady = false;
		}
		if (!moveLeft)
		{
			leftReady = true;;
		}

		//dashing
		if(dash && dashFuel > 0)
		{
			strafeVel = speed + (baseSpeed * dashEngine.GetComponent<equipmentProperties>().dashMulti);
			dashFuel -= fuelUseRate;
			Debug.Log("dashing");
		}
		if(!dash || dashFuel <= 0)
		{
			if (dashFuel >= maxDashFuel)
			{
				dashFuel = maxDashFuel;
			}
			else
			{
				dashFuel += dashRefuelRate;
			}
			strafeVel = speed + baseSpeed;
		}
	}

	void playerRepositioning(int amount)
	{
		currentPosition += amount;
		if (currentPosition < 0)
		{
			currentPosition = availablePositions.Length - 1;
		}
		if (currentPosition > availablePositions.Length - 1)
		{
			currentPosition = 0;
		}
		transform.position = Camera.main.transform.position + availablePositions[currentPosition];
		Camera.main.transform.eulerAngles += new Vector3(0, 90 * amount, 0);
	}

	void playerRecentering()
	{
		Vector3 screenCenter = Vector3.zero;
		if(currentPosition == 0 || currentPosition == 2) 
		{
			screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0));
		}
		if(currentPosition == 1 || currentPosition == 3) 
		{
			screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, -Camera.main.GetComponent<cameraFollow>().followOffset));
		}
		Vector3 recenterDirection = screenCenter - transform.position;
		recenterDirection.z = 0;
		rigidbody.AddForce(recenterDirection * recenterVel);
	}

	//aiming and weapon control
	void playerAiming()
	{
		transform.LookAt(Camera.main.ScreenToWorldPoint(mousePos));
	}

	void weaponControl()
	{
		//ranged
		if (fireRanged && !raiseShield)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			GameObject proj = Instantiate(rangedWeapon, transform.position, Quaternion.identity) as GameObject;
			proj.GetComponent<equipmentProperties>().inheritedDamage = attack;

			if(Physics.Raycast(ray, out hit, Mathf.Infinity, rangedRayMask))
			{
				proj.transform.LookAt(hit.point);
			}
			else
			{
				proj.transform.rotation = transform.rotation;
			}
		}

		//melee
		if (useMelee && !raiseShield && meleeCooldown)
		{
			GameObject weapon = Instantiate(meleeWeapon, transform.forward, Quaternion.identity) as GameObject;
			weapon.GetComponent<equipmentProperties>().inheritedDamage = attack;
			meleeCooldown = false;
		}

		//defense
		if(raiseShield)
		{
			totalDef = defense * shield.GetComponent<equipmentProperties>().defenseIncrease;
		}
		if(!raiseShield)
		{
			totalDef = defense;
		}

		//bomb	
		if (useBomb)
		{
			if (!bombSent)
			{
				if(bombCount > 0)
				{
					newBomb = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;
					bombCount -= 1;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					
					if(Physics.Raycast(ray, out hit, Mathf.Infinity, rangedRayMask))
					{
						newBomb.transform.LookAt(hit.point);
					}
					else
					{
						newBomb.transform.rotation = transform.rotation;
					}
					bombSent = true;
				}
			}

			else if (bombSent)
			{
				newBomb.GetComponent<bombProperties>().explodeThis();
				bombSent = false;
			}
		}
	}

	//
	//collision handling
	//

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == enemyDamageTag)
		{
			takeDamage(other.gameObject.GetComponent<equipmentProperties>().damageValue);
			Destroy(other.gameObject);
		}
	}

	void takeDamage(int amount)
	{
		currentArmor -= amount;
	}
}
