using UnityEngine;
using System.Collections;

public class meleeWeaponProperties : MonoBehaviour 
{
	//buffer for attack animations
	private float timer;

	void Start()
	{
		timer = GetComponent<equipmentProperties>().attackDelay;
	}
	void Update()
	{
		runTimer();
		setPosition();
	}
	void setPosition()
	{
		Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 playerForward = GameObject.FindGameObjectWithTag("Player").transform.forward;
		transform.position = (playerPos + playerForward) * GetComponent<equipmentProperties>().posOffset;
	}
	void runTimer()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<playerMech>().meleeCooldown = true;
			Destroy(this.gameObject);
		}
	}
}
