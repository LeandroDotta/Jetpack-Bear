using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler {

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;

	public string stage;
	
	public Sprite disabledSprite;
	public StagePanel stagePanel;

	private Button button;
	private Image padlock;
	private Text numberText;


	void Start() 
	{
		button = GetComponent<Button>();
		padlock = transform.Find("Padlock").GetComponent<Image>();
		numberText = transform.Find("Number").GetComponent<Text>();

		if(!button.IsInteractable()){
			button.image.overrideSprite = disabledSprite;
			padlock.gameObject.SetActive(true);
			numberText.gameObject.SetActive(false);
		}

		int hiveCount = GameManager.Instance.GetSavedHiveCount(stage);

		if(hiveCount >= 1) hive1.color = Color.white;
		if(hiveCount >= 2) hive2.color = Color.white;
		if(hiveCount >= 3) hive3.color = Color.white;
	}

	public void LoadStage()
	{
		SceneManager.LoadScene(stage);
	}

    public void OnSelect(BaseEventData eventData)
    {
        if(stagePanel != null)
			stagePanel.Show(stage, GameManager.Instance.GetSavedHiveCount(stage));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(stagePanel != null)
			stagePanel.Show(stage, GameManager.Instance.GetSavedHiveCount(stage));
    }
}
