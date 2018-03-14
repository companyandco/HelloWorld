using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
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
	public static World Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
    public int time = 0;
	public long totalSane;
	public long totalInfected;
	public long totalDead;
	
	//info sur le virus
	public static float transmitionHuman = 0f;
	public static float transmitionOther = 0f;
	public static float virulence = 0;
	public static float lethality = 0;
	public static int tempRes = 10;
	public static int HumidityRes = 10;
	public static List<string> symptoms = new List<string>();
	public static List<string> transmitions  = new List<string>();
	public static int startHum;
	public static int startTemp;
	
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

	public static Region GetRegionFromName(string name)
	{
		for (int j = 0; j < Earth.regionlist.Count; j++)
		{
			if (Earth.regionlist[j].Name == name)
				return Earth.regionlist[j];
		}
		return null;
	}
	
	/////////////////////////////////////////////////////////
	/// Initialisation du gameplay
	/////////////////////////////////////////////////////////
	
	void Start ()
	{
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
		Earth.regionlist[0].infected = 1;
		Earth.regionlist[0].Population -= 1;
		startHum = Earth.regionlist[0].humidity;
		startTemp = Earth.regionlist[0].temp;

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
					if (region.infected < 1000 && region.infected % 100 < 4)
						Main_Controller_off.powerO++;
					else if (region.infected < 10000 && region.infected % 2000 < 25)
						Main_Controller_off.powerO += 2;
					else if (region.infected < 100000 && region.infected % 20000 < 50)
						Main_Controller_off.powerO += 3;
					else if (region.infected < 1000000 && region.infected % 200000 < 50)
						Main_Controller_off.powerO += 4;
				}

				//generation passive pour le joueur defensif
				if (i == 50)
				{
					Main_Controller_def.powerD++;
					time++;
					i = 1;
				}
					Debug.Log("Infected: " + region.infected + " Dead:" + region.dead);
					Debug.Log(Main_Controller_off.powerO);
			}
		}
		i++;
	}

}
