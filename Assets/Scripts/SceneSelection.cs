using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelection : MonoBehaviour
{
	//TODO: Don't hardcode these values.
	/*
	public Scene OnlineScene;
	public Scene OfflineScene;
	public Scene SettingsScene;
	public Scene AboutScene;
	*/

	
	public void GO_ONLINE ()
	{
		Debug.Log ( "GO_ONLINE" );
		SceneManager.LoadScene ( 1 );
		//StartCoroutine ( LoadAsynchronously ( 1 ) );
	}

	public void GO_OFFLINE_NEW_GAME ()
	{		
		SceneManager.LoadScene ("SinglePlayer");
	}
	
	public void GO_MAIN_MENU (){
		SceneManager.LoadScene ("main_menu");
	}
	
	/*
	public void GO_OFFLINE_LOAD_GAME ()
	{
		Debug.Log ( "GO_OFFLINE_LOAD_GAME" );
	}
	*/

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
		//StartCoroutine ( LoadAsynchronously ( 4 ) );
	}
	
	/*
	public GameObject loadingScreen;

	public Slider slider;
	
	IEnumerator LoadAsynchronously ( Scene scene )
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ( scene.buildIndex );

		this.loadingScreen.SetActive ( true );

		while ( !operation.isDone )
		{
			float progress = Mathf.Clamp01 ( operation.progress / 0.9f );
			
			slider.value = progress;

			yield return null;
		}
	}
	
	IEnumerator LoadAsynchronously ( int index )
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync ( index );

		this.loadingScreen.SetActive ( true );

		while ( !operation.isDone )
		{
			float progress = Mathf.Clamp01 ( operation.progress / 0.9f );
			
			slider.value = progress;

			yield return null;
		}
	}
	*/
}
