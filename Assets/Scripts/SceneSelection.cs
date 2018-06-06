using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
	public GameObject LoadingScreen;


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

	public void SendData ( bool hasWin )
	{
		char c = ( hasWin ) ? '1' : '2';
		Application.OpenURL ( "https://gotobreak.000webhostapp.com/game/insertmatch.php?p2=Computer&p1won=" + c );
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
	
	public GameObject MainMenuCanvas;
	public GameObject SettingsCanvas;

	public void GO_SETTINGS ()
	{
		this.MainMenuCanvas.SetActive ( false );
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