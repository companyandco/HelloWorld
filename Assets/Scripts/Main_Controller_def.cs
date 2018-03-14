using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Controller_def : MonoBehaviour {

	//variables
	public static int powerD = 10;
	
	//info def
	private static List<string> gestion;
	private static List<string> research;
	
	//Defense
	//Gestion
	private static bool closeBorderUsed = false;
	public static void CloseBorder(Main_Controller.Region country)
	{
		if (!closeBorderUsed && powerD - 10 >= 0)
		{
			closeBorderUsed = true;
			gestion.Add("Fermeture temporaire");
			powerD -= 5;
			country.isClosed = true;
		}

	}
	public void CloseBorderButton()
	{
		CloseBorder(Main_Controller.GetRegionFromName(GameManager.lastContinentClicked));
	}
	//Recherche
	public static bool found = false;
	public static void Localisation(Main_Controller.Region country)
	{
		if (powerD - 2 >= 0)
		{
			research.Add("Localisation");
			powerD += 10;
			if (country.infected != 0)
			{
				Debug.Log(true);
				found = true;
				//TODO NOTIFICATION
			}
		}
	}
	public void LocalisationButton(Main_Controller.Region country)
	{
		Localisation(country);
	}
	
	public static void ResearchSymp()
	{
		if (found && powerD - 2 >= 0)
		{
			research.Add("Recherche de Symptomes");
			powerD -= 2;
		}
		Debug.Log(new System.Random().Next(0,Main_Controller.symptoms.Count));
	}
	public void ResearchSympButton()
	{
		ResearchSymp();
	}
	
	public static void ResearchTrans()
	{
		if (found && powerD - 2 >= 0)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 2;
			Debug.Log(new System.Random().Next(0,Main_Controller.transmitions.Count));
		}
	}
	public void ResearchTransButton()
	{
		ResearchTrans();
	}
}
