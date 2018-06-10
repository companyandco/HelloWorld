using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject LoadingScreen;
	public float FadeSpeed = 0.75f;
	public float WaitingTime = 0.5f;

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
			Destroy(todisable);
	}
	
	// Use this for initialization
	void Start ()
	{
		LoadingScreen.SetActive(true);
		LoadingScreen.GetComponent<CanvasGroup>().alpha = 1.0f;
		StartCoroutine(FadeOut(LoadingScreen.GetComponent<CanvasGroup>(), LoadingScreen));
	}
}
