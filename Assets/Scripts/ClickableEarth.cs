using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ClickableEarth : MonoBehaviour
{

	#region vars

	public Camera Camera;

	public GameObject Cube;

	#endregion

	void Start ()
	{


	}

	void Update ()
	{


	}

	private void OnMouseUp ()
	{
		Ray ray = this.Camera.ScreenPointToRay ( Input.mousePosition );
		
		RaycastHit hit;
		
		if ( Physics.Raycast ( ray, out hit ) )
		{
			Transform objectHit = hit.transform;

			Debug.Log ( "Object hit ! ObjectName: " + objectHit.name );

			Instantiate ( this.Cube, hit.point, Quaternion.identity );

			Debug.Log ( "Instantiated Cube at : " + hit.point );
		}
	}
}