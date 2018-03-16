using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
/*
public class Country
{
	public string Name { get; set; }
	public long Population { get; set; }
	public double Density { get; set; }
	public double Life_expectancy { get; set; }
	public double GDP { get; set; }
	public double average_humidity { get; set; }
    public double average_temp { get; set; }
}

public class Region : Country
{
}
	
public class World : Region
{
	public List<Region> regionlist { get; set; }
}
*/


public class WorldData : MonoBehaviour {
	public static Main_Controller.World ReadFromJsonFile(string filePath)
	{
		return JsonConvert.DeserializeObject<Main_Controller.World>(File.ReadAllText(filePath));
	}
}
