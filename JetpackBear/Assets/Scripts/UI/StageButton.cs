using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler {

	public Stage stage;
	public StagePanel stagePanel;

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;
	
	private StageInfo stageInfo;

	private Button button;
	private Image padlock;
	private Image iconVideo;
	private Text numberText;


	void Start() 
	{
		button = GetComponent<Button>();
		padlock = transform.Find("Padlock").GetComponent<Image>();
		iconVideo = transform.Find("IconVideo").GetComponent<Image>();
		
		numberText = transform.Find("Number").GetComponent<Text>();
		numberText.text = stage.number.ToString();

		stageInfo = DataManager.LoadStageInfo(stage.key);

		if(stageInfo.unlocked)
		{
			print(stage.type);
			if(stage.type == StageType.Normal)
				numberText.gameObject.SetActive(true);
			else if (stage.type == StageType.Cinematic)
				iconVideo.gameObject.SetActive(true);
		}
		else
		{
			button.interactable = false;
			padlock.gameObject.SetActive(true);
		}		

		if(stageInfo.hiveCount >= 1) hive1.color = Color.white;
		if(stageInfo.hiveCount >= 2) hive2.color = Color.white;
		if(stageInfo.hiveCount >= 3) hive3.color = Color.white;
	}

	public void LoadStage()
	{
		SceneManager.LoadScene(stage.scene);
	}

    public void OnSelect(BaseEventData eventData)
    {
        if(stagePanel != null && button.interactable)
			stagePanel.Show(stage.displayName, stageInfo.hiveCount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(stagePanel != null && button.interactable)
			stagePanel.Show(stage.displayName, stageInfo.hiveCount);
    }
}
