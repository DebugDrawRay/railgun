using UnityEngine;
using System.Collections;

public class targetingRet : MonoBehaviour 
{
	public float lerpVar;
	public Vector3 normalScale;
	public Vector3 onFireScale;

	void Update () 
	{
		mouseFollow();
		fireAnim();
	}

	void mouseFollow ()
	{
		transform.position = Input.mousePosition;
	}

	void fireAnim()
	{
		if(Input.GetButton("UseRangedWeapon"))
		{
			transform.localScale = Vector3.Lerp(transform.localScale, onFireScale, lerpVar);
		}
		if(!Input.GetButton("UseRangedWeapon"))
		{
			transform.localScale = Vector3.Lerp(transform.localScale, normalScale, lerpVar);
		}
	}
}
