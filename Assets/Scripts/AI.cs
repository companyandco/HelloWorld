using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
	public static bool isSP = false;

	public Text text;
	
	
	void Start()
	{
		isSP = true;

		this.hasFound = false;
	}

	private bool hasFound;
	
	private int i = 0;

	private void FixedUpdate()
	{
		if ( i == 450 )
		{
			if ( !Main_Controller_def.found )
			{
				Main_Controller_def.Localisation (
					Main_Controller.Earth.regionlist [Random.Range ( 0, Main_Controller.Earth.regionlist.Count )] );
				this.hasFound = false;
			} else
			{
				this.hasFound = true;
				if (Main_Controller.symptoms.Count > 3 && Main_Controller_def.foundSymptoms.Count != Main_Controller.symptoms.Count){
					Main_Controller_def.ResearchSymp(Main_Controller.symptoms[Random.Range(0,Main_Controller.symptoms.Count)]);
				}
				else
				{
					if (Main_Controller_def.foundTransmitions.Count != Main_Controller.transmitions.Count)
						Main_Controller_def.ResearchTrans(Main_Controller.transmitions[Random.Range(0,Main_Controller.transmitions.Count)]);
					else
						Main_Controller_def.ResearchAntidote();
				}
			}
			i = 0;
		} else
		{
			i++;
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
