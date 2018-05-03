using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
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
		SceneManager.LoadScene ( "test3" );
	}

	public void GO_OFFLINE_NEW_GAME ()
	{
		SceneManager.LoadScene ( "SinglePlayer" );
	}

	public void GO_MAIN_MENU ()
	{
		Main_Controller.ResetVariables ();
		SceneManager.LoadScene ( "main_menu" );
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
}