using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour 
{
	private const float MIN_VOL = -80;
	private const float MAX_VOL = 0;

	public AudioMixer masterMixer;

	public void ToggleSound(bool toggle)
	{
		masterMixer.SetFloat("sfxVolume", toggle ? MAX_VOL : MIN_VOL);
	}

	public void ToggleMusic(bool toggle)
	{
		masterMixer.SetFloat("musicVolume", toggle ? MAX_VOL : MIN_VOL);
	}
}
