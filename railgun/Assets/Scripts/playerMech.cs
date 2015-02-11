using UnityEngine;
using System.Collections;

public class playerMech : MonoBehaviour 
{
	//movement vars
	public Vector3 forwardVel;
	public float strafeVel;
	public float recenterVel;

	//positioning vars
	private int currentPosition;
	public Vector3[] availablePositions;

	private bool rightReady;
	private bool leftReady;
	//input vars
	//"Horizontal"
	//"Vertical"
	//"UseRangedWeapon"
	//"UseMeleeWeapon"
	//"UseBomb"
	//"RaiseShield"
	//"ChangePositionLeft"
	//"ChangePositionRight"
	private float horAxis;
	private float verAxis;
	private bool moveRight;
	private bool moveLeft;


	// Use this for initialization
	void Start () 
	{
		railMovement();
		playerRepositioning(0);
		varInit();
	}
	void varInit()
	{
		rightReady = true;
		leftReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerMovement();
		playerRecentering();
		inputListener();
	}
	//input control
	void inputListener()
	{
		horAxis = Input.GetAxisRaw("Horizontal");
		verAxis = Input.GetAxisRaw("Vertical");
		moveRight = Input.GetButtonDown("ChangePositionRight");
		moveLeft = Input.GetButtonDown("ChangePositionLeft");
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
}
