using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	public void ShowCursor()
	{
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.visible = false;
	}

	public void ResetProgress()
	{
		float sfxVol = AudioControl.SfxVolume;
		float musicVol = AudioControl.MusicVolume;
		
		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetFloat(AudioControl.KEY_SFX, sfxVol);
		PlayerPrefs.SetFloat(AudioControl.KEY_MUSIC, musicVol);
	}
}
