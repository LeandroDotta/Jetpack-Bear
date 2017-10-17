using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour 
{
	public static MusicManager Instance { get; private set; }

	public AudioClip defaultMusic;

	private AudioSource audioSource;

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void SetDefaultMusic()
	{
		if(!CheckCurrentClip(defaultMusic))
		{
			SetMusic(defaultMusic);
		}
	}

	public void SetMusic(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}

	public bool CheckCurrentClip(AudioClip clip)
	{
		print(audioSource.clip.name);
		return audioSource.clip.name == clip.name;
	}
}
