using UnityEngine;
using System.Collections;

public class basicFlyer : MonoBehaviour 
{
	//all vars are contained in enemy properties for global ref
	private float travelSpeed;
	private float damageOutput;
	private GameObject projectileType;
	private float armor;
	private string attackDir;

	void Start () 
	{
		getProperties();
	}
	
	//get all properties 
	void getProperties()
	{
		travelSpeed = GetComponent<enemyProperties>().travelSpeed;
		damageOutput = GetComponent<enemyProperties>().damageOutput;
		projectileType = GetComponent<enemyProperties>().projectileType;
		armor = GetComponent<enemyProperties>().armor;
		attackDir = GetComponent<enemyProperties>().attackDir;
	}

	void Update () 
	{
		movementEngine();
		attackEngine();
	}

	void movementEngine()
	{
		constantForce.force = setDir(attackDir);
	}

	//handles move direction calculations
	Vector3 setDir(string dir)
	{
		float playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMech>().constantForce.force.z;

		if (dir == "n")
		{
			return new Vector3(0, 0, -travelSpeed);		
		}
		if (dir == "e")
		{
			return new Vector3(-travelSpeed, 0, playerSpeed);			
		}
		if (dir == "s")
		{
			return new Vector3(0, 0, travelSpeed + playerSpeed);	
		}
		if (dir == "w")
		{
			return new Vector3(travelSpeed, 0, playerSpeed);	
		}
		return Vector3.zero;
	}

	void attackEngine()
	{

	}
}
