using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour 
{
	private const float MIN_VOL = -80;
	private const float MAX_VOL = 0;

	public const string KEY_SFX = "sfxVolume";
	public const string KEY_MUSIC = "musicVolume";

	public AudioMixer masterMixer;

	public Toggle btnSfx;
	public Toggle btnMusic;

	public static float SfxVolume 
	{ 
		get
		{
			return PlayerPrefs.GetFloat(KEY_SFX, MAX_VOL);
		}
	}

	public static float MusicVolume 
	{ 
		get
		{
			return PlayerPrefs.GetFloat(KEY_MUSIC, MAX_VOL);
		}
	}


	void Start()
	{
		float vol = PlayerPrefs.GetFloat(KEY_SFX, MAX_VOL);
		masterMixer.SetFloat(KEY_SFX, vol);
		btnSfx.isOn = vol == MAX_VOL;

		vol = PlayerPrefs.GetFloat(KEY_MUSIC, MAX_VOL);
		masterMixer.SetFloat(KEY_MUSIC, vol);
		btnMusic.isOn = vol == MAX_VOL;
	}

	public void ToggleSound(bool toggle)
	{
		float vol = toggle ? MAX_VOL : MIN_VOL;
		masterMixer.SetFloat(KEY_SFX, vol);
		PlayerPrefs.SetFloat(KEY_SFX, vol);
	}

	public void ToggleMusic(bool toggle)
	{
		float vol = toggle ? MAX_VOL : MIN_VOL;

		masterMixer.SetFloat(KEY_MUSIC, vol);
		PlayerPrefs.SetFloat(KEY_MUSIC, vol);
	}
}
