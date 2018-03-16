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
		if ( i == 500 )
		{
			if ( !Main_Controller_def.found )
			{
				Main_Controller_def.Localisation (
					Main_Controller.Earth.regionlist [Random.Range ( 0, Main_Controller.Earth.regionlist.Count )] );
				this.hasFound = false;
			} else
			{
				this.hasFound = true;
				if (Main_Controller_def.foundSymptoms.Count != Main_Controller.symptoms.Count){
					Main_Controller_def.ResearchSymp();
				}
				else
				{
					if (Main_Controller_def.foundTransmitions.Count != Main_Controller.transmitions.Count)
						Main_Controller_def.ResearchTrans();
					else
						Main_Controller_def.ResearchAntidote();
				}
			}
			i = 0;
		} else
		{
			i++;
		}

		if ( this.hasFound )
		{
			this.text.text = "Ai has found antidote: \n" + "true";
		} else
		{
			this.text.text = "Ai has found antidote: \n" + "false";			
		}
	}
}
