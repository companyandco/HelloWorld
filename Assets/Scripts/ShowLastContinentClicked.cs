using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLastContinentClicked : MonoBehaviour
{

	
	public GameObject go;
	private static Text t;

	public Text Title;
	public Text BaseInfo;
	public Text PopInfo;
	public GameObject CountryInfo;
	public static GameObject CountryInforamtion;
	public static Text TitleInformation;
	public static Text BasicInformation;
	public static Text PopulationInformation;

	public static string continent;

	void Start ()
	{		
		t = go.GetComponent<Text>();
		CountryInforamtion = CountryInfo;
		TitleInformation = Title;
		BasicInformation = BaseInfo;
		PopulationInformation = PopInfo;
	}

	public static void UpdateText()
	{
		if (continent != null || continent != "")
			t.text = continent;
		else
			t.text = "None";
	}

	public static void ActivateInformationWindow()
	{
		if (Main_Controller.isStarted)
		{
			string[] namelist = continent.Split(new char[] {','});
			string regionName = "";
			if(namelist.Length == 2)
				regionName = namelist[1].Substring(1);

			Main_Controller.Region region = Main_Controller.GetRegionFromName(regionName);
			Main_Controller.Country country = Main_Controller.GetCountryFromName(continent);
			if (region == null || country == null)
				CountryInforamtion.SetActive(false);
			else
			{
                string humidty = "";
                string temp = "";
                switch (country.average_humidity)
                {
                    case -2:
                        humidty = "très faible";
                        break;
                    case -1:
                        humidty = "faible";
                        break;
                    case 1:
                        humidty = "élevé";
                        break;
                    case 2:
                        humidty = "très élevé";
                        break;
                    default:
                        humidty = "normal";
                        break;
                }
                switch (country.average_temp)
                {
                    case -2:
                        temp = "très faible";
                        break;
                    case -1:
                        temp = "faible";
                        break;
                    case 1:
                        temp = "élevé";
                        break;
                    case 2:
                        temp = "très élevé";
                        break;
                    default:
                        temp = "normal";
                        break;
                }
                CountryInforamtion.SetActive(true);
				TitleInformation.text = continent;
				BasicInformation.text = country.Population + "\n" + 
				                        country.Density + "\n" +
				                        country.Life_expectancy + "\n" + 
				                        country.GDP + "\n" + 
				                        humidty + "\n" + 
				                        temp;
				PopulationInformation.text = country.infected + "\n" +
				                             country.Population + "\n" +
				                             (country.infected + country.Population) + "\n" +
				                             country.dead;
			}
		}
	}
}
