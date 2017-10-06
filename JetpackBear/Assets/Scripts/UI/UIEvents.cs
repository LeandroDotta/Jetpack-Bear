using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEvents : MonoBehaviour 
{
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void LoadSceneAdditive(string name)
	{
		Scene scene = SceneManager.GetSceneByName(name);
		
		// Carrega a cena apenas se ela ainda não estiver carregada
		if(scene.buildIndex == -1)
		{
			SceneManager.LoadScene(name, LoadSceneMode.Additive);
		}
	}

	public void LoadLevelSelectionScene()
	{
		string lastPlayedStage = DataManager.LastPlayedStage;

		if(string.IsNullOrEmpty(lastPlayedStage))
			LoadScene("Prologue");
		else
			LoadScene("LevelSelection");
	}

	public void UnloadSceneAdditive(string name)
	{
		Scene scene = SceneManager.GetSceneByName(name);
		SceneManager.UnloadSceneAsync(scene);
	}

	public void Pause()
	{
		if(StageManager.Instance != null)
			StageManager.Instance.Pause();
	}

	public void Resume()
	{
		if(StageManager.Instance != null)
			StageManager.Instance.Resume();
	}

	public void RestartStage()
	{
		if(StageManager.Instance != null)
			StageManager.Instance.RestartStage();
	}

	public void NextStage()
	{
		if(StageManager.Instance != null)
			StageManager.Instance.NextStage();
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void ResetProgress()
	{
		GameManager.Instance.ResetProgress();
	}
}
