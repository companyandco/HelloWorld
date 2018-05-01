using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldLiveInfos : MonoBehaviour
{

	public GameObject TextObject;
	public GameObject MainControllerObject;

	private Text Text;
	private Main_Controller MainController;
	
	void Start ()
	{

		this.Text = this.TextObject.GetComponent <Text> ();
		this.MainController = this.MainControllerObject.GetComponent <Main_Controller> ();

	}
	
	void Update ()
	{

		string s = "";

        s += "Continent: " + PlayerGameManager.lastContinentClicked + "\n";
        var region = Main_Controller.GetRegionFromName(PlayerGameManager.lastContinentClicked);
        if (region != null)
            s += "Population infecté dans la région: " + region.infected + "\n";
        else
            s += "Population infecté dans la région: " + "aucun" + "\n";
        s += "Population mondiale saine: " + (this.MainController.totalSane + this.MainController.totalInfected) + "\n";
        s += "Population mondiale infectée: " + this.MainController.totalInfected + "\n";
        s += "Population mondiale morte: " + this.MainController.totalDead + "\n";
		s += "puissance: " + Main_Controller_off.powerO + "\n";
        this.Text.text = s;


	}
	
}
