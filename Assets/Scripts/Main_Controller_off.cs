using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Main_Controller_off : MonoBehaviour {

	//variables
	public static int powerO = 10;
	

	
	//Attaque
	//Transmition
	private static bool ResHumUsed = false;
	public static bool ResHum()
	{
		if (!ResHumUsed && powerO - 5 >= 0)
		{
			ResHumUsed = true;
			powerO -= 5;
            Debug.Log("b1.1 button pressed");
            Main_Controller.transmitions.Add("Resistence a l'humidite");
			Main_Controller.HumidityRes += 10;
			return true;
		}
		return false;
	}
	public void ResHumButton()
	{
		if (ResHum());
	}
	
	
	private static bool ResTempUsed = false;
	public static bool ResTemp()
	{
		if (!ResTempUsed && powerO - 5 >= 0)
		{
			ResTempUsed = true;
			powerO -= 5;
            Debug.Log("b1.2 button pressed");
			Main_Controller.transmitions.Add("Resistence a la temperature");
			Main_Controller.tempRes += 10;
			return true;
		}
		return false;
	}
	public void ResTempButton()
	{
		if (ResTemp());
	}
	
	
	private static bool ResUsed = false;
	public static bool Res()
	{
		if (!ResUsed && powerO - 5 >= 0)
		{
			ResUsed = true;
			powerO -= 5;
			Main_Controller.transmitions.Add("Resistence au climat");
			Main_Controller.HumidityRes += 5;
			Main_Controller.tempRes += 5;
			return true;
		}

		return false;
	}
	public void ResButton()
	{
		if (Res())
			;
	}
	
	
	//Symptomes
	private static bool sneezingUsed = false;
	public static bool Sneezing()
	{
		if (!sneezingUsed && powerO - 5 >= 0)
		{
			sneezingUsed = true;
			powerO -= 5;
            Debug.Log("b2.1 button pressed");
			Main_Controller.symptoms.Add("Eternuements");
			Main_Controller.transmitionHuman += 0.1f;
			Main_Controller.virulence += 1;
			return true;
		}

		return false;
	}

	
	public void SneezingButton()
	{
		if (Sneezing())
			;
	}

	private static bool CoughUsed = false;
	public static bool Cough()
	{
		if (!CoughUsed &&powerO - 5 >= 0)
		{
			CoughUsed = true;
			powerO -= 5;
            Debug.Log("b2.2 button pressed");
			Main_Controller.symptoms.Add("Toux");
			Main_Controller.transmitionHuman += 0.05f;
			Main_Controller.virulence += 2;
			return true;
		}

		return false;
	}
	public void CoughButton()
	{
		if (Cough())
			;
	}

	
	private static bool SoreThroatUsed = true;
	public static bool SoreThroat()
	{
		if (!SoreThroatUsed && powerO - 5 >= 0)
		{
			SoreThroatUsed = true;
			powerO -= 5;
            Debug.Log("b2.3 button pressed");
			Main_Controller.symptoms.Add("Mal de Gorge");
			Main_Controller.virulence += 4;
			return true;
		}

		return false;
	}
	public void SoreThroatButton()
	{
		if (SoreThroat())
			;
	}

	
	private static bool HeartFailureUsed = false;
	public static bool HeartFailure()
	{
		if (!HeartFailureUsed && powerO - 20 >= 0)
		{
			HeartFailureUsed = true;
			powerO -= 20;
			Debug.Log("b2.4 button pressed");
			Main_Controller.symptoms.Add("Mal de Gorge");
			Main_Controller.virulence += 4;
			Main_Controller.lethality += 0.02f;
			return true;
		}

		return false;
	}
	public void HeartFailureButton()
	{
		if (HeartFailure())
			;
	}
}
