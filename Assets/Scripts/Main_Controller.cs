﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main_Controller : MonoBehaviour
{
	
	
	/////////////////////////////////////////////////////////
	/// Variable
	/////////////////////////////////////////////////////////
	
	public class Region
	{
		public string Name;
		public long Population;
		public double Density;
		public double Life_expectancy;
		public double GDP;
		public int humidity;
		public int temp;
		
		public long infected = 0; 
		public long dead = 0;
		public float transmitionHuman = 0;
		public float transmitionOther = 0;
		public bool isClosed = false;
	}
	
	public class World : Region
	{
		public List<Region> regionlist;
	}

    //info sur l'UI
    public GameObject panelD;
    public GameObject panelO;
    public GameObject cameraD;
    public GameObject cameraO;
    public bool isDefending = true;

    //info sur la partie
	public World Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
    public int time = 0;
	public long totalSane;
	public long totalInfected;
	public long totalDead;
	public int powerO = 10;
	private int powerD = 10;

    //info sur le virus
	private float transmitionHuman = 0f;
	private float transmitionOther = 0f;
	private float virulence = 0;
	public float lethality = 0;
	private int tempRes = 10;
	private int HumidityRes = 10;
	private List<string> symptoms = new List<string>();
	private List<string> transmitions  = new List<string>();
	private int startHum;
	private int startTemp;
	
	//info def
	private List<string> gestion;
	private List<string> research;
	
	
	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["exemple"] retourne un string qui est sa description
	public readonly Dictionary<string, string> Description = new Dictionary<string, string> ()
	{
		//Defense
		//Gestion
		{"Fermeture temporaire", "Ferme temporairement une frontiere d'un pays a l'autre"},
		//Recherche
		{"Localisation", "Lance la recherche du virus dans le pays selectionne"},
		{"Recherche de Symptomes", "Lance une recherche visant les symptomes du virus, retourne un des symptomes du virus, s'il l'a"},
		{"Recherche de Transmitions", "Lance une recherche visant les transmitions du virus, retourne un des transmitions du virus, s'il l'a"},
		
		//Attaque
		//Transmition
		{"Resistence a l'humidite", "Le virus devient plus resistent au climat sec et humdide."},
		{"Resistence a la temperature", "Le virus devient plus resistent au chaud et au froid."},
		{"Resistence au climat", "Le virus est capable de survivre dans les regions les plus extremes."},// debloque apres avoir selectionne les deux precedent
		//Symptomes
		{"Eternuements","Provoque des eternuements, augmentant la transmition et la virulence."},
		{"Toux","Provoque une legere toux, augmentant la transmition et la virulence."},
		{"Mal de gorge","Provoque une legere toux, augmentant la virulence."},
		{"Crise cardiaque","Augmente la lethalite de votre virus"},
	};
	
	
	/////////////////////////////////////////////////////////
	/// Competences
	/////////////////////////////////////////////////////////
	//Defense
	//Gestion
	private bool isBorderClosed = false;
	public void CloseBorder(Region country1)
	{
		if (!isBorderClosed && powerD - 10 >= 0)
		{
			isBorderClosed = true;
			gestion.Add("Fermeture temporaire");
			powerD -= 5;
			country1.isClosed = true;
		}

	}
	//Recherche
	public void Localisation(Region country)
	{
		if (powerD - 2 >= 0)
		{
			research.Add("Localisation");
			powerD += 10;
			Debug.Log(country.infected != 0);
			
		}
	}
	
	public void ResearchSymp()
	{
		if (powerD - 2 >= 0)
		{
			research.Add("Recherche de Symptomes");
			powerD -= 2;
		}
		Debug.Log(new System.Random().Next(0,symptoms.Count));
	}
	
	public void ResearchTrans()
	{
		if (powerD - 2 >= 0)
		{
			research.Add("Recherche de Transmitions");
			powerD -= 2;
			Debug.Log(new System.Random().Next(0,transmitions.Count));
		}
	}

	//Attaque
	//Transmition
	private bool ResHumUsed = false;
	public void ResHum()
	{
		if (!ResHumUsed && powerO - 5 >= 0)
		{
			ResHumUsed = true;
			powerO -= 5;
            Debug.Log("b1.1 button pressed");
            transmitions.Add("Resistence a l'humidite");
			HumidityRes += 10;
		}
	}
	
	private bool ResTempUsed = false;
	public void ResTemp()
	{
		if (!ResTempUsed && powerO - 5 >= 0)
		{
			ResTempUsed = true;
			powerO -= 5;
            Debug.Log("b1.2 button pressed");
            transmitions.Add("Resistence a la temperature");
			tempRes += 10;
		}
	}
	
	private bool ResUsed = false;
	public void Res()
	{
		if (!ResUsed && powerO - 5 >= 0)
		{
			ResUsed = true;
			powerO -= 5;
            Debug.Log("b1.3 button pressed");
            transmitions.Add("Resistence au climat");
			HumidityRes += 5;
			tempRes += 5;
		}
	}
	
	//Symptomes
	private bool sneezingUsed = false;
	public void Sneezing()
	{
		if (!sneezingUsed && powerO - 5 >= 0)
		{
			sneezingUsed = true;
			powerO -= 5;
            Debug.Log("b2.1 button pressed");
            symptoms.Add("Eternuements");
			transmitionHuman += 0.1f;
			virulence += 1;
		}
	}

	private bool CoughUsed = false;
	public void Cough()
	{
		if (!CoughUsed &&powerO - 5 >= 0)
		{
			CoughUsed = true;
			powerO -= 5;
            Debug.Log("b2.2 button pressed");
            symptoms.Add("Toux");
			transmitionHuman += 0.05f;
			virulence += 2;
		}
	}

	private bool SoreThroatUsed = true;
	public void SoreThroat()
	{
		if (!SoreThroatUsed && powerO - 5 >= 0)
		{
			SoreThroatUsed = true;
			powerO -= 5;
            Debug.Log("b2.3 button pressed");
            symptoms.Add("Mal de Gorge");
			virulence += 4;
			//TODO envoyer string SoreThroat a l'autre joueur
		}
	}

	private bool HeartFailureUsed = false;
	public void HeartFailure()
	{
		if (!HeartFailureUsed && powerO - 20 >= 0)
		{
			HeartFailureUsed = true;
			powerO -= 20;
			Debug.Log("b2.4 button pressed");
			symptoms.Add("Mal de Gorge");
			virulence += 4;
			lethality += 0.02f;
			//TODO envoyer string HeartFailure a l'autre joueur
		}
	}
	
	
	/////////////////////////////////////////////////////////
	/// Initialisation du gameplay
	/////////////////////////////////////////////////////////
	
	void Start ()
	{
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
		Earth.regionlist[0].infected = 1;
		startHum = Earth.regionlist[0].humidity;
		startTemp = Earth.regionlist[0].temp;
		
		Debug.Log("Total population " + totalSane);

        //UI start
        if (panelD == null)
        {
            panelD = GameObject.Find("Panel_Defensive");
            panelD.SetActive(false);
        }
        if (panelO == null)
        {
            panelO = GameObject.Find("Panel_Offensive");
            panelO.SetActive(false);
        }

        if (cameraO == null)
        {
            cameraO = GameObject.Find("CameraOffensive");
            cameraO.SetActive(true);
        }
        if (cameraD == null)
        {
            cameraD = GameObject.Find("CameraDefensive");
            cameraD.SetActive(true);
        }
        //TODO: Change this whether we're defending or not.
		//TODO: Set the currently used camera to be the MainCamera (with the maincamera tag) so that ClickableEarth doesn't go crazy af.
        isDefending = false;

        if (isDefending)
        {
            cameraO.SetActive(false);
            cameraD.SetActive(true);
        }
        else
        {
            cameraO.SetActive(true);
            cameraD.SetActive(false);
        }
    }
	
	
	/////////////////////////////////////////////////////////
	/// Interface
	/////////////////////////////////////////////////////////
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            if (isDefending)
            {
                if (panelD.activeInHierarchy)
                    panelD.SetActive(false);
                else
                    panelD.SetActive(true);
            }
            else
            {
                if (panelO.activeInHierarchy)
                    panelO.SetActive(false);
                else
                    panelO.SetActive(true);
            }
        }
    }

	
	/////////////////////////////////////////////////////////
	/// Gameplay
	/////////////////////////////////////////////////////////
	
	private int i = 1;

	private void FixedUpdate()
	{
		if (i % 25 == 0)
		{
			totalSane = 0;
			totalInfected = 0;
			totalDead = 0;
			foreach (var region in Earth.regionlist)
			{
				//get info about the world
				totalSane += region.Population;
				totalInfected += region.infected;
				totalDead += region.dead;

				//check if climate is ok
				if (region.humidity <= startHum + HumidityRes && region.humidity >= startHum - HumidityRes &&
				    region.temp <= startTemp + tempRes && region.temp >= startTemp - tempRes)
				{
					//apply transmitions to region
					region.transmitionOther = transmitionOther;
					region.transmitionHuman = transmitionHuman;

					//if that region has 0 infected
					if (region.infected == 0)
					{
						if (Random.Range(0.1f, 1f) < transmitionOther)
						{
							region.infected = 1;
							region.Population -= 1;
						}
						else if (!region.isClosed && Random.Range(0.1f, 1f) < transmitionHuman)
						{
							region.infected = 1;
							region.Population -= 1;
						}

					}
				}

				if (region.Population != 0)
				{
					long extraInfected = (long) ((transmitionHuman * 0.2f + transmitionOther * 0.1f) * (region.infected + 100));
					region.infected += extraInfected;
					region.Population -= extraInfected;

					if (region.Population < 0)
						region.Population = 0;
				}

				long extraDead = (long) (region.infected * lethality);
				region.dead += extraDead;
				region.infected -= extraDead;

				//generation de powerO pour le joueur offensif
				if (region.infected > 200)
				{
					if (region.infected < 1000 && region.infected % 200 < 3)
						powerO++;
					else if (region.infected < 10000 && region.infected % 2000 < 5)
						powerO += 2;
					else if (region.infected < 100000 && region.infected % 20000 < 10)
						powerO += 3;
					else if (region.infected < 1000000 && region.infected % 200000 < 50)
						powerO += 4;
				}

				//generation passive pour le joueur defensif
				if (i == 50)
				{
					powerD++;
					time++;
					i = 1;
				}
					Debug.Log("Infected: " + region.infected + " Dead:" + region.dead);
			}
		}
		i++;
	}

}
