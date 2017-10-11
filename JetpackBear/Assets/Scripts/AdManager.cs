using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour 
{
	public const string AD_TYPE_COINS = "Coins";
	public const string AD_TYPE_SHIELD = "Shield";

	#if UNITY_EDITOR
		public const string AD_BANNER = "unused";
        public const string AD_EXTRA_COINS = "unused";
		public const string AD_FREE_SHIELD = "unused";
    #elif UNITY_ANDROID
		public const string AD_BANNER = "ca-app-pub-8727551359284183/9788124953";
        public const string AD_EXTRA_COINS = "ca-app-pub-8727551359284183/5506244955";
		public const string AD_FREE_SHIELD = "ca-app-pub-8727551359284183/8479715045";
    #elif UNITY_IOS
		public const string AD_BANNER = "";
        public const string AD_EXTRA_COINS = "";
		public const string AD_FREE_SHIELD = "";
    #else
		public const string AD_BANNER = "unexpected_platform";
        public const string AD_EXTRA_COINS = "unexpected_platform";
		public const string AD_FREE_SHIELD = "unexpected_platform";
    #endif

	public const string TEST_DEVICE_ID = "3CE41CDE8E7B7180";

	private GameObject adManagerUI;

	private RewardBasedVideoAd videoAd;
	private BannerView bannerView;
	private string currentAdUnit;
	private bool canceled;

	public static AdManager Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		videoAd = RewardBasedVideoAd.Instance;

		videoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
		videoAd.OnAdFailedToLoad += HandleAdFailedToLoad;
		videoAd.OnAdStarted += HandleAdStarted;
	}

	public void HideBanner()
	{
		#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		bannerView.Hide();
		#endif
	}

	public void LoadBanner()
	{
		#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		bannerView = new BannerView(AD_BANNER, AdSize.Banner, AdPosition.Bottom);
    	AdRequest request = new AdRequest.Builder()
			.AddTestDevice(TEST_DEVICE_ID)
			.Build();
    	bannerView.LoadAd(request);
		#endif
	}

	public void LoadAdVideo(string adUnit, GameObject adUI)
	{
		adManagerUI = adUI;
		canceled = false;

		// Armazena o id da propaganda atual (caso seja necessário recarregar).
		currentAdUnit = adUnit;

		// Se já houver um vídeo carregador, o exibe
		if(videoAd.IsLoaded())
		{
			videoAd.Show();
			return;
		}

		// Inicia o carregamento do vídeo
		videoAd.LoadAd(new AdRequest.Builder()
			.AddTestDevice(TEST_DEVICE_ID)
			.Build(), adUnit);

		adManagerUI.SendMessage("OnAdVideoLoading");

		StartCoroutine("ShowAdVideoCoroutine");
	}

	public void CancelAdVideo()
	{
		canceled = true;
	}

	private IEnumerator ShowAdVideoCoroutine()
	{
		
		while(!videoAd.IsLoaded())
		{
			yield return new WaitForEndOfFrame();
		}

		if(!canceled)
		{
			videoAd.Show();
		}
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;

		adManagerUI.SendMessage("OnAdRewarded", args, SendMessageOptions.DontRequireReceiver);

		if(type == AD_TYPE_COINS)
		{
			DataManager.Coins += Convert.ToInt32(amount);
		}
		else if(type == AD_TYPE_SHIELD)
		{
			PowerUp powerUp = new PowerUp();
			powerUp.id = PowerUpId.Shield;
			powerUp.units = 1;
			DataManager.SetPowerUpData(powerUp);
		}
	}

	private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		if(Application.internetReachability == NetworkReachability.NotReachable)
		{
			adManagerUI.SendMessage("OnAdFailedLoading", "Could not load the video. Check your connection or try later.", SendMessageOptions.DontRequireReceiver);
			return;
		}

		// Tenta recarregar o vídeo
		videoAd.LoadAd(new AdRequest.Builder()
			.AddTestDevice(TEST_DEVICE_ID)
			.Build(), currentAdUnit);
	}

	private void HandleAdStarted(object sender, EventArgs args)
	{
		adManagerUI.SendMessage("OnAdVideoStarted", SendMessageOptions.DontRequireReceiver);
	}
}