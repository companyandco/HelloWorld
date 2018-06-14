using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent{

	// Random events occuring during the game, feel free to add attributes and Changes in ApplyChanges
	public string title;
	public string message;
	public float transmitionHuman;
	public int virulence;
	public float lethality;
	public int deceased;
	public Main_Controller.Country country;

	public void Init(string Title, string Message, float TransmitionHuman, int Virulence, float Lethality, int Deceased, Main_Controller.Country Country) 
	{
		title = Title;
		message = Message;
		transmitionHuman = TransmitionHuman;
		virulence = Virulence;
		lethality = Lethality;
		deceased = Deceased;
		country = Country;
	}
	public void ApplyChanges (){ //( 0f, 0, 0f, 0, null) for no changes
		long toSub;
		long deads=0;
		if (country != null)
		{
			toSub = country.infected / country.Population * this.deceased;
			if (country.infected > toSub) 
			{
				country.infected -= toSub;
			}
			country.Population -= (this.deceased - toSub);
			if (country.Population < 0) {
				deads += this.deceased + country.Population;
				country.Population = 0;
			}
			else 
			{
				deads = this.deceased;
			}
			country.dead += deads;

		}
		Main_Controller.transmitionHuman += this.transmitionHuman;
		Main_Controller.virulence += this.virulence;
		Main_Controller.lethality += this.lethality;
		Debug.Log ("Applied changes of an event : transmission+=" + this.transmitionHuman + " ,virulence+=" + this.virulence + "and lethality+=" + this.lethality + ". This event also killed " + this.deceased + " people in the country " + this.country.Name+", "+Main_Controller.GetContinent (country).Name);
	}
}
