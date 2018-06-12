using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchController : MonoBehaviour
{

	public SceneSelection SceneSelector;
	public GameObject SceneLoading;
	
	// Use this for initialization
	void Start ()
	{
		if (SceneSelector != null)
			SceneSelector.LaunchCustomScreen(SceneLoading);
		else
		{
			Instantiate(SceneLoading);
			SceneLoading.SetActive(true);
			Thread.Sleep(1000);
			SceneManager.LoadScene("main_menu");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
