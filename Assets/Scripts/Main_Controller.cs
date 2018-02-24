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
	
	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["Purge"] retourne un string qui est sa description
	
	public readonly Dictionary<String, String> Description = new Dictionary<String, String>
	{
		{"Localisation", "Lance la recherche du virus dans le pays selectionne "},
		{"Oxygen", "Le virus se multiplie au contact de l'air, augmentant sa transmition"}
	};
	
	
	void Start ()
	{
		
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region

		totalSane = 0; //TODO fonction qui fait la somme de toute les populations de regions
		totalInfected = 1;
		power = 10;
		
		//TODO recuperer la temperature moyenne  de la region et son humidite
		tempRes = 14;
		HumidityRes = 50;

	}
	
	void Update () {
		
	}
}
