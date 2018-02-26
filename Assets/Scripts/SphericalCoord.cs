using System.Collections;
using System.Collections.Generic;
using NUnit.Compatibility;
using UnityEditor;
using UnityEngine;

public class SphericalCoord 
{
	// 0 is north pole, 180 is south pole
	public float Latitude
	{
		get { return this._Latitude; }
		set
		{
			this._Latitude = value;
			if ( this._Latitude > 270 )
			{
				this._Latitude -= 270;
			} else if ( this._Latitude > 0 )
			{
				this._Latitude += 90;
			}
		}
	}

	private float _Latitude;
	
	// 0 is left edge, 360 is right edge
	public float Longitude { get; set; }
	
	public override string ToString ()
	{
		return string.Format ( "[Latitude: {0}, Longitude: {1}]", Latitude, Longitude );
	}

}
