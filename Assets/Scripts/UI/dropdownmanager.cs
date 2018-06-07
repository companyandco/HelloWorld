using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownmanager : MonoBehaviour
{

	public bool ShowSymptome;
	public Dropdown dropdown;
	public Text TitleDescription;
	public Text TextDescription;
	
		    
	private Dictionary<string, string> description = MouseInformation.Description;
	private List<string> data = new List<string>();
	private List<string> dataReadable = new List<string>();
	
	void Start () {
		dropdown.onValueChanged.AddListener(delegate {
			DropdownValueChanged(dropdown);
		});
		
		if(ShowSymptome)
			data = MouseInformation.Transmition;
		else
			data = MouseInformation.Symptomes;

		foreach (string s in data)
			dataReadable.Add(MouseInformation.ToReadableString(s));
	    
		dropdown.ClearOptions();
		dropdown.AddOptions(data);
	}
	
	void DropdownValueChanged(Dropdown change)
	{
		int choice = change.value;
		string choicestr = data[choice];

		TitleDescription.text = dataReadable[choice];
		TextDescription.text = description[choicestr];
	}
}
