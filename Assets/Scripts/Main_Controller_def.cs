using System;
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
	#region defense

	//Gestion
	#region gestion

	#region closeborder

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
			isUsed = CloseBorder ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = CloseBorder ( Main_Controller.netRegion );
			Main_Controller.netRegion = null;
		}
		if (isUsed)
			Main_Controller.OnSpellUsed ( "CloseBorder" );
	}

	#endregion

	#endregion

	//Recherche
	#region recherche

	#region localisation

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
			isUsed = Localisation ( Main_Controller.GetRegionFromName ( PlayerGameManager.lastContinentClicked ) );
		} else
		{
			isUsed = Localisation ( Main_Controller.netRegion );
			Main_Controller.netRegion = null;
		}
		if(isUsed)
			Main_Controller.OnSpellUsed ( "Localisation" );
	}

	#endregion

	#region researchsymp

	public static bool ResearchSymp(string symptom)
	{
		if (Main_Controller.Scd == 0 && found && powerD - 2 >= 0 && Main_Controller.symptoms.Count != 0)
		{
			research.Add("Recherche de Symptomes");
			powerD -= 2;
			Main_Controller.Scd = (int)(15-Main_Controller.virulence);;
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
			Main_Controller.OnSpellUsed ( "ResearchSymp" );
	}

	#endregion

	#region researchtrans

	public static bool foundTrans = false;
	public static bool ResearchTrans(string transmition)
	{
		if (Main_Controller.Tcd == 0 && found && powerD - 2 >= 0 && Main_Controller.transmitions.Count != 0)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 2;
			Main_Controller.Tcd = (int)(15-Main_Controller.virulence);
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
			Main_Controller.OnSpellUsed ( "ResearchTrans" );

	}

	#endregion

	#region researchantidote

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
			Main_Controller.OnSpellUsed ( "ResearchAntidote" );

	}

	#endregion

	#region sanitarycampaign

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
			Main_Controller.OnSpellUsed ( "SanitaryCampaign" );
	}

	#endregion

	#region betterhygiene

	public static bool BetterHygiene ()
	{
		if (Main_Controller.Tcd == 0 && powerD - 5 >= 0)
		{
			powerD -= 5;
			
			Main_Controller.Tcd = 5;

			Main_Controller.transmitionHuman -= 0.1f;

			Main_Controller.transmitionOther -= 0.1f;

			Main_Controller.virulence -= 2;

			return true;
		}

		return false;
	}

	public void BetterHygieneButton ()
	{
		if (BetterHygiene ())
			Main_Controller.OnSpellUsed ( "BetterHygiene" );
	}

	#endregion

	#region vaccinateanimals

	public static bool isVaccinateAnimalsUsed = false;

	public static bool VaccinateAnimals ()
	{
		if ( Main_Controller.Tcd == 0 && powerD - 3 >= 0 )
		{
			powerD -= 3;
			
			Main_Controller.transmitionOther = 0.1f;

			Main_Controller.Tcd = 5;

			isVaccinateAnimalsUsed = true;

			return true;
		}

		return false;
	}

	public void VaccinateAnimalButton ()
	{
		if (!isVaccinateAnimalsUsed && VaccinateAnimals ())
			Main_Controller.OnSpellUsed ( "VaccinateAnimals" );
	}

	#endregion

	#region boost

	public static bool isBoostUsed = false;

	//TODO: another boost
	public static bool Boost ()
	{
		if ( powerD - 2 >= 0 )
		{
			powerD -= 2;
			
			Main_Controller.Tcd /= 2;

			isBoostUsed = true;

			return true;
		}

		return false;
	}

	public void BoostButton ()
	{
		if (Boost ())
			Main_Controller.OnSpellUsed ( "Boost" );
	}

	#endregion

	#endregion

	#endregion

}
