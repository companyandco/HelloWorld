using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	
	private int i = 1;

	private void FixedUpdate()
	{
		if (i == 25)
		{
			
			if (!Main_Controller_def.found)
			Main_Controller_def.Localisation(
				Main_Controller.Earth.regionlist[Random.Range(0, Main_Controller.Earth.regionlist.Count)]);
			i = 1;
			
			

		}
		else
			i++;

	}
}
