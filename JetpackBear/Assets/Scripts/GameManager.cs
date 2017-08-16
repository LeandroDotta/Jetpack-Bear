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

	public void SaveHiveCount(string stageName, int count)
	{
		PlayerPrefs.SetInt(string.Format("Hives_{0}", stageName), count);
	}

	public int GetSavedHiveCount(string stageName)
	{
		return PlayerPrefs.GetInt(string.Format("Hives_{0}", stageName), 0);
	}
}
