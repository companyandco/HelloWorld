using System.Collections;
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

	public static bool HasWon = false;
	public static string OpponentName = "Computer";
	
	

	/////////////////////////////////////////////////////////
	/// NETWORKING SHIT RIGHT HERE
	/////////////////////////////////////////////////////////
	
	private static Client c;
	
	/////////////////////////////////////////////////////////
	/// NETWORKING SHIT RIGHT HERE
	/////////////////////////////////////////////////////////
	
	
	
	
	
	public class Country
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

	public class Region:Country
	{
		public List<Country> countrylist;
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
	public GameObject StartingHelper;
    public GameObject camera;
    public static bool isDefending;
	public float FadeSpeed = 2f;

    //info sur la partie
	public static World Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
    public int time = 0;
	public long totalSane;
	public long totalInfected;
	public long totalDead;
    public static bool isStarted;
    public static Region StartRegion;

	//Info sur les events
	public static List <RandomEvent> eventsList;
	public int maxRandEvent;
	private int maxRand;
	public RandomEvent tempEvent;
	public Canvas notif;
	public Text Message;
	public Text Title;


	public static int Tcd = 50;
	public static int Scd = 50;
	public static string sanitaryBonus = "";
	
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
        ResetVariables();
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
			isDefending = c.IsHost;
		} else 
		{
			if(isDefending == null)
				isDefending = false;
		}
		if(!isDefending)
			StartingHelper.SetActive(true);
		else
			Destroy(StartingHelper);
		
		RandomEvents();

		StartTheGame ();
    }

	void StartTheGame ()
	{
		//TODO appel de l'UI demandant a l'utilisateur de selectionner une region
		//temp solution
        /*
		Earth.regionlist[0].infected = 1;
		Earth.regionlist[0].Population -= 1;
		startHum = Earth.regionlist[0].humidity;
		startTemp = Earth.regionlist[0].temp;*/

		//UI start

		
		if (camera == null)
		camera = Camera.main.transform.gameObject;
		camera.SetActive(true);
	}
	
	void RandomEvents()
	{
		maxRand = maxRandEvent;
		eventsList=new List<RandomEvent>();
		eventsList.Add (new RandomEvent());
		eventsList[0].Init("Tsunami","Un immense tsunami frappe l'Alaska !", 0f, 0, 0f, 1000, GetRegionFromName("NorthAmerica"));
	}

	void OpenNotification(string title, string message)
	{
		Title.text = title;
		Message.text = message;
		notif.enabled = true;
	}
	public void CloseNotification()
	{
		notif.enabled = false;
	}
		
	/////////////////////////////////////////////////////////
	/// Interface
	/////////////////////////////////////////////////////////
	
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.F) && isStarted)
        {
	        if (isDefending)
	        {
		        if (panelD.activeInHierarchy)
			        StartCoroutine(FadeOut(panelD.GetComponent<CanvasGroup>(), panelD));
		        else
			        StartCoroutine(FadeIn(panelD.GetComponent<CanvasGroup>(), panelD));
	        }
	        else
	        {
		        if (panelO.activeInHierarchy)
			        StartCoroutine(FadeOut(panelO.GetComponent<CanvasGroup>(), panelO));
		        else
			        StartCoroutine(FadeIn(panelO.GetComponent<CanvasGroup>(), panelO));
	        }
        }
		else if ( Input.GetKeyDown ( KeyCode.Escape ) )
        {
	        ResetVariables ();
	        //SceneSelection.LoadNewScreen("main_menu"); cannot be done due to nonstatic in static context
			SceneManager.LoadScene ( "main_menu" );
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			SceneManager.LoadScene("SinglePlayerEvents");
		}
	}
	
	
	IEnumerator FadeIn(CanvasGroup obj, GameObject toactive = null)
	{
		float curTime = obj.alpha;
		if (toactive != null && !toactive.activeInHierarchy)
			toactive.SetActive(true);
		while (curTime <= 1)
		{
			obj.alpha = curTime;
			curTime += Time.deltaTime * FadeSpeed;
			yield return null;
		}
	}
	
	IEnumerator FadeOut(CanvasGroup obj, GameObject todisable = null)
	{
		float curTime = obj.alpha;
		while (curTime >= 0)
		{
			obj.alpha = curTime;
			curTime -= Time.deltaTime * FadeSpeed;
			yield return null;
		}
		if (todisable != null && todisable.activeInHierarchy)
			todisable.SetActive(false);
	}
	

	public static void ResetVariables ()
	{
		Earth = null;
		Earth = WorldData.ReadFromJsonFile("Assets/WorldInfos.json");
		/*
		time = 0;
		totalSane = 0;
		totalInfected = 0;
		totalDead = 0;
		*/
		
		//RandomEvents ();

		Tcd = 50;
		Scd = 50;
		sanitaryBonus = "";

		transmitionHuman = 0;
		transmitionOther = 0;
		virulence = 0;
		lethality = 0;
		tempRes = 10;
		HumidityRes = 10;
		symptoms = new List <string> ();
		transmitions = new List <string> ();
		startHum = Earth.regionlist [0].humidity;
		startTemp = Earth.regionlist [0].temp;
		HighDensityRes = 0.15f;
        isStarted = false;
        StartRegion = null;

		Main_Controller_def.found = false;
		Main_Controller_def.foundSymptoms = new List <string> ();
		Main_Controller_def.foundTrans = false;
		Main_Controller_def.isBoostUsed = false;
		Main_Controller_def.isVaccinateAnimalsUsed = false;
		Main_Controller_def.powerD = 10;
		Main_Controller_def.vaccineFound = false;

		Main_Controller_off.CoughUsed = false;
		Main_Controller_off.DepressionUsed = false;
		Main_Controller_off.diarrheaUsed = false;
		Main_Controller_off.feverUsed = false;
		Main_Controller_off.FluUsed = false;
		Main_Controller_off.HeartFailureUsed = false;
		Main_Controller_off.HighDensityResUsed = false;
		Main_Controller_off.InsomniaUsed = false;
		Main_Controller_off.isAttackAnimalsUsed = false;
		Main_Controller_off.nauseaUsed = false;
		Main_Controller_off.ParalisysUsed = false;
		Main_Controller_off.powerO = 30;
		Main_Controller_off.ResHumUsed = false;
		Main_Controller_off.ResTempUsed = false;
		Main_Controller_off.ResUsed = false;
		Main_Controller_off.sneezingUsed = false;
		Main_Controller_off.SoreThroatUsed = false;
		Main_Controller_off.StrokeUsed = false;
	}
	
	/////////////////////////////////////////////////////////
	/// Gameplay
	/////////////////////////////////////////////////////////
	
	private int i = 1;

	private void FixedUpdate()
	{
        if (isStarted && i % 25 == 0)
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
                    if (!isDefending && region.infected == 0)
                    {
                        if (Random.Range(0.1f, 10f) < transmitionOther)
                        {
                            region.infected = 1;
                            OnSpellUsed("NewRegionInfected", region.Name);
                            region.Population -= 1;
                        }
                        else if (!region.isClosed && Random.Range(0.04f, 15f) < transmitionHuman)
                        {
                            region.infected = 1;
                            OnSpellUsed("NewRegionInfected", region.Name);
                            region.Population -= 1;
                        }

                    }
                }

                //update infected
                if (region.Population != 0 && region.infected != 0)
                {
                    long extraInfected = (long)((transmitionHuman * 0.2f + transmitionOther * 0.1f) * (region.infected + 100));
                    region.infected += extraInfected;
                    region.Population -= extraInfected;

                    if (region.Population < 0)
                        region.Population = 0;
                }

                //update dead
                if (lethality != 0 && region.infected != 0)
                {
                    long extraDead = (long)(region.infected * lethality) + 7;
                    region.dead += extraDead;
                    region.infected -= extraDead;
                    if (region.infected < 0)
                        region.infected = 0;
                }

                //update vaccined
                if (Main_Controller_def.vaccineFound && region.infected != 0)
                {
                    long extraSane;
                    if (region.Name == sanitaryBonus)
                        extraSane = (long)(region.infected * 0.2) + 7;
                    else
                        extraSane = (long)(region.infected * 0.1) + 7;

                    region.Population += extraSane;
                    region.infected -= extraSane;
                    if (region.infected < 0)
                        region.infected = 0;
                }

                //update randomEvents
                if (!isDefending && eventsList.Count > 0)
                {

                    if (Random.Range(1, maxRand) == 1)
                    {

                        tempEvent = eventsList[Random.Range(0, eventsList.Count)];
                        tempEvent.ApplyChanges();
                        OpenNotification(tempEvent.title, tempEvent.message);
                        eventsList.Remove(tempEvent);
                    }
                    if (maxRand <= 1)
                    {
                        maxRand = maxRandEvent;
                    }
                    maxRand -= 1;
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
                //Debug.Log("Infected: " + region.infected + " Dead:" + region.dead);


                //ui update -BEGIN
                listText[j++].text = region.infected + "\n" +
                                   region.Population + "\n" +
                                   (region.Population + region.infected) + "\n" +
                                   region.dead;
            }
            WorldDataText.text = totalInfected + "\n" +
                                 totalSane + "\n" +
                                 (totalSane + totalInfected) + "\n" +
                                 totalDead;
            //ui update -END
            if (isDefending)
                PowerDText.text = Main_Controller_def.powerD.ToString();
            else
                PowerOText.text = Main_Controller_off.powerO.ToString();
            //end ui update
            //Debug.Log(Main_Controller_off.powerO);

            //check if game is over 
            if (totalInfected == 0 && totalSane == 0)
            {
                HasWon = !isDefending;
                SceneManager.LoadScene("victoire");
                return;
            }
            if (totalInfected == 0)
            {
                HasWon = isDefending;
                SceneManager.LoadScene("defaite");
                return;
            }
        }
        else if (isStarted && StartRegion != null)
        {
            StartRegion.infected = 1;
            StartRegion.Population -= 1;
            startHum = StartRegion.humidity;
            startTemp = StartRegion.temp;

            powerD.SetActive(isDefending);
            powerO.SetActive(!isDefending);
            panelD.SetActive(isDefending);
            panelO.SetActive(!isDefending);
            StartRegion = null;
        }
        else if (!isStarted)
        {
            if (PlayerGameManager.lastContinentClicked != "Oceans" && PlayerGameManager.lastContinentClicked != null)
            {
                isStarted = true;
                StartRegion = GetRegionFromName(PlayerGameManager.lastContinentClicked);
                OnSpellUsed("RegionSelected", PlayerGameManager.lastContinentClicked);
            }
        }
        else
        {
            i++;
        }
	}

 	public static void OnSpellUsed (string spellName)
	{
		if (!AI.isSP)
			c.Send ( "CSPELL|" + spellName );
	}

	public static void OnSpellUsed ( string spellName, string value )
	{
		if (!AI.isSP)
			c.Send ( "CSPELLR|" + spellName + "|" + value);
	}

	public static Region netRegion = null;
	public static void OnRpcOnSpellUsedCallbackRegion(string msg, string value)
	{
		switch ( msg )
		{
			case "RandomEvent":
				eventsList [int.Parse(value)].ApplyChanges();
				eventsList.Remove (eventsList [int.Parse(value)]);
				break;
			case "NewRegionInfected":
				Region region = GetRegionFromName(value);
				region.infected = 1;
				region.Population -= 1;
				break;
			case "CloseBorder":
				Main_Controller_def.CloseBorder(GetRegionFromName(value));
				break;
			case "Localisation":
				Main_Controller_def.Localisation(GetRegionFromName(value));
				break;
			case "ResearchSymp":
				//FIXME
				Main_Controller_def.ResearchSymp(value);
				break;
			case "ResearchTrans":
				//FIXME
				Main_Controller_def.ResearchTrans(value);
				break;
			case "SanitaryCampaign":
				Main_Controller_def.SanitaryCampaign ( GetRegionFromName ( value ) );
				break;
            case "RegionSelected":
                isStarted = true;
                StartRegion = GetRegionFromName(value);
                break;
            default:
				Debug.Log ( "WTF?" );
				break;
		}
	}
		
	public static void OnRpcOnSpellUsedCallback(string msg)
	{
		switch ( msg )
		{
			//def
			case "ResHum":
				Main_Controller_off.ResHum();
				break;
			case "ResTemp":
				Main_Controller_off.ResTemp();
				break;
			case "Res":
				Main_Controller_off.Res();
				break;
			case "Sneezing":
				Main_Controller_off.Sneezing();
				break;
			case "Cough":
				Main_Controller_off.Cough();
				break;
			case "SoreThroat":
				Main_Controller_off.SoreThroat();
				break;
			case "HeartFailure":
				Main_Controller_off.HeartFailure();
				break;
			case "Diarrhea":
				Main_Controller_off.Diarrhea();
				break;
			case "Fever":
				Main_Controller_off.Fever();
				break;
			case "Nausea":
				Main_Controller_off.Nausea();
				break;
			case "ResearchAntidote":
				Main_Controller_def.ResearchAntidote ();
				break;
			case "Boost":
				Main_Controller_def.Boost ();
				break;
			case "VaccinateAnimals":
				Main_Controller_def.VaccinateAnimals ();
				break;
			case "BetterHygiene":
				Main_Controller_def.BetterHygiene ();
				break;
			case "HighDensityRes":
				Main_Controller_off.HighDensityRes ();
				break;
			case "Depression":
				Main_Controller_off.Depression ();
				break;
			case "Flu":
				Main_Controller_off.Flu ();
				break;
			case "Insomnia":
				Main_Controller_off.Insomnia ();
				break;
			case "Stroke":
				Main_Controller_off.Stroke ();
				break;
			case "Paralisys":
				Main_Controller_off.Paralisys ();
				break;
			case "AttackAnimals":
				Main_Controller_off.AttackAnimals ();
				break;
			default:
				Debug.Log ( "WTF?" );
				break;
		}
	}
}
