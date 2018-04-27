using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Main_Controller_off : MonoBehaviour {

	//variables
	public static int powerO = 30;

	public Main_Controller mc;
	
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
		if (ResHum() && !AI.isSP)
			this.mc.OnSpellUsed ( "ResHum" );
	}
	
	
	private static bool ResTempUsed = false;
	public static bool ResTemp()
	{
		if (!ResTempUsed && powerO - 5 >= 0)
		{
			powerO -= 5;
			ResTempUsed = true;
            Debug.Log("b1.2 button pressed");
			Main_Controller.transmitions.Add("Resistence a la temperature");
			Main_Controller.tempRes += 10;
			return true;
		}
		return false;
	}
	public void ResTempButton()
	{
		if (ResTemp() && !AI.isSP)
			this.mc.OnSpellUsed ( "ResTemp" );
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
		if (Res() && !AI.isSP)
			this.mc.OnSpellUsed ( "Res" );
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
		if (Sneezing() && !AI.isSP)
			this.mc.OnSpellUsed ( "Sneezing" );
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
		if (Cough() && !AI.isSP)
			this.mc.OnSpellUsed ( "Cough" );
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
			Main_Controller.transmitionHuman += 0.001f;
			return true;
		}

		return false;
	}
	public void SoreThroatButton()
	{
		if (SoreThroat() && !AI.isSP)
			this.mc.OnSpellUsed ( "SoreThroat" );
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
		if (HeartFailure() && !AI.isSP)
			this.mc.OnSpellUsed ( "HeartFailure" );
	}
	
	private static bool diarrheaUsed = false;
	public static bool Diarrhea()
	{
		if (!diarrheaUsed && powerO - 10 >= 0)
		{
			diarrheaUsed = true;
			powerO -= 10;
			Debug.Log("b2.5 button pressed");
			Main_Controller.symptoms.Add("Diarrhee");
			Main_Controller.transmitionHuman += 0.05f;
			Main_Controller.virulence += 2;
			Main_Controller.lethality += 0.01f;
			return true;
		}

		return false;
	}
	public void DiarrheaButton()
	{
		if (Diarrhea() && !AI.isSP)
			this.mc.OnSpellUsed ( "Diarrhea" );
	}
	private static bool feverUsed = false;
	public static bool Fever()
	{
		if (!feverUsed && powerO - 5 >= 0)
		{
			feverUsed = true;
			powerO -= 5;
			Debug.Log("b2.6 button pressed");
			Main_Controller.symptoms.Add("Fievre");
			Main_Controller.virulence += 3;
			Main_Controller.lethality += 0.01f;
			return true;
		}

		return false;
	}
	public void FeverButton()
	{
		if (Fever() && !AI.isSP)
			this.mc.OnSpellUsed ( "Fever" );
	}

	private static bool nauseaUsed = false;
	public static bool Nausea()
	{
		if (!nauseaUsed && powerO - 5 >= 0)
		{
			nauseaUsed = true;
			powerO -= 5;
			Debug.Log("b2.7 button pressed");
			Main_Controller.symptoms.Add("Nausee");
			Main_Controller.transmitionHuman += 0.2f;
			Main_Controller.virulence += 1;
			Main_Controller.lethality += 0.005f;
			return true;
		}

		return false;
	}
	public void NauseaButton()
	{
		if (Nausea() && !AI.isSP)
			this.mc.OnSpellUsed ( "Nausea" );
	}
	
	private static bool DepressionUsed = false;
	public static bool Depression()
	{
		if (!DepressionUsed && powerO - 5 >= 0)
		{
			DepressionUsed = true;
			powerO -= 5;
			Main_Controller.symptoms.Add("Depression");
			Main_Controller.transmitionHuman -= 0.05f;
			Main_Controller.virulence += 1;
			Main_Controller.lethality += 0.007f;
			return true;
		}

		return false;
	}
	public void DepressionButton()
	{
		if (Nausea() && !AI.isSP)
			this.mc.OnSpellUsed ( "Depression" );
	}
	
	private static bool DepressionUsed = false;
	public static bool Depression()
	{
		if (!DepressionUsed && powerO - 5 >= 0)
		{
			DepressionUsed = true;
			powerO -= 5;
			Main_Controller.symptoms.Add("Depression");
			Main_Controller.transmitionHuman -= 0.05f;
			Main_Controller.virulence += 1;
			Main_Controller.lethality += 0.007f;
			return true;
		}

		return false;
	}
	public void DepressionButton()
	{
		if (Depression() && !AI.isSP)
			this.mc.OnSpellUsed ( "Depression" );
	}
	
	
	private static bool FluUsed = false;
	public static bool Flu()
	{
		if (!FluUsed && powerO - 5 >= 0)
		{
			FluUsed = true;
			powerO -= 5;
			Main_Controller.symptoms.Add("Flu");
			Main_Controller.transmitionHuman += 0.3f;
			Main_Controller.virulence += 2;
			Main_Controller.lethality += 0f;
			return true;
		}

		return false;
	}
	public void FluButton()
	{
		if (Flu() && !AI.isSP)
			this.mc.OnSpellUsed ( "Flu" );
	}
	
	private static bool InsomniaUsed = false;
	public static bool Insomnia()
	{
		if (!InsomniaUsed && powerO - 5 >= 0)
		{
			InsomniaUsed = true;
			powerO -= 5;
			Main_Controller.symptoms.Add("Insomnia");
			Main_Controller.virulence += 1;
			Main_Controller.lethality += 0.007f;
			return true;
		}

		return false;
	}
	public void InsomniaButton()
	{
		if (Insomnia() && !AI.isSP)
			this.mc.OnSpellUsed ( "Insomnia" );
	}
	
	private static bool StrokeUsed = false;
	public static bool Stroke()
	{
		if (!StrokeUsed && powerO - 5 >= 0)
		{
			StrokeUsed = true;
			powerO -= 5;
			Main_Controller.symptoms.Add("Stroke");
			Main_Controller.transmitionHuman -= 0.05f;
			Main_Controller.virulence += 5;
			Main_Controller.lethality += 0.1f;
			return true;
		}

		return false;
	}
	public void StrokeButton()
	{
		if (Stroke() && !AI.isSP)
			this.mc.OnSpellUsed ( "Stroke" );
	}

}
