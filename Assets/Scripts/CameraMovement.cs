using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class CameraMovement : MonoBehaviour
{
	private bool isAlreadyClicking = false;

	public Transform target;

	private float distance = 10f;

	[Range ( 0f, 250f )] public float xSpeed = 50f;
	[Range ( 0f, 250f )] public float ySpeed = 50f;

	[Range ( -90f,  0f )] public float minAngle = -10;
	[Range (   0f, 90f )] public float maxAngle = 25f;

	[Range (  1f, 10f )] public float minDist = 3f;
	[Range ( 10f, 20f )] public float maxDist = 15f;

	private float x, y;
	private Rigidbody rb;

	void Start ()
	{
		var angles = this.transform.eulerAngles;
		this.x = angles.x;
		this.y = angles.y;

		this.rb = GetComponent <Rigidbody> ();
		if ( this.rb != null )
		{
			this.rb.freezeRotation = true;
		}
	}

	void Update ()
	{
		if ( !this.isAlreadyClicking && Input.GetButtonDown ( "Fire2" ) )
		{
			this.isAlreadyClicking = true;
		}
		if ( this.isAlreadyClicking && Input.GetButtonUp ( "Fire2" ) )
		{
			this.isAlreadyClicking = false;
		}
		if ( this.isAlreadyClicking && Input.GetButton ( "Fire2" ) /*|| Input.GetAxis ( "Mouse ScrollWheel" ) != 0*/ )
		{
			DragCamera ();
		}
	}

	private void DragCamera ()
	{
		if ( !this.isAlreadyClicking )
		{
			return;
		}

		if ( this.target )
		{
			this.x += Input.GetAxis ( "Mouse X" ) * this.xSpeed * this.distance * 0.02f;
			this.y -= Input.GetAxis ( "Mouse Y" ) * this.ySpeed * 0.02f;

			this.y = ClampAngle ( this.y, this.minAngle, this.maxAngle );
			
			Quaternion rotation = Quaternion.Euler ( this.y, this.x, 0f );

			this.distance = Mathf.Clamp ( 
				this.distance - Input.GetAxis ( "Mouse ScrollWheel" ) * 2f, 
				this.minDist, 
				this.maxDist 
			);

			RaycastHit hit;
			
			if ( Physics.Linecast ( this.target.position, this.transform.position, out hit ) )
			{
				this.distance -= hit.distance;
			}
			
			Vector3 negDistance = new Vector3 ( 0.0f, 0.0f, -this.distance );
			Vector3 position = rotation * negDistance + this.target.position;

			this.transform.rotation = rotation;
			this.transform.position = position;
		}
	}

	public static float ClampAngle ( float angle, float min, float max )
	{
		if ( angle < -360f )
			angle += 360f;
		if ( angle > 360f )
			angle -= 360f;
		return Mathf.Clamp ( angle, min, max );
	}
}