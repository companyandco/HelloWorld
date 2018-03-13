using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main_Controller : MonoBehaviour
{
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
    public static GameObject panelD;
    public static GameObject panelO;
    public static GameObject cameraD;
    public static GameObject cameraO;
    public static bool isDefending = true;

    //info sur la partie
	public World Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
    public int time = 0;
	public long totalSane;
	public long totalInfected;
	public long totalDead;
	private int power = 10;

    //info sur le virus
	private float transmitionHuman = 0f;
	private float transmitionOther = 0f;
	private int virulence = 0;
	private int lethality = 0;
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
		
	};
	
	
	//Competences :
	//Defense
	//Gestion
	public void CloseBorder(Region country1)
	{
		if (power - 10 >= 0)
		{
			gestion.Add("Fermeture temporaire");
			power -= 5;
			country1.isClosed = true;
		}

	}
	//Recherche
	public void Localisation(Region country)
	{
		if (power - 5 >= 0)
		{
			research.Add("Localisation");
			power -= 5;
			Debug.Log(country.infected != 0);
		}
	}
	
	public void ResearchSymp()
	{
		if (power - 5 >= 0)
		{
			research.Add("Recherche de Symptomes");
			power -= 5;
		}
		Debug.Log(new System.Random().Next(0,symptoms.Count));
	}
	
	public void ResearchTrans()
	{
		if (power - 5 >= 0)
		{
			research.Add("Recherche de Transmitions");
			power -= 5;
			Debug.Log(new System.Random().Next(0,transmitions.Count));
		}
	}

	//Attaque
	//Transmition
	public void ResHum()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b1.1 button pressed");
            transmitions.Add("Resistence a l'humidite");
			HumidityRes += 10;
		}
	}
	
	public void ResTemp()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b1.2 button pressed");
            transmitions.Add("Resistence a la temperature");
			tempRes += 10;
		}
	}
	
	public void Res()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b1.3 button pressed");
            transmitions.Add("Resistence au climat");
			HumidityRes += 5;
			tempRes += 5;
		}
	}
	
	//Symptomes
	public void Sneezing()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b2.1 button pressed");
            symptoms.Add("Eternuements");
			transmitionHuman += 0.1f;
			virulence += 1;
		}
	}
	
	public void Cough()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b2.2 button pressed");
            symptoms.Add("Toux");
			transmitionHuman += 0.05f;
			virulence += 2;
		}
	}
	public void SoreThroat()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b2.3 button pressed");
            symptoms.Add("Mal de Gorge");
			virulence += 4;
			//TODO envoyer string SoreThroat a l'autre joueur
		}
	}
	
	
	//Start
	
	void Start ()
	{
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
		Earth.regionlist[0].infected = 1;
		startHum = Earth.regionlist[0].humidity;
		startTemp = Earth.regionlist[0].temp;
		
		//recup de toute la population mondiale saine
		totalSane = 0;
		
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

	private void FixedUpdate()
	{
		foreach (var region in Earth.regionlist)
		{
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
					if (Random.Range(0f, 1f) < transmitionHuman)
					{
						region.infected = 1;
						region.Population -= 1;
					}
					else if (region.isClosed && Random.Range(0f, 1f) < transmitionOther)
					{
						region.infected = 1;
						region.Population -= 1;
					}

				}
			}

			if (region.Population != 0)
			{
				region.infected += region.infected * ((long)(transmitionHuman*10f) + (long)(transmitionOther*10f));
				region.Population -= region.infected * ((long)(transmitionHuman*10f) + (long)(transmitionOther*10f));
				
				if (region.Population < 0)
					region.Population = 0;
			}
			
			region.dead = region.infected * lethality;
			region.infected -= region.infected * lethality;

			Debug.Log(region.Population + " " + region.infected + " " + region.dead);

			
			
		}
		
	}
}
