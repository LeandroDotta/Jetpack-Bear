using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour 
{
	public AudioClip music;

	void Start()
	{
		if(!MusicManager.Instance.CheckCurrentClip(music))
		{
			MusicManager.Instance.SetMusic(music);
		}
	}
}
