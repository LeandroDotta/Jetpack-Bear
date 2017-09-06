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
	
	// public RectTransform messagePanel;
	// public Text textCenterMessage;
	// public Button btnResume;
	// public Button btnNextStage;

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;

	private Color32 transparentBlack = new Color32(0, 0, 0, 100);

	void Start()
	{
		StageManager.Instance.OnPause += ShowPauseScreen;
		StageManager.Instance.OnResume += HidePauseScreen;
		StageManager.Instance.OnLose += ShowLoseScreen;
		StageManager.Instance.OnWin += ShowWinScreen;

		stageName.text = StageManager.Instance.stage.displayName;
	}

	public void ShowPauseScreen()
	{
		pausePanel.gameObject.SetActive(true);
	}

	public void HidePauseScreen()
	{
		settingsPanel.gameObject.SetActive(false);
		pausePanel.gameObject.SetActive(false);
	}

	public void ShowLoseScreen()
	{
		losePanel.gameObject.SetActive(true);
	}

	public void ShowWinScreen()
	{
		int hiveCount = StageManager.Instance.CollectedHiveCount;
		hive1.color = hiveCount >= 1 ? (Color32)Color.white : transparentBlack;
		hive2.color = hiveCount >= 2 ? (Color32)Color.white : transparentBlack;
		hive3.color = hiveCount == 3 ? (Color32)Color.white : transparentBlack;

		winPanel.gameObject.SetActive(true);
	}

	// public void ShowMessagePanel(string message)
	// {
	// 	textCenterMessage.text = message;

	// 	messagePanel.gameObject.SetActive(true);
	// 	btnNextStage.gameObject.SetActive(false);
	// 	btnResume.gameObject.SetActive(false);
	// }

	// public void HideMessagePanel()
	// {
	// 	messagePanel.gameObject.SetActive(false);
	// }
}
