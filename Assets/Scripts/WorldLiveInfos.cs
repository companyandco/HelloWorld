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
		
		s += "LastContinentClicked: " + PlayerGameManager.lastContinentClicked + "\n";
		s += "CurrentWorldPopulation: " + (this.MainController.totalSane + this.MainController.totalInfected) + "\n";
		s += "CurrentlyInfectedCount: " + this.MainController.totalInfected + "\n";
		s += "CurrentDeadPeopleCount: " + this.MainController.totalDead + "\n";
		
		this.Text.text = s;

	}
	
}
