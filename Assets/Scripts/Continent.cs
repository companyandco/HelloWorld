using System.Collections.Generic;
using UnityEngine;

public class Continent : MonoBehaviour
{

	#region variables

	public bool isSelected;

	public string ContinentName;

	public long ContinentPopulation;

	public float ContinentPopulationDensity;

	public float ContinentLifeExpectancy;

	public float ContinentGDP;

	public float ContinentAverageHumidity;

	public float ContinentAverageTemperature;

	#endregion

	#region constructor

	public void SetValues ( List <string> cparams )
	{
		this.ContinentPopulation = long.Parse ( cparams [0] );
		this.ContinentPopulationDensity = float.Parse ( cparams [1] );
		this.ContinentLifeExpectancy = float.Parse ( cparams [2] );
		this.ContinentGDP = float.Parse ( cparams [3] );
		this.ContinentAverageHumidity = int.Parse ( cparams [4] );
		this.ContinentAverageTemperature = int.Parse ( cparams [5] );
	}
	
	#endregion

	#region methods

	private void Update ()
	{
		if ( Input.GetMouseButtonDown ( 0 ) )
		{
			Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

			RaycastHit hit;

			if ( Physics.Raycast ( ray, out hit ) )
			{
				string continentHitName = hit.transform.gameObject.transform.parent.name;

				this.isSelected = continentHitName == this.ContinentName;

				if ( this.isSelected )
					Debug.Log ( this.ContinentName + " selected." );
			}
		}
	}

	private void OnGUI ()
	{
		if ( this.isSelected )
		{
			string infos = this.ContinentName +
			               "\nPopulation: " + this.ContinentPopulation +
			               "\nDensity: " + this.ContinentPopulationDensity +
			               "\nLifeExpectancy: " + this.ContinentLifeExpectancy +
			               "\nGDP: " + this.ContinentGDP +
			               "\nHumidity: " + this.ContinentAverageHumidity +
			               "\nTemperature: " + this.ContinentAverageTemperature;
			GUI.Label ( new Rect ( 10, 10, 10000000, 10000000 ), infos );
		}
	}
	
	#endregion

}