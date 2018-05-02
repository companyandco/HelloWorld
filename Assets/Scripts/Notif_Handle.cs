using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notif_Handle : MonoBehaviour {

	public static Notif_Handle Instance{ get; set;}
	public Canvas canvas;
	public Text Message;
	public Text Title;

	void Awake()
	{
		Instance = this;
	}

	public void closeNotification(){
		var notif = GetComponent<Canvas> ();
		notif.enabled = false;
	}

	public static void openNotification(string title, string message){
		
		Instance.Message.text=message;
		Instance.Title.text=title;
		Instance.canvas.enabled = true;


	}
}
