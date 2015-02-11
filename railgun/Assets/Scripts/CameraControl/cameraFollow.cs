using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour 
{
	//tag references
	public string playerMech;
	public float followOffset;
	void Start()
	{
		initializePosition();
	}
	
	void initializePosition()
	{
		transform.position = new Vector3(GameObject.FindGameObjectWithTag(playerMech).transform.position.x, 
										 GameObject.FindGameObjectWithTag(playerMech).transform.position.y, 
										 GameObject.FindGameObjectWithTag(playerMech).transform.position.z + followOffset);
		rigidbody.drag = GameObject.FindGameObjectWithTag(playerMech).rigidbody.drag;
		constantForce.force = GameObject.FindGameObjectWithTag(playerMech).constantForce.force;
	}
	void Update () 
	{
		//followEngine();	
	}

	/*void followEngine()
	{
		transform.position = new Vector3(transform.position.x, 
										 transform.position.y, 
										 GameObject.FindGameObjectWithTag(playerMech).transform.position.z + followOffset);
	}*/
}
