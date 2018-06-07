using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	public GameObject responsePanel;
	private GameObject panel;
	private GameObject button;

	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["exemple"] retourne un string qui est sa description
	
	public static readonly List<string> Transmition = new List<string>()
	{
		"Res_hum", "Res_temp", "Res_climat", "High_density_res", "Infection_animale"
	};
	
	public static readonly List<string> Symptomes = new List<string>()
	{
		"Eternuement", "Toux", "Mal_gorge", "Diarrhee", "Fievre", "Crise_car", "Nausee", "Depression", "Grippe", "Insomnie", "AVC", "Paralysie"
	};

	public static string ToReadableString(string str)
	{
		switch (str)
		{
			case "Fermeture_temp":
				return "Fermeture temporaire";
			case "Sanitary_campaign":
				return "Campagne sanitaire";
			case "Vaccinate_animals":
				return "Vacciner les animaux";
			case "Recherche_sympt":
				return "Recherche de Symptômes";
			case "Recherche_trans":
				return "Recherche de transmissions";
			case "Recherche_anti":
				return "Recherche de l'antidote";
			case "Localisation":
				return "Localisation";
			case "Res_hum":
				return "Resistance Humidité";
			case "Res_temp":
				return "Resistance Temperature";
			case "Res_climat":
				return "Resistance Climat";
			case "High_density_res":
				return "Resistance population dense";
			case "Infection_animale":
				return "Infection Animale";
			case "Eternuement":
				return "Eternuements";
			case "Toux":
				return "Eternuements";
			case "Mal_gorge":
				return "Mal de Gorge";
			case "Diarrhee":
				return "Diarrhée";
			case "Fievre":
				return "Fiévre";
			case "Crise_car":
				return "Crise cardiaque";
			case "Nausee":
				return "Nausée";
			case "Depression":
				return "Dépression";
			case "Grippe":
				return "Grippe";
			case "Insomnie":
				return "Insomnie";
			case "AVC":
				return "A V C";
			case "Paralysie":
				return "Paralysie";
			default:
				return "";
		}
	}
	
	
	public static readonly Dictionary<string, string> Description = new Dictionary<string, string>()
	{
		//Defense
		//Gestion
		{
			"Fermeture_temp", 
			"Ferme temporairement une frontiere d'un pays a l'autre"
		},
		{
			"Sanitary_campaign",
			"Envoyer des équipes médicales pour donner le traitement contre le virus à la population"
		},
		{
			"Vaccinate_animals",
			"Vacciner les animaux afin de réduire la transmission autre"
		},
		//Recherche
		{
			"Recherche_sympt", 
			"Lance une recherche visant les symptomes du virus, retourne un des symptomes du virus, s'il l'a"
		},
		{
			"Recherche_trans", 
			"Lance une recherche visant les transmitions du virus, retourne un des transmitions du virus, s'il l'a"
		},
		{
			"Recherche_anti", 
			"Lance une recherche de l'antidote"
		},
		{
			"Localisation",
			"Lance la recherche du virus dans le pays selectionne"
		},

		//Attaque
		//Transmition
		{
			"Res_hum", 
			"Le virus devient plus resistent au climat sec et humdide."
		},
		{
			"Res_temp", 
			"Le virus devient plus resistent au chaud et au froid."
		},
		{
			"Res_climat", 
			"Le virus est capable de survivre dans les regions les plus extremes."
		},
		{
			"High_density_res",
			"Le virus est capable de survivre plus longtemps et de se propager plus rapidement dans les regions à grande population."
		},
		{
			"Infection_animale",
			"Les porteurs du virus risquent d'infecter les animaux (augmente la transmission autre)"
		},
		
		// debloque apres avoir selectionne les deux precedent
		//Symptomes
		{
			"Eternuement",
			"Provoque des eternuements, augmentant la transmition et la virulence."
		},
		{
			"Toux",
			"Provoque une legere toux, augmentant la transmition et la virulence."
		},
		{
			"Mal_gorge",
			"Provoque une legere toux, augmentant la virulence."
		},
		{
			"Diarrhee", 
			"Provoque des pertes importantes d'eau et des hemoragies internes."
		},
		{
			"Fievre",
			"Provoque de fortes fièvres qui peuvent être fatales pour les plus fragiles."
		},
		{
			"Crise_car",
			"Augmente la lethalite de votre virus"
		},
		{
			"Nausee",
			"Les porteurs du virus ont la nausee"
		},
		{
			"Depression",
			"Les porteurs du virus sont dépressifs"
		},
		{
			"Grippe",
			"Les porteurs du virus ont la grippe"
		},
		{
			"Insomnie",
			"Les porteurs du virus sont sujets à des insomnies"
		},
		{
			"AVC",
			"Les porteurs du virus risquent de faire un AVC"
		},
		{
			"Paralysie",
			"Les porteurs du virus sont paralysés"
		},
	};

	public void OnPointerEnter(PointerEventData data)
	{
		panel = Instantiate(responsePanel);
		Text[] texts = panel.GetComponentsInChildren<Text>();
		texts[0].text = ToReadableString(this.name);
		texts[1].text = Description[this.name];
		responsePanel.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data)
	{
		Destroy(panel);
	}

	private void OnDisable()
	{
		Destroy(panel);
	}
}
