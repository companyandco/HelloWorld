using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class ClickableEarth : MonoBehaviour
{
	public Texture2D texture;

	private Dictionary <Color32, Continent> MapColorToContinent;

	void Start ()
	{
		this.MapColorToContinent = new Dictionary <Color32, Continent> ();
		PopulateDictionnary ();
	}

	void PopulateDictionnary ()
	{
		foreach (var continent in Main_Controller.Earth.regionlist)
		{
			foreach (var country in continent.countrylist) 
			{
				this.MapColorToContinent.Add (new Color32 ((byte)country.r,(byte)(country.g),(byte)(country.b),255), new Continent( country.Name + ", " + continent.Name));
			}
		}
		Color32 cyan = new Color32 ( 0, 255, 255, 255 );
		this.MapColorToContinent.Add ( cyan, new Continent ( "Oceans" ) );
	}

	//TODO: Split into different files

	private void OnMouseUp ()
	{
		Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );

		RaycastHit hit;

		if ( Physics.Raycast ( ray, out hit ) )
		{
			Transform objectHit = hit.transform;

			//Instantiate ( this.Cube, hit.point, Quaternion.identity );
			var point=hit.point;
			Debug.Log (point.y+", "+ (point.y-0.2f*point.y));
			if (point.y > 1.4 && point.y<1.93)
				point.y -= 0.1f * point.y;
			if (point.y > 1.93)
				point.y -= 0.18f * point.y;
			Vector3 p = new Vector3 ( point.x, point.y, point.z );


			SphericalCoord cs = CoordHelper.TransformToSphericalCoord ( p, this.transform.position );

			Vector2 uv = CoordHelper.SphericalToUV ( cs );

			ReadFromMap ( uv );
		}
	}

	public void ReadFromMap ( Vector2 uv )
	{
		Vector2 pixelToInspect = new Vector2 ( ( uv.x * this.texture.width ),  ( this.texture.height - ( uv.y * this.texture.height ) ));

		Color32 c = this.texture.GetPixel ( (int)pixelToInspect.x, (int)pixelToInspect.y );

		Debug.Log ( "Color clicked: " + c );

		Continent continentClicked;

		try
		{
			continentClicked = MapColorToContinent [c];
		} 
		catch ( Exception e )
		{
			//Debug.Log ( "ReadFromMap::Couldn't read map at these coordinates. Returning Oceans." );
			
			Console.WriteLine ( e );

			PlayerGameManager.lastContinentClicked = "Oceans";
			
			return;
		}

		//Debug.Log ( "Continent clicked: " + continentClicked.Name );
		
		PlayerGameManager.lastContinentClicked = continentClicked.Name;
		PlayerGameManager.OnLastContinentClickedChange ();
		
	}

}