using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
	public Stage stage;
	

	public UICollectedHives collectedHives;
	
	private StageInfo stageInfo;

	[HideInInspector]
	public PlayerController bear;
	
	
	public static StageManager Instance { get; set; }

	public int CollectedCoins { get; set; }
	public int CollectedHiveCount { get; set; }

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

	// Events
	public delegate void PauseAction();
	public event PauseAction OnPause;

	public delegate void ResumeAction();
	public event ResumeAction OnResume;

	public delegate void LoseAction();
	public event LoseAction OnLose;

	public delegate void WinAction();
	public event WinAction OnWin;

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

		stageInfo = DataManager.LoadStageInfo(stage.key);
		bear = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();
	}

	void Update()
	{
		if(Input.GetButtonDown("Restart"))
			RestartStage();
	}

	public void Pause()
	{
		IsPaused = true;
		GameManager.Instance.ShowCursor();

		if(OnPause != null)
			OnPause();
	

		if(collectedHives != null)
			collectedHives.Show();

		SoundEffects.Instance.Play(SoundEffects.Instance.sfxPause);
	}

	public void Resume()
	{
		IsPaused = false;
		GameManager.Instance.HideCursor();

		if(OnResume != null)
			OnResume();

		if(collectedHives != null)
			collectedHives.HideAfter(3f);

		SoundEffects.Instance.Play(SoundEffects.Instance.sfxUISlide);
	}

	void OnDestroy()
	{
		IsPaused = false;
	}

	public void AddHive()
	{
		CollectedHiveCount++;
		
		if(collectedHives != null)
		{
			collectedHives.SetHives(CollectedHiveCount);
			collectedHives.ShowForSeconds(3f);
		}
	}

	public void AddCoin()
	{
		CollectedCoins++;
	}

	public void Lose()
	{
		SoundEffects.Instance.Play(SoundEffects.Instance.sfxLose);
		StartCoroutine(LoseCoroutine());
	}

	public IEnumerator LoseCoroutine()
	{
		yield return new WaitForSeconds(1.5f);

		GameManager.Instance.ShowCursor();

		if(OnLose != null)
			OnLose();

		if(collectedHives != null)
			collectedHives.Show();
	}

	public IEnumerator WinCoroutine()
	{
		// Salva a quantidade de colmeias coletadas
		if(CollectedHiveCount > stageInfo.hiveCount)
		{		
			stageInfo.hiveCount = CollectedHiveCount;
			DataManager.SaveStageInfo(stage.key, stageInfo);
		}
		
		// Libera o próximo level
		StageInfo nextStageInfo = DataManager.LoadStageInfo(stage.nextStage.key);
		if(!nextStageInfo.unlocked)
		{
			nextStageInfo.unlocked = true;
			DataManager.SaveStageInfo(stage.nextStage.key, nextStageInfo);
		}

		// Adiciona as moedas coletadas no saldo
		DataManager.Coins += CollectedCoins;

		if(bear.magnetController.powerUp.units > 0)
			bear.magnetController.Descrease();

		yield return new WaitForSeconds(1.5f);

		GameManager.Instance.ShowCursor();

		if(OnWin != null)
			OnWin();
	}

	public void NextStage()
	{
		DataManager.LastPlayedStage = stage.nextStage.key;
		SceneManager.LoadScene(stage.nextStage.scene);
	}

	public void RestartStage()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}