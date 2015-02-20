using UnityEngine;
using System.Collections;

public class bombProperties : MonoBehaviour 
{	
	void Start()
	{
		rigidbody.velocity = transform.forward * GetComponent<equipmentProperties>().travelSpeed;
	}
	void Update () 
	{
		runFuse();
	}

	void runFuse()
	{
		float parentDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
		if (parentDistance >= GetComponent<equipmentProperties>().fuseDistance)
		{
			explodeThis();
		}
		
	}

	public void explodeThis()
	{
		Instantiate(GetComponent<equipmentProperties>().explosion, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
