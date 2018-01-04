using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour 
{
	public GameObject gameManager;
	public GameObject musicManager;
	public GameObject soundEffects;
	public GameObject adManager;
	public GameObject loadingScreen;

	private void Awake() 
	{
		if(GameManager.Instance == null)
			Instantiate(gameManager);

		if(MusicManager.Instance == null)
			Instantiate(musicManager);

		if(SoundEffects.Instance == null)
			Instantiate(soundEffects);

		if(AdManager.Instance == null)
			Instantiate(adManager);
		
		if(LoadingScreen.Instance == null)
			Instantiate(loadingScreen);

		if(Localization.currentLanguageStrings.Count == 0)
			Localization.LoadStrings();

		Destroy(this.gameObject);
	}
}
