using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Country
{
	public string Name { get; set; }
	public long Population { get; set; }
	public double Density { get; set; }
	public double Life_expectancy { get; set; }
	public double GDP { get; set; }
}

public class Region : Country
{
}
	
public class World : Region
{
}

public class WorldData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		World Earth = new World ();
		Earth.Population = 7162119000;
		Region Asia = new Region ();
		Region Africa = new Region ();
		Region Europe = new Region ();
		Region North_America = new Region ();
		Region South_America = new Region ();
		Region Oceania = new Region ();
		List<Region> ContinentList=new List<Region>();
		ContinentList.Add(Asia);
		ContinentList.Add(Africa);
		ContinentList.Add(Europe);
		ContinentList.Add(South_America);
		ContinentList.Add(North_America);
		ContinentList.Add (Oceania);
		Asia.Name="Asia";
		Asia.Population = 4436224000;
		Asia.Density = 137;
		Asia.Life_expectancy = 69;
		Asia.GDP = 27224;

		Africa.Name="Africa";
		Africa.Population = 1216130000;
		Africa.Density = 40.6;
		Africa.Life_expectancy = 53;
		Africa.GDP = 3215;

		Europe.Name="Europe";
		Europe.Population = 738849000;
		Europe.Density = 32;
		Europe.Life_expectancy = 74;
		Europe.GDP = 19700;

		North_America.Name="North_America";
		North_America.Population = 579024000;
		North_America.Density = 22.9;
		North_America.Life_expectancy = 79;
		North_America.GDP = 20160;

		South_America.Name="South_America";
		South_America.Population = 422535000;
		South_America.Density = 22.8;
		South_America.Life_expectancy = 73;
		South_America.GDP = 3990;

		Oceania.Name="Oceania";
		Oceania.Population = 39901000;
		Oceania.Density = 4.5;
		Oceania.Life_expectancy = 75;
		Oceania.GDP = 1625;
	}
}