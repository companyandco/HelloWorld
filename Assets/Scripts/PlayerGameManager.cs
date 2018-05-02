using UnityEngine;

public static class PlayerGameManager
{

	public static string lastContinentClicked;

	public static void OnLastContinentClickedChange ()
	{
		if ( lastContinentClicked == null )
		{
			Debug.LogError ( "OnLastContinentClickedChange::lastContinentClicked is null." );
			return;
		}
		
		Debug.Log ( "OnLastContinentClickedChange: " + lastContinentClicked );

		ShowLastContinentClicked.continent = lastContinentClicked;
	}

}
