using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerLoading : MonoBehaviour
{

	public Slider slider;

	public Text text;

	private float startingTime;
	private float progress;
	
	void Start ()
	{
		this.startingTime = Time.time;
		this.progress = 0f;
	}
	
	void Update ()
	{
		if ( Time.time < this.startingTime + 8f )
		{

			this.progress += Time.deltaTime / 8f;

			this.slider.value = this.progress;

			int n = ( int ) ( 3f * ( Mathf.Sin ( 5f * Time.time ) + 1 ) / 2f ) + 1;

			string s = "Loading";

			for ( int i = 0; i < n; i++ )
			{
				s += ".";
			}

			this.text.text = s;

		} else
			Destroy ( this.transform.gameObject );

	}
	
}
