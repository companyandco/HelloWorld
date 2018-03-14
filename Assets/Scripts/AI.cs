using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	
	private int i = 0;

	private void FixedUpdate()
	{
		if (i == 150)
		{
			if (!Main_Controller_def.found)
				Main_Controller_def.Localisation(
					Main_Controller.Earth.regionlist[Random.Range(0, Main_Controller.Earth.regionlist.Count)]);
			else
			{
				if (!Main_Controller_def.foundSymp){
					Main_Controller_def.ResearchSymp();
				}
				else
				{
					if (!Main_Controller_def.foundTrans)
						Main_Controller_def.ResearchTrans();
					else
						Main_Controller_def.ResearchAntidote();
				}
				
				
			}			
			

			i = 0;
		}
		else
			i++;

	}
}
