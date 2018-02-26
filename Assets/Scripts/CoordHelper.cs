using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordHelper 
{

	public static SphericalCoord TransformToSphericalCoord ( Vector3 targetPos, Vector3 parentPos )
	{
		SphericalCoord sc = new SphericalCoord ();

		Vector3 dirToTarget = targetPos - parentPos;

		Quaternion quatToTarget = Quaternion.LookRotation ( dirToTarget );
		
		sc.Latitude = quatToTarget.eulerAngles.x;
		sc.Longitude = quatToTarget.eulerAngles.y;
		
		return sc;
	}

	public static Vector2 SphericalToUV ( SphericalCoord sc )
	{
		Vector2 uv = new Vector2(
			1 - (sc.Longitude / 360f),
			sc.Latitude / 180f
		);
		
		return uv;
	}
	
}
