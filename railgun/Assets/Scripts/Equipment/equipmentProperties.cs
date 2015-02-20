using UnityEngine;
using System.Collections;

public class equipmentProperties : MonoBehaviour 
{
	//projectiles
	public float travelSpeed;

	//weapons
	public int baseDamage;
	public int inheritedDamage;
	public int damageValue;

	//shields
	public int defenseIncrease;

	//melee
	public float posOffset;
	public float attackDelay;

	//dashing
	public float dashMulti;
	public float maxFuel;
	public float refuelRate;

	//bombs
	public float fuseDistance;
	public GameObject explosion;

	public float maxSize;
	public Vector3 expandRate;


	void Start()
	{
		damageValue = inheritedDamage + baseDamage;
	}
}
