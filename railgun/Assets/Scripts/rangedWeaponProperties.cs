using UnityEngine;
using System.Collections;

public class rangedWeaponProperties : MonoBehaviour 
{
	void Start()
	{
		rigidbody.velocity = transform.forward * GetComponent<equipmentProperties>().travelSpeed;
	}
}
