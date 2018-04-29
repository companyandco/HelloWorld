using System;
using System.Collections.Generic;
using UnityEngine;

public class EarthGeneration : MonoBehaviour
{
	public GameObject EarthPrefab;

	public static Dictionary <string, List <string>> WorldInfos;

	[NonSerialized]
	public List <Continent> Continents;
	
	void Start ()
	{
		WorldInfos = new Dictionary <string, List <string>> ();

		PopulateDictionnary ();

		this.Continents = new List <Continent> ();
		
		GameObject earth = Instantiate ( this.EarthPrefab );

		earth.name = "Earth";

		GameObject continents = null;

		for ( int i = 0; i < earth.transform.childCount; i++ )
		{
			Transform t = earth.transform.GetChild ( i );

			if ( t.name == "Continents" )
				continents = t.gameObject;
		}

		if ( continents == null )
		{
			Debug.LogError ( "Couldn't find the instantiated \"Continents\" GameObjet." );
			return;
		}

		for ( int i = 0; i < continents.transform.childCount; i++ )
		{
			Transform t = continents.transform.GetChild ( i );

			Continent c = t.gameObject.AddComponent <Continent> ();

			c.ContinentName = t.name;

			c.SetValues ( WorldInfos[c.ContinentName] );

			this.Continents.Add ( c );
		}

		string s = "";

		foreach ( Continent c in Continents )
		{
			s += c.ContinentName;
			s += " ";
		}
		
		Debug.Log ( s );
		
	}

	void PopulateDictionnary ()
	{
		List <string> africaStats = new List <string> () {"1216130000", "40.6", "53.0", "3215.0", "56", "25"};
		List <string> eurasiaStats = new List <string> () {"5175073000", "84.5", "71.5", "54448.0", "70", "13"};
		List <string> northamericaStats = new List <string> () {"579024000", "22.9", "79.0", "20160.0", "77", "11"};
		List <string> oceaniaStats = new List <string> () {"39901000", "4.5", "75.0", "1625.0", "71", "17"};
		List <string> southamericaStats = new List <string> () {"422535000", "22.8", "73.0", "3990.0", "61", "16"};

		WorldInfos ["Africa"] = africaStats;
		WorldInfos ["Eurasia"] = eurasiaStats;
		WorldInfos ["NorthAmerica"] = northamericaStats;
		WorldInfos ["Oceania"] = oceaniaStats;
		WorldInfos ["SouthAmerica"] = southamericaStats;
	}
}