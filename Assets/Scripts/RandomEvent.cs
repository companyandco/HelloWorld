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
		long toSub;
		long deads=0;
		if (region != null)
		{
			toSub = region.infected / region.Population * this.deceased;
			region.infected -= toSub;
			region.Population -= this.deceased - toSub;
			if (region.Population < 0) {
				deads += this.deceased + region.Population;
			}
			else 
			{
				deads = this.deceased;
			}
			region.dead += deads;
		}
		Main_Controller.transmitionHuman += this.transmitionHuman;
		Main_Controller.virulence += this.virulence;
		Main_Controller.lethality += this.lethality;
		Debug.Log ("Applied changes of an event : transmission+=" + this.transmitionHuman + " ,virulence+=" + this.virulence + "and lethality+=" + this.lethality + ". This event also killed " + this.deceased + " people in the region " + this.region.Name);
	}
}
