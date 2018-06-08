using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownmanager : MonoBehaviour
{

	public bool ShowSymptome;
	public Dropdown dropdown;
	public Button button;
	public Text TitleDescription;
	public Text TextDescription;
	
		    
	private Dictionary<string, string> description = MouseInformation.Description;
	private List<string> data = new List<string>();
	private List<string> dataReadable = new List<string>();
	
	void Start () {
		dropdown.onValueChanged.AddListener(delegate {
			DropdownValueChanged(dropdown);
		});
		button.onClick.AddListener(TaskOnClick);

		
		if(ShowSymptome)
			data = MouseInformation.Symptomes;
		else
			data = MouseInformation.Transmition;

		foreach (string s in data)
			dataReadable.Add(MouseInformation.ToReadableString(s));
	    
		dropdown.ClearOptions();
		dropdown.AddOptions(dataReadable);
	}
	
	void DropdownValueChanged(Dropdown change)
	{
		int choice = change.value;
		string choicestr = data[choice];

		TitleDescription.text = dataReadable[choice];
		TextDescription.text = description[choicestr];
	}
	
	void TaskOnClick()
	{
		this.gameObject.SetActive(false);
		if(ShowSymptome)
			Main_Controller_def.ResearchSympButton(data[dropdown.value]);
		else 
			Main_Controller_def.ResearchTransButton(data[dropdown.value]);
	}
}
