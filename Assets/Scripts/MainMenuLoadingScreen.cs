using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuLoadingScreen : MonoBehaviour
{

	public GameObject Canvas;

	private VideoPlayer vp;

	private Image img;
	
	void Start ()
	{

		this.vp = this.Canvas.GetComponent <VideoPlayer> ();

		if ( this.vp == null )
			Debug.LogError ( "MainMenuLoadingScreen::Start::Couldn't find VideoPlayer Component." );

		this.img = this.Canvas.GetComponent <Image> ();

		if ( this.img == null )
		{
			Debug.LogError ( "MainMenuLoadingScreen::Start::Couldn't find Image Component." );
			return;
		}

		this.img.enabled = true;

	}
	
	void Update () {
		
		//TODO/FIXME: Causes flickering on the first frame.
		if ( this.vp.isPlaying )
		{
			this.img.enabled = false;
			Destroy ( this.gameObject );
		}
		
	}
}
