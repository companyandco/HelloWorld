using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Controller_off : MonoBehaviour {

	//variables
	public static int powerO = 10;
	

	
	//Attaque
	//Transmition
	private static bool ResHumUsed = false;
	public static void ResHum()
	{
		if (!ResHumUsed && powerO - 5 >= 0)
		{
			ResHumUsed = true;
			powerO -= 5;
            Debug.Log("b1.1 button pressed");
            Main_Controller.transmitions.Add("Resistence a l'humidite");
			Main_Controller.HumidityRes += 10;
		}
	}
	public void ResHumButton()
	{
		ResHum();
	}
	
	
	private static bool ResTempUsed = false;
	public static void ResTemp()
	{
		if (!ResTempUsed && powerO - 5 >= 0)
		{
			ResTempUsed = true;
			powerO -= 5;
            Debug.Log("b1.2 button pressed");
			Main_Controller.transmitions.Add("Resistence a la temperature");
			Main_Controller.tempRes += 10;
		}
	}
	public void ResTempButton()
	{
		ResTemp();
	}
	
	
	private static bool ResUsed = false;
	public static void Res()
	{
		if (!ResUsed && powerO - 5 >= 0)
		{
			ResUsed = true;
			powerO -= 5;
            Debug.Log("b1.3 button pressed");
			Main_Controller.transmitions.Add("Resistence au climat");
			Main_Controller.HumidityRes += 5;
			Main_Controller.tempRes += 5;
		}
	}
	public void ResButton()
	{
		Res();
	}
	
	
	//Symptomes
	private static bool sneezingUsed = false;
	public static void Sneezing()
	{
		if (!sneezingUsed && powerO - 5 >= 0)
		{
			sneezingUsed = true;
			powerO -= 5;
            Debug.Log("b2.1 button pressed");
			Main_Controller.symptoms.Add("Eternuements");
			Main_Controller.transmitionHuman += 0.1f;
			Main_Controller.virulence += 1;
		}
	}

	
	public void SneezingButton()
	{
		Sneezing();
	}

	private static bool CoughUsed = false;
	public static void Cough()
	{
		if (!CoughUsed &&powerO - 5 >= 0)
		{
			CoughUsed = true;
			powerO -= 5;
            Debug.Log("b2.2 button pressed");
			Main_Controller.symptoms.Add("Toux");
			Main_Controller.transmitionHuman += 0.05f;
			Main_Controller.virulence += 2;
		}
	}
	public void CoughButton()
	{
		Cough();
	}

	
	private static bool SoreThroatUsed = true;
	public static void SoreThroat()
	{
		if (!SoreThroatUsed && powerO - 5 >= 0)
		{
			SoreThroatUsed = true;
			powerO -= 5;
            Debug.Log("b2.3 button pressed");
			Main_Controller.symptoms.Add("Mal de Gorge");
			Main_Controller.virulence += 4;
			//TODO envoyer string SoreThroat a l'autre joueur
		}
	}
	public void SoreThroatButton()
	{
		SoreThroat();
	}

	
	private static bool HeartFailureUsed = false;
	public static void HeartFailure()
	{
		if (!HeartFailureUsed && powerO - 20 >= 0)
		{
			HeartFailureUsed = true;
			powerO -= 20;
			Debug.Log("b2.4 button pressed");
			Main_Controller.symptoms.Add("Mal de Gorge");
			Main_Controller.virulence += 4;
			Main_Controller.lethality += 0.02f;
			//TODO envoyer string HeartFailure a l'autre joueur
		}
	}
	public void HeartFailureButton()
	{
		HeartFailure();
	}
}
