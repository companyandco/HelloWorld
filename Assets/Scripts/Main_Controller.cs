using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Main_Controller : MonoBehaviour
{
	//////////////////////////////////////////
	public class Country
	{
		public string Name { get; set; }
		public long Population { get; set; }
		public double Density { get; set; }
		public double Life_expectancy { get; set; }
		public double GDP { get; set; }
		//simon pls
		public long humidity = 50;
		public long temp = 14;
		
		public long infected = 0;
		public long dead = 0;
		public long transmitionHuman = 0;
		public long transmitionOther = 0;
		public bool isClosed = false;


	}

	public class Region : Country
	{
	}
	
	public class World : Region
	{
	}
	
	private List<Region> RegionList;
    //////////////////////////////////////////

    //info sur l'UI
    public static GameObject panel1;
    public static GameObject panel2;
    public static bool isDefending = true;

    //info sur la partie
    public static int time = 0;
	public static long totalSane;
	public static long totalInfected;
	private int power;

    //info sur le virus
	private int transmitionHuman = 0;
	private int transmitionOther = 0;
	private int virulence = 0;
	private int lethality = 0;
	private int tempRes;
	private int HumidityRes;
	private List<String> symptoms;
	private List<String> transmitions;
	private int startHum;
	private int startTemp;
	
	//info def
	private List<String> gestion;
	private List<String> research;
	
	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["exemple"] retourne un string qui est sa description
	
	public readonly Dictionary<String, String> Description = new Dictionary<String, String>
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
	public void CloseBorder(Country country1)
	{
		if (power - 10 >= 0)
		{
			gestion.Add("Fermeture temporaire");
			power -= 5;
			country1.isClosed = true;
		}

	}
	//Recherche
	public bool Localisation(Country country)
	{
		if (power - 5 >= 0)
		{
			research.Add("Localisation");
			power -= 5;
			return country.infected != 0;
		}
		return false;
	}
	
	public String ResearchSymp()
	{
		if (power - 5 >= 0)
		{
			research.Add("Recherche de Symptomes");
			power -= 5;
			return symptoms[new System.Random().Next(0,symptoms.Count)];
		}
		return "";
	}
	
	public String ResearchTrans()
	{
		if (power - 5 >= 0)
		{
			research.Add("Recherche de Transmitions");
			power -= 5;
			return transmitions[new System.Random().Next(0,transmitions.Count)];
		}
		return "";
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
			transmitionHuman += 10;
			virulence += 1;
		}
	}
	
	public void Cough()
	{
		if (power - 5 >= 0)
		{
            Debug.Log("b2.2 button pressed");
            symptoms.Add("Toux");
			transmitionHuman += 5;
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
		}
	}
	
	
	//Start
	
	void Start ()
	{
		//////////////////////////////////////////
		World Earth = new World ();
		Earth.Population = 7162119000;
		Region Asia = new Region ();
		Region Africa = new Region ();
		Region Europe = new Region ();
		Region North_America = new Region ();
		Region South_America = new Region ();
		Region Oceania = new Region ();
		RegionList =new List<Region>();
		RegionList.Add(Asia);
		RegionList.Add(Africa);
		RegionList.Add(Europe);
		RegionList.Add(South_America);
		RegionList.Add(North_America);
		RegionList.Add (Oceania);
		Asia.Name="Asia";
		Asia.Population = 4436224000;
		Asia.Density = 137;
		Asia.Life_expectancy = 69;
		Asia.GDP = 27224;

		Africa.Name="Africa";
		Africa.Population = 1216130000;
		Africa.Density = 40.6;
		Africa.Life_expectancy = 53;
		Africa.GDP = 3215;

		Europe.Name="Europe";
		Europe.Population = 738849000;
		Europe.Density = 32;
		Europe.Life_expectancy = 74;
		Europe.GDP = 19700;

		North_America.Name="North_America";
		North_America.Population = 579024000;
		North_America.Density = 22.9;
		North_America.Life_expectancy = 79;
		North_America.GDP = 20160;

		South_America.Name="South_America";
		South_America.Population = 422535000;
		South_America.Density = 22.8;
		South_America.Life_expectancy = 73;
		South_America.GDP = 3990;

		Oceania.Name="Oceania";
		Oceania.Population = 39901000;
		Oceania.Density = 4.5;
		Oceania.Life_expectancy = 75;
		Oceania.GDP = 1625;
		//////////////////////////////////////////
		
		
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
		RegionList[0].infected = 1;
		
		totalSane = 0;
		foreach (var region in RegionList)
		{
			totalSane += region.Population;
		}
		Debug.Log("Total population " + totalSane);
		totalInfected = 1;
		power = 10;
		
		//TODO recuperer la temperature moyenne  de la region et son humidite
		
		tempRes = 14;
		HumidityRes = 50;

		symptoms = new List<String>();
		transmitions = new List<String>();

		if ( panel1 == null )
		{
            panel1 = GameObject.Find ( "Panel_Defensive" );
			panel1.SetActive ( false );
		}
        if ( panel2 == null )
        {
            panel2 = GameObject.Find ( "Panel_Offensive" );
			panel2.SetActive ( false );
		}
		
		//TODO: Change this whether we're defending or not.
        isDefending = true;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            if (isDefending)
            {
                if (panel1.activeInHierarchy)
                    panel1.SetActive(false);
                else
                    panel1.SetActive(true);
            }
            else
            {
                if (panel2.activeInHierarchy)
                    panel2.SetActive(false);
                else
                    panel2.SetActive(true);
            }
        }
    }

	private void FixedUpdate()
	{
		foreach (var region in RegionList)
		{
			if (region.humidity <= startHum + HumidityRes && region.humidity >= startHum - HumidityRes &&
			    region.temp <= startTemp + tempRes && region.temp >= startTemp - tempRes)
			{
				region.transmitionOther = transmitionOther;
				if (!region.isClosed)
					region.transmitionHuman = transmitionHuman;
			}

			region.infected = region.infected * (transmitionHuman + transmitionOther);
			region.Population -= region.infected * (transmitionHuman + transmitionOther);
			if (region.Population < 0)
			{
				region.infected += region.Population;
				region.Population = 0;
			}
			region.dead = region.infected * lethality;
			region.infected -= region.infected * lethality;
			

		}
		
	}
}
