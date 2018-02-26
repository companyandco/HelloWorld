using UnityEngine;
using System.Collections.Generic;

public class ClickableEarth : MonoBehaviour
{

	public Camera Camera;

	public GameObject Cube;

	public Texture2D texture;

	private Dictionary <Vector4, Continent> MapColorToContinent;

	private void Awake ()
	{
		this.MapColorToContinent = new Dictionary <Vector4, Continent> ();
		PopulateDictionnary ();
	}

	void PopulateDictionnary ()
	{
		Vector4 red = new Vector4 ( 1, 0, 0, 1 );
		this.MapColorToContinent.Add ( red, new Continent ( "NorthAmerica" ) );

		Vector4 magenta = new Vector4 ( 1, 0, 1, 1 );
		this.MapColorToContinent.Add ( magenta, new Continent ( "SouthAmerica" ) );

		//FIXME: Fucked up for Europe, gotta check if everything is aligned right.
		Vector4 green = new Vector4 ( 0, 1, 0, 1 );
		this.MapColorToContinent.Add ( green, new Continent ( "Europe" ) );

		Vector4 yellow = new Vector4 ( 1, 1, 0, 1 );
		this.MapColorToContinent.Add ( yellow, new Continent ( "Asia" ) );

		Vector4 white = Vector4.one;
		this.MapColorToContinent.Add ( white, new Continent ( "Africa" ) );

		Vector4 blue = new Vector4 ( 0, 0, 1, 1 );
		this.MapColorToContinent.Add ( blue, new Continent ( "Oceania" ) );

		Vector4 black = new Vector4 ( 0, 0, 0, 1 );
		this.MapColorToContinent.Add ( black, new Continent ( "Antarctic" ) );

		Vector4 cyan = new Vector4 ( 0, 1, 1, 1 );
		this.MapColorToContinent.Add ( cyan, new Continent ( "Oceans" ) );
	}

	//TODO: Split into different files

	private void OnMouseUp ()
	{
		Ray ray = this.Camera.ScreenPointToRay ( Input.mousePosition );

		RaycastHit hit;

		if ( Physics.Raycast ( ray, out hit ) )
		{
			Transform objectHit = hit.transform;

			//Instantiate ( this.Cube, hit.point, Quaternion.identity );

			Vector3 p = new Vector3 ( hit.point.x, hit.point.y, hit.point.z );

			SphericalCoord cs = CoordHelper.TransformToSphericalCoord ( p, this.transform.position );

			Vector2 uv = CoordHelper.SphericalToUV ( cs );

			ReadFromMap ( uv );
		}
	}

	public Continent ReadFromMap ( Vector2 uv )
	{
		Vector2 pixelToInspect = new Vector2 ( ( int ) ( uv.x * this.texture.width ), ( int ) ( this.texture.height - ( uv.y * this.texture.height ) ) );

		Color c = this.texture.GetPixel ( ( int ) pixelToInspect.x, ( int ) pixelToInspect.y );

		Debug.Log ( "Color clicked: " + c );

		Continent continentClicked = MapColorToContinent [c];

		Debug.Log ( "Continent clicked: " + continentClicked.Name );

		return continentClicked;
	}

}