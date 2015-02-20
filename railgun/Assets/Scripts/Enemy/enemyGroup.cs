using UnityEngine;
using System.Collections;

public class enemyGroup : MonoBehaviour 
{

	private Vector3 spawnPoint;
	public float distanceOffset;
	public float horOffset;
	public float spawnHeight;
	public string spawnQuadrant;
	public GameObject[] availableEnemies;
	public Vector3[] enemyPositions;

	//handles all spawn functions, no update
	void Start () 
	{
		spawnPoint = formatSpawn(spawnQuadrant);
		spawnEnemies();
	}

	//sets spwan "centerpoint" and facing
	Vector3 formatSpawn(string dir)
	{
		if (dir == "n")
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
			return new Vector3(transform.position.x + horOffset, spawnHeight, transform.position.z + distanceOffset);		
		}
		if (dir == "e")
		{
			transform.eulerAngles = new Vector3(0, 270, 0);
			return new Vector3(transform.position.x + distanceOffset, spawnHeight, transform.position.z + horOffset);			
		}
		if (dir == "s")
		{
			transform.eulerAngles = Vector3.zero;
			return new Vector3(transform.position.x + horOffset, spawnHeight, transform.position.z - distanceOffset);	
		}
		if (dir == "w")
		{
			transform.eulerAngles = new Vector3(0, 90, 0);
			return new Vector3(transform.position.x + distanceOffset, spawnHeight, transform.position.z + horOffset);	
		}
		return transform.position;
	}

	//spawns enemies, can be given formation offsets
	void spawnEnemies()
	{
		for(int i = 0; i <= availableEnemies.Length - 1; i++)
		{
			GameObject newEnemy = Instantiate(availableEnemies[i], spawnPoint + enemyPositions[i], transform.rotation) as GameObject;
			newEnemy.GetComponent<enemyProperties>().attackDir = spawnQuadrant;
		}
		Destroy(this.gameObject);
	}
}
