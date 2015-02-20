using UnityEngine;
using System.Collections;

public class targetingRetOuter : MonoBehaviour 
{
	public float lerpVar;
	void Update () 
	{
		mouseFollow();
	}

	void mouseFollow ()
	{
		transform.position = Vector3.Lerp(transform.position, Input.mousePosition, lerpVar);
	}
}
