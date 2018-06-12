using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject LoadingScreen;
	public float FadeSpeed = 1f;
	public GameObject PlayButtons;
	public GameObject SettingsObject;
	public GameObject AboutButtons;
	
	private float WaitingTime = 0;

	IEnumerator FadeOut(CanvasGroup obj, GameObject todisable = null)
	{
		yield return new WaitForSeconds(WaitingTime);
		
		float curTime = 1f;
		while (curTime >= 0)
		{
			obj.alpha = curTime;
			curTime -= Time.deltaTime * FadeSpeed;
			yield return null;
		}
		if (todisable != null && todisable.activeInHierarchy)
			todisable.SetActive(false);
	}
	
	IEnumerator FadeIn(CanvasGroup obj, GameObject toactive = null)
	{
		yield return new WaitForSeconds(WaitingTime);

		float curTime = 0f;
		if (toactive != null && !toactive.activeInHierarchy)
			toactive.SetActive(true);
		while (curTime <= 1)
		{
			obj.alpha = curTime;
			curTime += Time.deltaTime * FadeSpeed;
			yield return null;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		LoadingScreen.SetActive(true);
		LoadingScreen.GetComponent<CanvasGroup>().alpha = 1.0f;
		
		WaitingTime = 0.5f;
		float backup = FadeSpeed;
		FadeSpeed = 1f;
		StartCoroutine(FadeOut(LoadingScreen.GetComponent<CanvasGroup>(), LoadingScreen));
		WaitingTime = 0f;
		FadeSpeed = backup;
	}

	public void FadeIn(GameObject obj)
	{
		StartCoroutine(FadeIn(obj.GetComponent<CanvasGroup>(), obj));
	}
	
	public void FadeOut(GameObject obj)
	{
		StartCoroutine(FadeOut(obj.GetComponent<CanvasGroup>(), obj));
	}

	public void FadeAllOut()
	{
		if (PlayButtons.activeInHierarchy)
			FadeOut(PlayButtons);
		if (AboutButtons.activeInHierarchy)
			FadeOut(AboutButtons);
		if (SettingsObject.activeInHierarchy)
			FadeOut(SettingsObject);
	}

	public void PlayBouttonClicked()
	{
		if (!PlayButtons.activeInHierarchy)
			FadeIn(PlayButtons);
		if (AboutButtons.activeInHierarchy)
			FadeOut(AboutButtons);
		if (SettingsObject.activeInHierarchy)
			FadeOut(SettingsObject);
	}

	public void SettingsButtonClicked()
	{
		if (!SettingsObject.activeInHierarchy)
			FadeIn(SettingsObject);
		if (PlayButtons.activeInHierarchy)
			FadeOut(PlayButtons);
		if (AboutButtons.activeInHierarchy)
			FadeOut(AboutButtons);
	}

	public void AboutButtonClicked()
	{
		if (!AboutButtons.activeInHierarchy)
			FadeIn(AboutButtons);
		if (PlayButtons.activeInHierarchy)
			FadeOut(PlayButtons);
		if (SettingsObject.activeInHierarchy)
			FadeOut(SettingsObject);
	}

}
