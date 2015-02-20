using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class statusBar : MonoBehaviour 
{
	public string targetAttribute;
	
	void Update () 
	{
		GetComponent<Image>().fillAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMech>().getAttribute(targetAttribute) / 100.0f;
	}

}
