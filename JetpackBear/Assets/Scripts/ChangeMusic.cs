using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour 
{
	public AudioClip music;

	void Start()
	{
		MusicManager.Instance.SetMusic(music);
	}
}
