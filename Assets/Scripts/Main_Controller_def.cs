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

	//variables
	public static int powerD = 10;
	
	//info def
	private static List<string> gestion;
	private static List<string> research;
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
		if (CloseBorder(Main_Controller.GetRegionFromName(PlayerGameManager.lastContinentClicked)))
			//TODO;
	}
	//Recherche
	public static bool found = false;

	public static bool Localisation(Main_Controller.Region country)
	{
		if (powerD - 2 >= 0)
		{
			research.Add("Localisation");
			powerD += 10;
			if (country.infected != 0) {
				Debug.Log (true);
				found = true;
				//NOTIFICATION
				Main_Controller_def.Instance.notif.GetComponent<Canvas> ().enabled = true;
				Main_Controller_def.Instance.notif.GetComponent<Text> ().text = "infected found in " + country.Name;
				return true;
			} else {
				Main_Controller_def.Instance.notif.GetComponent<Canvas> ().enabled = true;
				Main_Controller_def.Instance.notif.GetComponent<Text> ().text = "no infected found in " + country.Name;
				return false;
			}
		}
	}
	public void LocalisationButton()
	{
		if (Localisation(Main_Controller.GetRegionFromName(PlayerGameManager.lastContinentClicked)))
			//TODO;
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
			;
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
			;
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
		if (ResearchTrans());
	}
		
}
