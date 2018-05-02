using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLastContinentClicked : MonoBehaviour
{

	public GameObject go;
	private Text t;

	public static string continent;

	void Start ()
	{

		t = go.GetComponent<Text>();

	}
	
	void Update ()
	{
		if (continent != null || continent != "")
			t.text = continent;
		else
			t.text = "None";
	}
}
