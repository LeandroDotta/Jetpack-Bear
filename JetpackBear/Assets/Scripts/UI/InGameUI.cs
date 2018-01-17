using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour 
{
	public RectTransform pausePanel;
	public RectTransform settingsPanel;
	public RectTransform losePanel;
	public RectTransform winPanel;
	
	public Text stageName;

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;

	[Header("Mobile")]
	public GameObject mobileControlsPrefab;
	private UIMobileControls mobileControls;

	private Color32 transparentBlack = new Color32(0, 0, 0, 100);

	private void OnEnable() 
	{
		#if UNITY_ANDROID || UNITY_IOS
		SettingsManager.OnChangeMobileControl += OnChangeMobileControl;
		#endif
	}

	private void OnDisable() 
	{
		#if UNITY_ANDROID || UNITY_IOS
		SettingsManager.OnChangeMobileControl -= OnChangeMobileControl;
		#endif
	}

	void Start()
	{
		StageManager.Instance.OnPause += ShowPauseScreen;
		StageManager.Instance.OnResume += HidePauseScreen;
		StageManager.Instance.OnLose += ShowLoseScreen;
		StageManager.Instance.OnWin += ShowWinScreen;

		stageName.text = Localization.currentLanguageStrings[StageManager.Instance.stage.displayName];

		#if UNITY_ANDROID || UNITY_IOS
		StartMobileControls();
		ShowMobileControls();
		#endif
	}

	public void ShowPauseScreen()
	{
		pausePanel.gameObject.SetActive(true);
		HideMobileControls();
	}

	public void HidePauseScreen()
	{
		settingsPanel.gameObject.SetActive(false);
		pausePanel.gameObject.SetActive(false);

		ShowMobileControls();
	}

	public void ShowLoseScreen()
	{
		losePanel.gameObject.SetActive(true);
		HideMobileControls();
	}

	public void ShowWinScreen()
	{
		int hiveCount = StageManager.Instance.CollectedHiveCount;
		hive1.color = hiveCount >= 1 ? (Color32)Color.white : transparentBlack;
		hive2.color = hiveCount >= 2 ? (Color32)Color.white : transparentBlack;
		hive3.color = hiveCount == 3 ? (Color32)Color.white : transparentBlack;

		winPanel.gameObject.SetActive(true);
		HideMobileControls();
	}

	private void StartMobileControls()
	{
		if(mobileControlsPrefab == null || SettingsManager.MobileControl == MobileControlMode.Tilt)
			return;

		GameObject clone = Instantiate(mobileControlsPrefab);
		clone.transform.SetParent(this.transform, false);
		clone.SetActive(false);

		mobileControls = clone.GetComponent<UIMobileControls>();
	}

	private void ShowMobileControls()
	{
		if(mobileControls != null && SettingsManager.MobileControl != MobileControlMode.Tilt)
			mobileControls.gameObject.SetActive(true);
	}

	private void HideMobileControls()
	{
		if(mobileControls != null)
			mobileControls.gameObject.SetActive(false);
	}

	private void OnChangeMobileControl(MobileControlMode newMode)
	{
		if(newMode != MobileControlMode.Tilt)
		{
			if(mobileControls == null)
				StartMobileControls();

			mobileControls.SetControls(newMode);
		}
	}
}
