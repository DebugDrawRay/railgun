using UnityEngine;
using System.Collections;

public class subTargetRet : MonoBehaviour 
{
	public float xOffset;
	public float yOffset;
	void Update () 
	{
		Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		GetComponent<LineRenderer>().SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)));
		GetComponent<LineRenderer>().SetPosition(1, Camera.main.ScreenToWorldPoint(new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, transform.position.z)));
	}
}
