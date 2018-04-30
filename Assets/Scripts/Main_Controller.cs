using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Main_Controller : MonoBehaviour
{
	
	/////////////////////////////////////////////////////////
	/// Variable
	/////////////////////////////////////////////////////////

	
	
	

	/////////////////////////////////////////////////////////
	/// NETWORKING SHIT RIGHT HERE
	/////////////////////////////////////////////////////////
	
	private Client c;
	
	/////////////////////////////////////////////////////////
	/// NETWORKING SHIT RIGHT HERE
	/////////////////////////////////////////////////////////
	
	
	
	
	
	public class Region
	{
		public string Name;
		public long Population;
		public double Density;
		public double Life_expectancy;
		public double GDP;
		public int humidity;
		public int temp;
		
		public long infected = 0; 
		public long dead = 0;
		public float transmitionHuman = 0;
		public float transmitionOther = 0;
		public bool isClosed = false;
	}
	
	public class World : Region
	{
		public List<Region> regionlist;
	}

    //info sur l'UI
    public GameObject panelD;
    public GameObject panelO;
	public GameObject powerD;
	public GameObject powerO;
	public Text PowerDText;
	public Text PowerOText;
	public Text WorldDataText;
	public Text NaDataText;
	public Text SaDataText;
	public Text EuDataText;
	public Text AfricaDataText;
	public Text OceaniaDataText;
	public Text AsiaDataText;
	public List<Text> listText;
    public GameObject camera;
    public bool isDefending;

    //info sur la partie
	public static World Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
    public int time = 0;
	public long totalSane;
	public long totalInfected;
	public long totalDead;

	public GameObject MainControllerDefPrefab;
	public GameObject MainControllerOffPrefab;
	
	public Main_Controller_def mcd;
	public Main_Controller_off mco;
	
	public List <RandomEvent> eventsList;
	public int maxRand=50000;
	public int tempIndex;

	public static int Tcd = 50;
	public static int Scd = 50;
	
	//info sur le virus
	public static float transmitionHuman = 0f;
	public static float transmitionOther = 0f;
	public static float virulence = 0;
	public static float lethality = 0;
	public static int tempRes = 10;
	public static int HumidityRes = 10;
	public static List<string> symptoms = new List<string>();
	public static List<string> transmitions  = new List<string>();
	public static int startHum;
	public static int startTemp;
	public static float HighDensityRes = 0.15f;
	
	//Dictionnaire contenant toutes les info sur chaque competences:
	//Utilisation: Description["exemple"] retourne un string qui est sa description
	public readonly Dictionary<string, string> Description = new Dictionary<string, string> ()
	{
		//Defense
		//Gestion
		{"Fermeture temporaire", "Ferme temporairement une frontiere d'un pays a l'autre"},
		//Recherche
		{"Localisation", "Lance la recherche du virus dans le pays selectionne"},
		{"Recherche de Symptomes", "Lance une recherche visant les symptomes du virus, retourne un des symptomes du virus, s'il l'a"},
		{"Recherche de Transmitions", "Lance une recherche visant les transmitions du virus, retourne un des transmitions du virus, s'il l'a"},
		
		//Attaque
		//Transmition
		{"Resistence a l'humidite", "Le virus devient plus resistent au climat sec et humdide."},
		{"Resistence a la temperature", "Le virus devient plus resistent au chaud et au froid."},
		{"Resistence au climat", "Le virus est capable de survivre dans les regions les plus extremes."},// debloque apres avoir selectionne les deux precedent
		//Symptomes
		{"Eternuements","Provoque des eternuements, augmentant la transmition et la virulence."},
		{"Toux","Provoque une legere toux, augmentant la transmition et la virulence."},
		{"Mal de gorge","Provoque une legere toux, augmentant la virulence."},
		{"Crise cardiaque","Augmente la lethalite de votre virus"},
	};

	public static Region GetRegionFromName(string name)
	{
		for (int j = 0; j < Earth.regionlist.Count; j++)
		{
			if (Earth.regionlist[j].Name == name)
				return Earth.regionlist[j];
		}
		return null;
	}
	
	/////////////////////////////////////////////////////////
	/// Initialisation du gameplay
	/////////////////////////////////////////////////////////
	
	void Start ()
	{
		//panelD = Instantiate ( this.panelD );
		//panelO = Instantiate ( this.panelO );
		listText.Add(AsiaDataText);
		listText.Add(EuDataText);
		listText.Add(SaDataText);
		listText.Add(NaDataText);
		listText.Add(OceaniaDataText);
		listText.Add(AfricaDataText);


		c = FindObjectOfType <Client> ();
		if (c != null) 
		{
			isDefending = this.c.IsHost;
		} else 
		{
			if(isDefending == null)
				isDefending = false;
		}

		mcd = Instantiate ( this.MainControllerDefPrefab ).GetComponent<Main_Controller_def> ();
		mco = Instantiate ( this.MainControllerOffPrefab ).GetComponent<Main_Controller_off> ();

		mcd.mc = this;
		mco.mc = this;
		
		RandomEvents();
		
		StartTheGame ();
    }

	void StartTheGame ()
	{
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
		Earth.regionlist[0].infected = 1;
		Earth.regionlist[0].Population -= 1;
		startHum = Earth.regionlist[0].humidity;
		startTemp = Earth.regionlist[0].temp;

		//UI start
		powerD.SetActive(this.isDefending);	
		powerO.SetActive(!this.isDefending);
		panelD.SetActive(this.isDefending);	
		panelO.SetActive(!this.isDefending);

		if (camera == null)
			camera = Camera.main.transform.gameObject;
		camera.SetActive(true);
	}
	
	void RandomEvents()
	{
		eventsList=new List<RandomEvent>();
		eventsList.Add (new RandomEvent());
		eventsList[0].Init("test event", 0f, 0, 0f, 0, GetRegionFromName("Asia"));
	}
	
	/////////////////////////////////////////////////////////
	/// Interface
	/////////////////////////////////////////////////////////
	
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key pressed");
            if (isDefending)
            {
                if (panelD.activeInHierarchy)
                    panelD.SetActive(false);
                else
                    panelD.SetActive(true);
            }
            else
            {
                if (panelO.activeInHierarchy)
                    panelO.SetActive(false);
                else
                    panelO.SetActive(true);
            }
        }
		else if (Input.GetKeyDown(KeyCode.Escape))
	        SceneManager.LoadScene("main_menu");
    }

	
	/////////////////////////////////////////////////////////
	/// Gameplay
	/////////////////////////////////////////////////////////
	
	private int i = 1;

	private void FixedUpdate()
	{
		if (i % 25 == 0)
		{
			//update cooldowns
			if (Tcd > 0)
				Tcd--;
			if (Scd > 0)
				Scd--;
			
			totalSane = 0;
			totalInfected = 0;
			totalDead = 0;
			int j = 0;
			foreach (var region in Earth.regionlist)
			{
				//get info about the world
				totalSane += region.Population;
				totalInfected += region.infected;
				totalDead += region.dead;

				//check if climate is ok
				if (region.humidity <= startHum + HumidityRes && region.humidity >= startHum - HumidityRes &&
				    region.temp <= startTemp + tempRes && region.temp >= startTemp - tempRes)
				{
					//apply transmitions to region
					region.transmitionOther = transmitionOther;
					region.transmitionHuman = transmitionHuman;

					//if that region has 0 infected
					if (region.infected == 0)
					{
						if (Random.Range(0.1f, 10f) < transmitionOther)
						{
							region.infected = 1;
							region.Population -= 1;
						}
						else if (!region.isClosed && Random.Range(0.04f, 15f) < transmitionHuman)
						{
							region.infected = 1;
							region.Population -= 1;
						}

					}
				}

				//update infected
				if (region.Population != 0 && region.infected != 0)
				{
					long extraInfected = (long) ((transmitionHuman * 0.2f + transmitionOther * 0.1f) * (region.infected + 100));
					region.infected += extraInfected;
					region.Population -= extraInfected;

					if (region.Population < 0)
						region.Population = 0;
				}
				
				//update dead
				if (lethality != 0 && region.infected != 0)
				{
					long extraDead = (long) (region.infected * lethality) + 7;
					region.dead += extraDead;
					region.infected -= extraDead;
					if (region.infected < 0)
						region.infected = 0;
				}

				//update vaccined
				if (Main_Controller_def.vaccineFound && region.infected != 0)
				{
					long extraSane = (long) (region.infected * 0.1) + 7;
					region.Population += extraSane;
					region.infected -= extraSane;
					if (region.infected < 0)
						region.infected = 0;
				}
				
				//update randomEvents
				if(eventsList.Count>0)
				{
					if (Random.Range (1, maxRand)==1) 
					{
						tempIndex = Random.Range (0, eventsList.Count);
						eventsList [tempIndex].ApplyChanges();
						eventsList.Remove (eventsList [tempIndex]);
						maxRand -= 1;
					}
					if (maxRand <= 1) 
					{
						maxRand = 50000;
					}
				}

				//PowerO generation
				if (region.infected > 100)
				{
					if (region.infected < 1000 && region.infected % 100 < 4)
						Main_Controller_off.powerO++;
					else if (region.infected < 10000 && region.infected % 2000 < 25)
						Main_Controller_off.powerO += 2;
					else if (region.infected < 100000 && region.infected % 20000 < 50)
						Main_Controller_off.powerO += 3;
					else if (region.infected < 1000000 && region.infected % 200000 < 50)
						Main_Controller_off.powerO += 4;
				}

				//PowerD generation
				if (i == 50)
				{
					Main_Controller_def.powerD++;
					time++;
					i = 1;
				}
				Debug.Log("Infected: " + region.infected + " Dead:" + region.dead);
				
				
				//ui update
				listText[j++].text = region.infected + "\n" +
				                   (region.Population - region.infected) + "\n" +
				                   (region.Population - region.dead) + "\n" +
				                   region.dead;
			}
			WorldDataText.text = totalInfected + "\n" +
			                     totalSane + "\n" +
			                     (totalSane + totalInfected) + "\n" +
			                     totalDead;
			if (isDefending)
				PowerDText.text = mcd.PowerD.ToString();
			else
				PowerOText.text = mco.PowerO.ToString();
			//end ui update
			Debug.Log(Main_Controller_off.powerO);
			
			//check if game is over 
			if (totalInfected == 0 && totalSane == 0)
			{
				SceneManager.LoadScene("victoire");
			}
			if (totalInfected == 0)
			{
				SceneManager.LoadScene("defaite");
			}
		}
		i++;
	}

	public void OnSpellUsed (string spellName)
	{

		////// NETWORKING //////
		this.c.Send ( "CMOV|" + spellName );

		//this.player.OnSpellUsed ( spellName );
	}

	public void OnSpellUsed ( string spellName, Region country )
	{
		//this.player.OnSpellUsed ( spellName, country );
	}

	public static Region netRegion = null;
	public void OnRpcOnSpellUsedCallbackRegion(string msg, Region region)
	{
		netRegion = region;
		switch ( msg )
		{
			case "CloseBorder":
				mcd.CloseBorderButton ();
				break;
			case "Localisation":
				this.mcd.LocalisationButton ();
				break;
			default:
				Debug.Log ( "WTF?" );
				break;
		}
	}
		
	public void OnRpcOnSpellUsedCallback(string msg)
	{
		switch ( msg )
		{
			case "ResearchSymp":
				this.mcd.ResearchSympButton ();
				break;
			case "ResearchTrans":
				this.mcd.ResearchTransButton ();
				break;
			case "ResHum":
				this.mco.ResHumButton ();
				break;
			case "ResTemp":
				this.mco.ResTempButton ();
				break;
			case "Res":
				this.mco.ResButton ();
				break;
			case "Sneezing":
				this.mco.SneezingButton ();
				break;
			case "Cough":
				this.mco.CoughButton ();
				break;
			case "SoreThroat":
				mco.SoreThroatButton();
				break;
			case "HeartFailure":
				mco.HeartFailureButton ();
				break;
			case "Diarrhea":
				this.mco.DiarrheaButton ();
				break;
			case "Fever":
				this.mco.FeverButton ();
				break;
			case "Nausea":
				this.mco.NauseaButton ();
				break;
			default:
				Debug.Log ( "WTF?" );
				break;
		}
	}
}
