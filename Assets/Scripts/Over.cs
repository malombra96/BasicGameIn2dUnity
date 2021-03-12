using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Over : MonoBehaviour
{
	
	public bool luz;
	private bool ciclo = true;
	public byte yellow = 255;
	public byte A;
	
	private void Awake()
	{
		//luz = false;
	}

	private void OnEnable()
	{
		ciclo = false;
		yellow = 254;
		
	}

	void FixedUpdate () {
		if (luz)
		{
			//print("YELLOW = "+yellow);
			if (ciclo) // baja
			{
				GetComponent<SpriteRenderer>().color = new Color32(255, yellow, yellow, A);
				yellow = (byte) (yellow - 15);
				ciclo = yellow > 0;
			}
			else
			{
				/*this.GetComponent<Image>().color = new Color32(255, 255, yellow , A);
				yellow = (byte) (yellow + 7);
				ciclo = yellow > 254;*/
				ciclo = true;
				yellow = 254;
			}
		}
		else
		{
			GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, A);
		}
	}
}
