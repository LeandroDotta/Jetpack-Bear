using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

	public AudioClip sfxHit;
	public AudioClip sfxLose;
	public AudioClip sfxPause;
	public AudioClip sfxPickup;
	public AudioClip sfxWin;

	[Header("UI")]
	public AudioClip sfxUIActivation;
	public AudioClip sfxUINavigation;
	public AudioClip sfxUISlide;

	private AudioSource source;

	public static SoundEffects Instance { get; private set; }

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
		source = GetComponent<AudioSource>();
	}

	public void Play(AudioClip audio)
	{
		source.PlayOneShot(audio);
	}
}
