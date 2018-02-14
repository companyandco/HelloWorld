using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class Main_Controller : MonoBehaviour
{

	public static int time = 0;
	public static int totalSane;
	public static int totalInfected;
	private int power;

	private int transmiton = 0;
	private int virulence = 0;
	private int chanceToKill = 0;
	private int tempRes;
	private int HumidityRes;
	
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
