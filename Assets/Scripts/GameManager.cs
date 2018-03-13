using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public string lastContinentClicked;

	public void OnLastContinentClickedChange ()
	{
		if ( lastContinentClicked == null )
		{
			Debug.LogError ( "OnLastContinentClickedChange::lastContinentClicked is null." );
			return;
		}
		
		Debug.Log ( "OnLastContinentClickedChange: " + lastContinentClicked );
		
		//TODO: Actually do something with that call back...
	}

}
