using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	public int stageNumber;
	public string nextStage;
	public bool isFinal;

	private PlayerController bear;
	public UICollectedHives collectedHives;

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

		bear = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerController>();

		HiveCount = 0;
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
		HiveCount++;
		
		if(collectedHives != null)
		{
			collectedHives.SetHives(HiveCount);
			collectedHives.ShowForSeconds(3f);
		}
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
		int savedHiveCount = GameManager.Instance.GetSavedHiveCount(StageName);
		if(HiveCount > savedHiveCount)
			GameManager.Instance.SaveHiveCount(StageName, HiveCount);

		// Salva o progresso do jogo (o últime level concluído)
		if(stageNumber > GameManager.Instance.StageProgress)
			GameManager.Instance.StageProgress = stageNumber;

		yield return new WaitForSeconds(1.5f);

		GameManager.Instance.ShowCursor();

		if(OnWin != null)
			OnWin();
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