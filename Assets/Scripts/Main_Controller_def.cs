using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Controller_def : MonoBehaviour {

	public static Main_Controller_def Instance; //for notification,see https://answers.unity.com/questions/753488/error-an-object-reference-is-required-to-access-no-2.html
	void Awake(){
		Instance = this;
		GameObject notif=GameObject.Find("Notifications");
	}

	public Main_Controller mc;
	
	//variables
	public static int powerD = 10;
	
	//info def
	private static List<string> gestion = new List<string>();
	private static List<string> research = new List<string>();
	public static List<string> foundSymptoms = new List<string>();
	public static List<string> foundTransmitions  = new List<string>();
	
	//Defense
	//Gestion
	public GameObject notif;
	private static bool closeBorderUsed = false;
	public static bool CloseBorder(Main_Controller.Region country)
	{
		if (!closeBorderUsed && powerD - 10 >= 0)
		{
			closeBorderUsed = true;
			gestion.Add("Fermeture temporaire");
			powerD -= 5;
			country.isClosed = true;
			return true;
		}

		return false;

	}
	public void CloseBorderButton(Main_Controller.Region region = null)
	{
		bool isUsed;
		if ( region == null )
		{
			isUsed = CloseBorder ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = CloseBorder ( region );
		}
		if (isUsed) 
			this.mc.OnSpellUsed ( "CloseBorder" );
	}
	
	//Recherche
	public static bool found = false;
	public static bool Localisation(Main_Controller.Region country)
	{
		if ( powerD - 2 >= 0 )
		{
			research.Add ( "Localisation" );
			powerD += 10;
			if ( country.infected != 0 )
			{
				Debug.Log ( true );
				found = true;
				//NOTIFICATION
				if (!AI.isSP)
				{
					Main_Controller_def.Instance.notif.GetComponent<Canvas>().enabled = true;
					Main_Controller_def.Instance.notif.GetComponent<Text>().text = "infected found in " + country.Name;
				}

				return true;
			} else
			{
				if (!AI.isSP)
				{
					Main_Controller_def.Instance.notif.GetComponent<Canvas>().enabled = true;
					Main_Controller_def.Instance.notif.GetComponent<Text>().text = "no infected found in " + country.Name;
				}

				return false;
			}
		}

		return false;
	}
	
	public void LocalisationButton(Main_Controller.Region country = null)
	{
		bool isUsed;
		if ( country == null )
		{
			isUsed = Localisation ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = Localisation ( country );
		}
		if(isUsed)
			this.mc.OnSpellUsed ( "Localisation" );
	}

	
	public static bool ResearchSymp()
	{
		if (found && powerD - 2 >= 0 && Main_Controller.symptoms.Count != 0)
		{
			research.Add("Recherche de Symptomes");
			
			powerD -= 2;
			string foundSymp = Main_Controller.symptoms[Random.Range(0,Main_Controller.symptoms.Count)];
			if (!foundSymptoms.Contains(foundSymp))
			foundSymptoms.Add(foundSymp);
			return true;
		}

		return false;
	}
	public void ResearchSympButton()
	{
		if(ResearchSymp())
			this.mc.OnSpellUsed ( "ResearchSymp" );
	}
	
	public static bool foundTrans = false;
	public static bool ResearchTrans()
	{
		if (found && powerD - 2 >= 0 && Main_Controller.transmitions.Count != 0)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 2;
			string foundTrans = Main_Controller.transmitions[Random.Range(0,Main_Controller.symptoms.Count)];
			if (!foundSymptoms.Contains(foundTrans)){
				foundSymptoms.Add(foundTrans);
				return true;
				}
		}
		return false;
	}
	public void ResearchTransButton()
	{
		if (ResearchTrans())
			this.mc.OnSpellUsed ( "ResearchTrans" );

	}
	
	public static bool vaccineFound = false;
	public static bool ResearchAntidote()
	{
		if (powerD - 5 >= 0 && 
		    foundTransmitions.Count == Main_Controller.transmitions.Count && 
		    foundSymptoms.Count == Main_Controller.symptoms.Count)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 5;
			Debug.Log(new System.Random().Next(0,Main_Controller.transmitions.Count));
			vaccineFound = true;
			return true;
		}
		return false;
	}
	public void ResearchAntidoteButton()
	{
		if (ResearchTrans())
			this.mc.OnSpellUsed ( "ResearchAntidote" );

	}
		
}
