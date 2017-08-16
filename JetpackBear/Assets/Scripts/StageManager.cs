using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	public string nextStage;
	public PlayerController bear;
	public InGameUI stageUI;

	public static StageManager Instance { get; set; }
	public int HiveCount { get; set; }
	public string StageName 
	{ 
		get
		{
			return SceneManager.GetActiveScene().name;
		} 
	}

	public bool IsPaused 
	{ 
		get
		{
			return Time.timeScale == 0;
		}
		set
		{
			Time.timeScale = value ? 0 : 1;
		}
	}

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
	}

	void Start()
	{
		IsPaused = false;
		GameManager.Instance.HideCursor();

		HiveCount = 0;
	}

	public void Pause()
	{
		IsPaused = true;
		GameManager.Instance.ShowCursor();

		if(stageUI != null)
			stageUI.ShowPauseScreen();
	}

	public void Resume()
	{
		IsPaused = false;
		GameManager.Instance.HideCursor();

		if(stageUI != null)
			stageUI.HideMessagePanel();
	}

	public void Lose()
	{
		bear.enabled = false;
		GameManager.Instance.ShowCursor();

		if(stageUI != null)
			stageUI.ShowLoseScreen();
	}

	public void Win()
	{
		bear.enabled = false;
		GameManager.Instance.ShowCursor();

		int savedHiveCount = GameManager.Instance.GetSavedHiveCount(StageName);
		if(HiveCount > savedHiveCount)
			GameManager.Instance.SaveHiveCount(StageName, HiveCount);

		if(stageUI != null)
			stageUI.ShowWinScreen();
	}

	public void NextStage()
	{
		SceneManager.LoadScene(nextStage);
	}

	public void RestartStage()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}