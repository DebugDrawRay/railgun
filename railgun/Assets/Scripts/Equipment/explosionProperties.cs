using UnityEngine;
using System.Collections;

public class explosionProperties : MonoBehaviour 
{

	void Update () 
	{
		expandExplosion();
	}

	void expandExplosion()
	{
		transform.localScale += GetComponent<equipmentProperties>().expandRate;

		if (transform.localScale.x >= GetComponent<equipmentProperties>().maxSize)
		{
			Destroy(this.gameObject);
		}
	}
}
