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
		btnCancelLoading.onClick.AddListener(() =>{
			AdManager.Instance.CancelAdVideo();
		});
	}

	public void LoadAdVideoShield()
	{
		AdManager.Instance.LoadAdVideo(AdManager.AD_FREE_SHIELD, this.gameObject);
	}

	public void LoadAdVideoCoins()
	{
		AdManager.Instance.LoadAdVideo(AdManager.AD_EXTRA_COINS, this.gameObject);
	}

	private void OnAdRewarded(Reward reward)
	{
		string type = reward.Type;
		double amount = reward.Amount;
		
		if(coinsPanel != null)
		{
			coinsPanel.AddCoins((int)amount);
		}

		textInfo.text = "Reward Recieved!";
		dialogInfo.SetActive(true);
	}

	private void OnAdVideoLoading()
	{
		loadingPanel.SetActive(true);
	}

	private void OnAdVideoStarted()
	{
		loadingPanel.SetActive(false);
	}

	private void OnAdFailedLoading(string message)
	{
		textInfo.text = message;
		dialogInfo.SetActive(true);

		loadingPanel.SetActive(false);
	}
}
