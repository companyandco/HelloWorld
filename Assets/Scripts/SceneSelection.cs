using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
	public GameObject LoadingScreen;
	public float FadeSpeed = 1f;
	
	private bool finishedFading = false;

	public void LaunchCustomScreen(GameObject screen)
	{
		StartCoroutine(FadeIn(screen.GetComponent<CanvasGroup>(), screen));
		StartCoroutine(LoadNewSceneCorutine("main_menu"));
	}


	public void LoadNewScreen(string scene)
	{
		Instantiate(LoadingScreen);
		LoadingScreen.SetActive(true);
		StartCoroutine(LoadNewSceneCorutine(scene));
	}
	
	public void LoadNewScreenImmediate(string scene)
	{
		Instantiate(LoadingScreen);
		LoadingScreen.SetActive(true);
		StartCoroutine(LoadNewSceneCorutineImmediate(scene));
	}

	
	IEnumerator LoadNewSceneCorutine(string str)
	{
		//temps d'attente minimum pour pas faire des transitions epileptiques
		yield return new WaitForSeconds(1);
		Debug.Log(str);
		
		//charge la scene de maniere asynchrone
		AsyncOperation async = SceneManager.LoadSceneAsync(str);

		//tant que la scene a pas fini de charger on attend
		while (!async.isDone)
		{
			yield return null;
		}
	}
	
	IEnumerator LoadNewSceneCorutineImmediate(string str)
	{
		//charge la scene de maniere asynchrone
		AsyncOperation async = SceneManager.LoadSceneAsync(str);
		
		while (!async.isDone)
		{
			yield return null;
		}
	}
	
	IEnumerator FadeIn(CanvasGroup obj, GameObject toactive = null)
	{
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
	
	IEnumerator FadeOut(CanvasGroup obj, GameObject todisable = null)
	{
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

	public void SendData ()
	{
		string opponent = Main_Controller.OpponentName;
		char c = ( Main_Controller.HasWon ) ? '0' : '1';
		Application.OpenURL ( "https://gotobreak.000webhostapp.com/game/insertmatch.php?p2=" + opponent + "&p1won=" + c );
	}

	public void OpenSite ( string page )
	{
		Application.OpenURL ( "https://gotobreak.000webhostapp.com/" + page + ".php" );
	}

	public void GO_ONLINE ()
	{
		Debug.Log ( "GO_ONLINE" );
		LoadNewScreen( "test3" );
		//SceneManager.LoadScene ( "test3" );
	}

	public void GO_OFFLINE_NEW_GAME ()
	{
		LoadNewScreen( "SinglePlayer" );
		//SceneManager.LoadScene ( "SinglePlayer" );
	}

	public void GO_MAIN_MENU ()
	{
		Main_Controller.ResetVariables ();
		LoadNewScreen( "main_menu" );
		//SceneManager.LoadScene ( "main_menu" );
	}
	
	public GameObject SettingsCanvas;

	public void GO_SETTINGS ()
	{
		this.SettingsCanvas.SetActive ( true );
	}

	public void GO_ABOUT ()
	{
		Debug.Log ( "GO_ABOUT" );
	}

	public void QUIT ()
	{
		Application.Quit ();
	}
}