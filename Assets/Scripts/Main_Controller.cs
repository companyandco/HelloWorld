using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class Main_Controller : MonoBehaviour
{

	//info sur la partie
	public static int time = 0;
	public static int totalSane;
	public static int totalInfected;
	private int power;

    //info sur le virus
	private int transmiton = 0;
	private int virulence = 0;
	private int chanceToKill = 0;
	private int tempRes;
	private int HumidityRes;
	private List<String> symptoms;
	private List<String> transmitions;
	
	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["Purge"] retourne un string qui est sa description
	
	public readonly Dictionary<String, String> Description = new Dictionary<String, String>
	{
		{"Localisation", "Lance la recherche du virus dans le pays selectionne"},
		{"Recherche de Symptomes", "Lance une recherche visant les symptomes du virus, retourne un des symptomes du virus, s'il l'a"},
		{"Recherche de Transmitions", "Lance une recherche visant les transmitions du virus, retourne un des transmitions du virus, s'il l'a"}
	};
	
	
	//Competences :
	
	public bool Localisation(int infected)
	{
		if (power - 5 >= 0)
		{
			power -= 5;
			return infected != 0;
		}
		return false;
	}

	public String ResearchSymp()
	{
		if (power - 5 >= 0)
		{
			power -= 5;
			return symptoms[new System.Random().Next(0,symptoms.Count)];
		}
		return "";
	}
	
	public String ResearchTrans()
	{
		if (power - 5 >= 0)
		{
			power -= 5;
			return transmitions[new System.Random().Next(0,transmitions.Count)];
		}
		return "";
	}
	
	
	//Start
	
	void Start ()
	{
		
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region

		totalSane = 0; //TODO fonction qui fait la somme de toute les populations de regions
		totalInfected = 1;
		power = 10;
		
		//TODO recuperer la temperature moyenne  de la region et son humidite
		tempRes = 14;
		HumidityRes = 50;


		symptoms = new List<String>();
		transmitions = new List<String>();
	}
	
	void Update () {
		
	}
}
