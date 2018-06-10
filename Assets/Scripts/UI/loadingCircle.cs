using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingCircle : MonoBehaviour {

	private RectTransform rectComponent;
	private float rotateSpeed = 200f;
	public float rotateSpeedFactor;
	
	// Use this for initialization
	void Start () {
		rectComponent = GetComponent<RectTransform>();
		if (rotateSpeedFactor == null)
			rotateSpeedFactor = 1;
	}
	
	// Update is called once per frame
	void Update () {
		rectComponent.Rotate(0f, 0f, -1 * rotateSpeedFactor * rotateSpeed * Time.deltaTime);
	}
}
