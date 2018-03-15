using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerLoading : MonoBehaviour
{

	public Slider slider;

	public Text text;
	
	void Update ()
	{
		if ( Time.time < 7f )
		{

			this.slider.value = Time.time / 7f;

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
