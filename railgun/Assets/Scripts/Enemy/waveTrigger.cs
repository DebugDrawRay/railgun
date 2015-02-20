using UnityEngine;
using System.Collections;

public class waveTrigger : MonoBehaviour 
{
	public string triggerTarget;
	public GameObject[] availableGroups;
	public float spawnInterval;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == triggerTarget)
		{
			spawnGroups();
		}
	}
	void spawnGroups()
	{
		foreach(GameObject group in availableGroups)
		{
			Instantiate(group, transform.position, Quaternion.identity);
		}
		Destroy(this.gameObject);
	}
	
}
