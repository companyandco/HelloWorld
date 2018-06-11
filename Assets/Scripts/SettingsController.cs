using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
	public AudioMixer AudioMixer;
	public Dropdown ResolutionDropdown;
	public Toggle FullscreenToggle;
	
	public void SET_VOLUME (float volume)
	{
		this.AudioMixer.SetFloat ( "volume", volume );
	}

	public void SET_QUALITY ( int qualityIndex )
	{
		QualitySettings.SetQualityLevel ( qualityIndex, true);
	}

	public void SET_FULLSCREEN ( bool isFullscreen )
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SET_RESOLUTION ( int resolutionIndex )
	{
		Screen.SetResolution ( this.resolutions[resolutionIndex].width, this.resolutions[resolutionIndex].height, Screen.fullScreen );
	}
	
	public GameObject MainMenuCanvas;
	public GameObject SettingsCanvas;

	public void CLOSE_SETTINGS_MENU ()
	{
		this.MainMenuCanvas.SetActive ( true );
		this.SettingsCanvas.SetActive ( false );
	}
	
	private Resolution[] resolutions;
	
	void Start ()
	{
		if (FullscreenToggle != null)
		{
			if (Screen.fullScreen)
				FullscreenToggle.isOn = true;
			else
				FullscreenToggle.isOn = false;
		}
		else
			Debug.LogWarning("The toggle GameObject has not Been linked to the settingsController !");


		this.resolutions = Screen.resolutions;
		
		this.ResolutionDropdown.ClearOptions ();
		
		List <string> options = new List <string> ();

		int currentResolutionIndex = 0;
		for ( int i = 0; i < this.resolutions.Length; i++ )
		{
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add ( option );
			
			if ( resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height )
				currentResolutionIndex = i;
		}

		this.ResolutionDropdown.AddOptions ( options );
		this.ResolutionDropdown.value = currentResolutionIndex;
		this.ResolutionDropdown.RefreshShownValue ();

	}

}
