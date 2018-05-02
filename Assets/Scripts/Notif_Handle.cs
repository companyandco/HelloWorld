using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notif_Handle : MonoBehaviour {

	public void closeNotification(){
		GetComponent<Canvas> ().enabled = false;
	}
}
