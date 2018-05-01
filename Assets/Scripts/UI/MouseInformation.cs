using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	public GameObject responsePanel;
	private GameObject panel;
	private Text ButtonChild;

	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["exemple"] retourne un string qui est sa description
	public readonly Dictionary<string, string> Description = new Dictionary<string, string>()
	{
		//Defense
		//Gestion
		{"Fermeture_temp", "Ferme temporairement une frontiere d'un pays a l'autre"},
		{"Localisation", "Lance la recherche du virus dans le pays selectionne"},
		//Recherche
		{"Recherche_sympt", "Lance une recherche visant les symptomes du virus, retourne un des symptomes du virus, s'il l'a"},
		{"Recherche_trans", "Lance une recherche visant les transmitions du virus, retourne un des transmitions du virus, s'il l'a"},
		{"Recherche_anti", "Lance une recherche de l'antidote" },

		//Attaque
		//Transmition
		{"Res_hum", "Le virus devient plus resistent au climat sec et humdide."},
		{"Res_temp", "Le virus devient plus resistent au chaud et au froid."},
		{"Res_climat", "Le virus est capable de survivre dans les regions les plus extremes."},
		// debloque apres avoir selectionne les deux precedent
		//Symptomes
		{"Eternuement","Provoque des eternuements, augmentant la transmition et la virulence."},
		{"Toux","Provoque une legere toux, augmentant la transmition et la virulence."},
		{"Mal_gorge","Provoque une legere toux, augmentant la virulence."},
		{"Diarrhee", "Provoque des pertes importantes d'eau et des hemoragies internes." },
		{"Fievre", "Provoque de fortes fièvres qui peuvent être fatales pour les plus fragiles." },
		{"Crise_car","Augmente la lethalite de votre virus"},
	};

	void Start () {
		//Debug.Log("Started program");
		ButtonChild = this.GetComponentInChildren<Text>();
	}

	public void OnPointerEnter(PointerEventData data)
	{
		panel = Instantiate(responsePanel);
		Text[] texts = panel.GetComponentsInChildren<Text>();
		texts[0].text = ButtonChild.text;
		texts[1].text = Description[this.name];
		responsePanel.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data)
	{
		//Debug.Log("--> Exited <--" + this.name +data);
		Destroy(panel);
	}
}
