using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour 
{
	private const float MIN_VOL = -80;
	private const float MAX_VOL = 0;

	private const string KEY_SFX = "sfxVolume";
	private const string KEY_MUSIC = "musicVolume";

	public AudioMixer masterMixer;

	public Toggle btnSfx;
	public Toggle btnMusic;

	void Start()
	{
		float vol = PlayerPrefs.GetFloat("sfxVolume", MAX_VOL);
		masterMixer.SetFloat("sfxVolume", vol);
		btnSfx.isOn = vol == MAX_VOL;

		vol = PlayerPrefs.GetFloat("musicVolume", MAX_VOL);
		masterMixer.SetFloat("musicVolume", vol);
		btnMusic.isOn = vol == MAX_VOL;
	}

	public void ToggleSound(bool toggle)
	{
		float vol = toggle ? MAX_VOL : MIN_VOL;
		masterMixer.SetFloat("sfxVolume", vol);
		PlayerPrefs.SetFloat("sfxVolume", vol);
	}

	public void ToggleMusic(bool toggle)
	{
		float vol = toggle ? MAX_VOL : MIN_VOL;

		masterMixer.SetFloat("musicVolume", vol);
		PlayerPrefs.SetFloat("musicVolume", vol);
	}
}
