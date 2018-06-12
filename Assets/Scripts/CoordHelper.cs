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
		/*
		SphericalCoord sc = new SphericalCoord ();
		var point = parentPos-targetPos;
		sc.Longitude = Mathf.Atan2 (point.x, point.z);
		sc.Latitude = Mathf.Atan2 (-point.y, new Vector2 (point.x, point.z).magnitude);
		sc.Latitude *= Mathf.Rad2Deg;
		sc.Longitude *= Mathf.Rad2Deg;
		return sc;
		*/
	}
	

	public static Vector2 SphericalToUV ( SphericalCoord sc )
	{
		Vector2 uv = new Vector2(
			(1 - (sc.Longitude / 360f)),
			((sc.Latitude / 180f))
		);
		
		return uv;
	}
	
}
