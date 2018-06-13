using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    private Dropdown dropdownMusic;
	public AudioMixer AudioMixer;
	public GameObject music;
	public AudioSource[] musics;
	public float FadeSpeed = 1f;
	
	void Start ()
	{
		dropdownMusic = this.GetComponent<Dropdown>();
		musics = music.GetComponents<AudioSource>();
		dropdownMusic.onValueChanged.AddListener(delegate { ChangeMusic(); });
	}

	public void ChangeMusic()
	{
		int choice = dropdownMusic.value;
		if (choice < 0)
			choice = 0;
		if (choice > musics.Length)
			choice = musics.Length;
		if (choice <= 0)
		{
			foreach (AudioSource source in musics)
				if(source.isPlaying)
					StartCoroutine(FadeOutMusic(source));
		}
		else
		{
			for (int i = 0; i < musics.Length; i++)
			{
				if (i == choice - 1)
					StartCoroutine(FadeInMusic(musics[choice - 1]));
				else if (musics[i].isPlaying)
					musics[i].Stop();
			}
		}
	}

	IEnumerator FadeInMusic(AudioSource source)
	{
		float endPoint = 0f;
		AudioMixer.GetFloat("volume", out endPoint);
		source.Play();
		float beginpoint = -50f;
		while (beginpoint < endPoint - 1)
		{
			Debug.Log(beginpoint + " - " + endPoint);
			AudioMixer.SetFloat("volume", beginpoint);
			beginpoint += Time.deltaTime * 30 * FadeSpeed;
			yield return null;
		}
	}
	
	IEnumerator FadeOutMusic(AudioSource source)
	{
		float beginpoint = 0f;
		AudioMixer.GetFloat("volume", out beginpoint);
		while (beginpoint > -50f)
		{
			AudioMixer.SetFloat("volume", beginpoint);
			beginpoint -= Time.deltaTime * 30 * FadeSpeed;
			yield return null;
		}
		source.Stop();
	}
}