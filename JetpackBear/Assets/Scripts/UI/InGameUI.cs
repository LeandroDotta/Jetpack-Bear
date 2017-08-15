using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour 
{
	public RectTransform messagePanel;

	public Text textCenterMessage;
	public Button btnResume;
	public Button btnNextStage;

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;

	private Color32 transparentBlack = new Color32(0, 0, 0, 100);

	public void ShowPauseScreen()
	{
		ShowMessagePanel("PAUSED");
		btnResume.gameObject.SetActive(true);
	}

	public void ShowLoseScreen()
	{
		ShowMessagePanel("FAILED");
	}

	public void ShowWinScreen()
	{
		ShowMessagePanel("VICTORY");
		btnNextStage.gameObject.SetActive(true);
	}

	public void ShowMessagePanel(string message)
	{
		textCenterMessage.text = message;

		messagePanel.gameObject.SetActive(true);
		btnNextStage.gameObject.SetActive(false);
		btnResume.gameObject.SetActive(false);

		int hiveCount = StageManager.Instance.HiveCount;
		hive1.color = hiveCount >= 1 ? (Color32)Color.white : transparentBlack;
		hive2.color = hiveCount >= 2 ? (Color32)Color.white : transparentBlack;
		hive3.color = hiveCount == 3 ? (Color32)Color.white : transparentBlack;
	}

	public void HideMessagePanel()
	{
		messagePanel.gameObject.SetActive(false);
	}
}
