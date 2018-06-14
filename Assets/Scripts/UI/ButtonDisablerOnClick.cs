using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisablerOnClick : MonoBehaviour {
    public Button button;
	// Use this for initialization
	void Start () {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClicDisabler(); });
    }

    public void OnClicDisabler() {
        button.interactable = false;
    }
}
