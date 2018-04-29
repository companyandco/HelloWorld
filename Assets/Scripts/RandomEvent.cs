using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent{

	// Random events occuring during the game, feel free to add attributes and Changes in ApplyChanges
	public string text;
	public float transmitionHuman;
	public int virulence;
	public float lethality;
	public int deceased;
	public Main_Controller.Region region;

	public void Init(string Text, float TransmitionHuman, int Virulence, float Lethality, int Deceased, Main_Controller.Region Region) 
	{
		text = Text;
		transmitionHuman = TransmitionHuman;
		virulence = Virulence;
		lethality = Lethality;
		deceased = Deceased;
		region = Region;
	}
	public void ApplyChanges (){ //( 0f, 0, 0f, 0, null) for no changes
		int toSub;
		if (region != null) {
			region.infected -= this.deceased / 2;
			if (region.infected < 0) {
				toSub = 0 - (int)region.infected;
				region.infected = 0;
				region.Population -= toSub+this.deceased-this.deceased/2;
				if(region.Population<0)
				{
					toSub = 0 - (int)region.Population;
					region.Population = 0;
					region.dead -= toSub;
				}
				region.dead += this.deceased;
			}

		}
		Main_Controller.transmitionHuman += this.transmitionHuman;
		Main_Controller.virulence += this.virulence;
		Main_Controller.lethality += this.lethality;
		Debug.Log ("Applied changes of an event : transmission+=" + this.transmitionHuman + " ,virulence+=" + this.virulence + "and lethality+=" + this.lethality + ". This event also killed " + this.deceased + " people in the region " + this.region.Name);
	}
}
