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
	
	public int PowerD
	{
		get { return powerD; }
	}

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
	public void CloseBorderButton()
	{
		bool isUsed;
		if ( Main_Controller.netRegion == null )
		{
//			isUsed = CloseBorder ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = CloseBorder ( Main_Controller.netRegion );
			Main_Controller.netRegion = null;
		}
/*
		if (isUsed) 
			this.mc.OnSpellUsed ( "CloseBorder" );
*/	}
	
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
	
	public void LocalisationButton()
	{
		bool isUsed;
		if ( Main_Controller.netRegion == null )
		{
//			isUsed = Localisation ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = Localisation ( Main_Controller.netRegion );
			Main_Controller.netRegion = null;
		}
		/*
		if(isUsed)
			this.mc.OnSpellUsed ( "Localisation" );
		*/
	}

	
	public static bool ResearchSymp(string symptom)
	{
		if (Main_Controller.Scd == 0 && found && powerD - 2 >= 0 && Main_Controller.symptoms.Count != 0)
		{
			research.Add("Recherche de Symptomes");
			powerD -= 2;
			Main_Controller.Scd = 10;
			//string foundSymp = Main_Controller.symptoms[Random.Range(0,Main_Controller.symptoms.Count)];
			if (!foundSymptoms.Contains(symptom))
			foundSymptoms.Add(symptom);
			return true;
		}

		return false;
	}
	public void ResearchSympButton()
	{
		if(ResearchSymp("a mettre"))//TODO output la valeur la liste ici
			this.mc.OnSpellUsed ( "ResearchSymp" );
	}
	
	public static bool foundTrans = false;
	public static bool ResearchTrans(string transmition)
	{
		if (Main_Controller.Tcd == 0 && found && powerD - 2 >= 0 && Main_Controller.transmitions.Count != 0)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 2;
			Main_Controller.Tcd = 10;
			//string foundTrans = Main_Controller.transmitions[Random.Range(0,Main_Controller.symptoms.Count)];
			if (!foundSymptoms.Contains(transmition)){
				foundSymptoms.Add(transmition);
				return true;
				}
		}
		return false;
	}
	public void ResearchTransButton()
	{
		if (ResearchTrans("a mettre"))//TODO output la valeur la liste ici
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
		if (ResearchAntidote())
			this.mc.OnSpellUsed ( "ResearchAntidote" );

	}
	
	public static bool SanitaryCampaign(Main_Controller.Region region)
	{
		if (powerD - 5 >= 0 && vaccineFound)
		{
			research.Add("Sanitary campaign");
			powerD -= 5;
			Main_Controller.sanitaryBonus = region.Name;
			return true;
		}
		return false;
	}
	public void sanitaryCampaignButton()
	{
		if (ResearchAntidote())
			this.mc.OnSpellUsed ( "SanitaryCampaign" );

	}
	
	
	
		
}
