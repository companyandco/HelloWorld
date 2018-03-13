using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent : Region
{

	private string name;

	public string Name
	{
		get { return this.name; }
		set { this.name = value; }
	}
	
	public Continent ( string name )
	{
		this.name = name;
	}
	
}
