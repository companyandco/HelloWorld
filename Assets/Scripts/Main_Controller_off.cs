using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Main_Controller_off : MonoBehaviour {

	//variables
	public static int powerO = 30;

	public Main_Controller mc;

	//Attaque
	#region attack

	//Transmition
	#region transmission

	#region reshum

	public static bool ResHumUsed = false;
	public static bool ResHum()
	{
		if (!ResHumUsed && powerO - 5 >= 0)
		{
			ResHumUsed = true;
			powerO -= 5;
			Main_Controller.transmitions.Add("Resistence a l'humidite");
			Main_Controller.HumidityRes += 10;
			return true;
		}
		return false;
	}
	
	public void ResHumButton()
	{
		if (ResHum() && !AI.isSP)
			Main_Controller.OnSpellUsed ( "ResHum" );
	}

	#endregion

	#region restemp

	public static bool ResTempUsed = false;
	public static bool ResTemp()
	{
		if (!ResTempUsed && powerO - 5 >= 0)
		{
			powerO -= 5;
			ResTempUsed = true;
			Main_Controller.transmitions.Add("Resistence a la temperature");
			Main_Controller.tempRes += 10;
			return true;
		}
		return false;
	}
	
	public void ResTempButton()
	{
		if (ResTemp() && !AI.isSP)
			Main_Controller.OnSpellUsed ( "ResTemp" );
	}

	#endregion

	#region res
	
	public static bool ResUsed = false;
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
			Main_Controller.OnSpellUsed ( "Res" );
	}

	#endregion

	#region highdensityres

	public static bool HighDensityResUsed = false;
	public static bool HighDensityRes()
	{
		if (!ResUsed && powerO - 5 >= 0)
		{
			HighDensityResUsed = true;
			powerO -= 5;
			Main_Controller.transmitions.Add("Bonus haute densité");
			Main_Controller.HighDensityRes += 0.05f;
			return true;
		}

		return false;
	}
	
	public void HighDensityResButton()
	{
		if (Res() && !AI.isSP)
			Main_Controller.OnSpellUsed ( "HighDensityRes" );
	}

	#endregion
	
	#endregion
	
	//Symptomes
	#region symptomes

	#region sneezing

	public static bool sneezingUsed = false;
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
			Main_Controller.OnSpellUsed ( "Sneezing" );
	}

	#endregion

	#region cough

	public static bool CoughUsed = false;
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
			Main_Controller.OnSpellUsed ( "Cough" );
	}

	#endregion

	#region sorethroat

	public static bool SoreThroatUsed = true;
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
			Main_Controller.OnSpellUsed ( "SoreThroat" );
	}

	#endregion

	#region heartfailure

	public static bool HeartFailureUsed = false;
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
			Main_Controller.OnSpellUsed ( "HeartFailure" );
	}

	#endregion

	#region diarrhea

	public static bool diarrheaUsed = false;
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
			Main_Controller.OnSpellUsed ( "Diarrhea" );
	}

	#endregion

	#region fever

	public static bool feverUsed = false;
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
			Main_Controller.OnSpellUsed ( "Fever" );
	}

	#endregion

	#region nausea

	public static bool nauseaUsed = false;
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
			Main_Controller.OnSpellUsed ( "Nausea" );
	}

	#endregion

	#region depression

	public static bool DepressionUsed = false;
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
			Main_Controller.OnSpellUsed ( "Depression" );
	}

	#endregion

	#region flu

	public static bool FluUsed = false;
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
			Main_Controller.OnSpellUsed ( "Flu" );
	}

	#endregion

	#region insomnia

	public static bool InsomniaUsed = false;
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
			Main_Controller.OnSpellUsed ( "Insomnia" );
	}

	#endregion
	
	#region stroke
	
	public static bool StrokeUsed = false;
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
			Main_Controller.OnSpellUsed ( "Stroke" );
	}
	
	#endregion
	
	#region paralisys 

	public static bool ParalisysUsed = false;
	public static bool Paralisys ()
	{
		if ( !ParalisysUsed && powerO - 10 >= 0 )
		{
			ParalisysUsed = true;
			powerO -= 10;
			Main_Controller.symptoms.Add ( "Paralisys" );

			Main_Controller.virulence += 2;
			
			Main_Controller.lethality += 0.2f;

			return true;
		}
		return false;
	}
	
	public void ParalisysButton ()
	{
		if (Paralisys () && !AI.isSP)
			Main_Controller.OnSpellUsed ( "Paralisys" );
	}
	
	#endregion

	#region attackanimals

	public static bool isAttackAnimalsUsed = false;

	public static bool AttackAnimals ()
	{
		if ( !isAttackAnimalsUsed && powerO - 5 >= 0 )
		{
			isAttackAnimalsUsed = true;

			powerO -= 5;
			
			Main_Controller.symptoms.Add ( "Animals" );

			Main_Controller.transmitionOther += 0.5f;
			
			return true;
		}

		return false;
	}

	public void AttackAnimalsButton ()
	{
		if (AttackAnimals ())
			Main_Controller.OnSpellUsed ( "AttackAnimals" );
	}

	#endregion

	#endregion

	#endregion

}
