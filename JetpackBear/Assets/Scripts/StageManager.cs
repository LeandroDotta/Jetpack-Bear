using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	public int stageNumber;
	public string nextStage;
	public bool isFinal;

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

	void Update()
	{
		
	}

	public void Pause()
	{
		IsPaused = true;
		GameManager.Instance.ShowCursor();

		if(stageUI != null)
			stageUI.ShowPauseScreen();

		SoundEffects.Instance.Play(SoundEffects.Instance.sfxPause);
	}

	public void Resume()
	{
		IsPaused = false;
		GameManager.Instance.HideCursor();

		if(stageUI != null)
			stageUI.HideMessagePanel();

		SoundEffects.Instance.Play(SoundEffects.Instance.sfxUISlide);
	}

	public IEnumerator LoseCoroutine()
	{
		yield return new WaitForSeconds(1.5f);

		GameManager.Instance.ShowCursor();

		if(stageUI != null)
			stageUI.ShowLoseScreen();
	}

	public IEnumerator WinCoroutine()
	{
		// Salva a quantidade de colmeias coletadas
		int savedHiveCount = GameManager.Instance.GetSavedHiveCount(StageName);
		if(HiveCount > savedHiveCount)
			GameManager.Instance.SaveHiveCount(StageName, HiveCount);

		// Salva o progresso do jogo (o últime level concluído)
		if(stageNumber > GameManager.Instance.StageProgress)
			GameManager.Instance.StageProgress = stageNumber;

		yield return new WaitForSeconds(1.5f);

		GameManager.Instance.ShowCursor();

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