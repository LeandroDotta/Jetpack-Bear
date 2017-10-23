using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdManagerUI : MonoBehaviour 
{
	public GameObject dialogWatch;
	public GameObject dialogInfo;
	public GameObject loadingPanel;

	public Button btnWatch;
	public Button btnCancelLoading;

	public Text textInfo;

	public UICoinPanel coinsPanel;

	void Start()
	{
	}

	public void LoadAdVideoShield()
	{
		AdManager.Instance.LoadAdVideo(AdManager.AD_FREE_SHIELD, this.gameObject);
	}

	public void LoadAdVideoCoins()
	{
		AdManager.Instance.LoadAdVideo(AdManager.AD_EXTRA_COINS, this.gameObject);
	}

	public void CancelVideoLoading()
	{
		AdManager.Instance.CancelAdVideo();
	}

	private void OnAdRewarded(Reward reward)
	{
		string type = reward.Type;
		double amount = reward.Amount;
		
		if(coinsPanel != null)
		{
			coinsPanel.AddCoins((int)amount);
		}

		textInfo.text = Localization.currentLanguageStrings["reward_recieved"];
		dialogInfo.SetActive(true);
	}

	private void OnAdVideoLoading()
	{
		//loadingPanel.SetActive(true);
		LoadingScreen.Instance.Show(cancelAction: () => {
			AdManager.Instance.CancelAdVideo();
		});
	}

	private void OnAdVideoStarted()
	{
		// loadingPanel.SetActive(false);
		LoadingScreen.Instance.Hide();
	}

	private void OnAdFailedLoading(string message)
	{
		textInfo.text = message;
		dialogInfo.SetActive(true);

		// loadingPanel.SetActive(false);
		LoadingScreen.Instance.Hide();
	}
}
