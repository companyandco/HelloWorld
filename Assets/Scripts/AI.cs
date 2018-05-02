using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
	public static bool isSP = false;

	public Text text;
	private bool hasFound = false;
	private bool canStart = false;
	private bool located = false;
	
	void Start()
	{
		isSP = true;
	}
	
	private int i = 0;
	private void FixedUpdate()
	{
		if (!canStart)
		{
			canStart = Main_Controller.symptoms.Count != 0 || Main_Controller.transmitions.Count != 0;
			return;
		}
		if (i != 450)
		{
			i++;
			return;
		}
		i = 0;

		if (!located)
			located = Main_Controller_def.Localisation(
				Main_Controller.Earth.regionlist[Random.Range(0, Main_Controller.symptoms.Count)]);
		else
		{
			if (Main_Controller_def.foundTransmitions.Count != Main_Controller.transmitions.Count)
				Main_Controller_def.ResearchTrans(Main_Controller.transmitions[Random.Range(0, Main_Controller.symptoms.Count)]);
			else
			{
				if (Main_Controller_def.foundTransmitions.Count != Main_Controller.transmitions.Count)
					Main_Controller_def.ResearchTrans(Main_Controller.transmitions[Random.Range(0, Main_Controller.symptoms.Count)]);
				else
					Main_Controller_def.ResearchAntidote();
			}
		}







		if ( Main_Controller_def.found )
		{
			this.text.text = "L'IA a localisé le virus: \n" + "vrai";
		} else
		{
			this.text.text = "L'IA a localisé le virus: \n" + "faux";			
		}
	}
}
