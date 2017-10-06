using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler {

	public Stage stage;
	public StagePanel stagePanel;
	public int mapRegion;

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
		// Iniciar os componenetes e objetos filhos
		button = GetComponent<Button>();
		padlock = transform.Find("Padlock").GetComponent<Image>();
		iconVideo = transform.Find("IconVideo").GetComponent<Image>();
		
		numberText = transform.Find("Number").GetComponent<Text>();
		numberText.text = stage.number.ToString();

		// Carrega as informações do estágio (para saber se está bloqueado)
		stageInfo = DataManager.LoadStageInfo(stage.key);

		// Ativa ou desativa o botão, dependendo se ele está liberado ou não
		if(stageInfo.unlocked || stage.alwaysUnlocked)
		{
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

		if(stage.type == StageType.Normal) // Se for um estágio normal, exibe as informações das colmeias coletadas
		{
			if(stageInfo.hiveCount >= 1) hive1.color = Color.white;
			if(stageInfo.hiveCount >= 2) hive2.color = Color.white;
			if(stageInfo.hiveCount >= 3) hive3.color = Color.white;
		}
		else if (stage.type == StageType.Cinematic) // Se for um estágio de vídeo (cinematic), remove as informações das colmeias coletadas
		{
			hive1.gameObject.SetActive(false);
			hive2.gameObject.SetActive(false);
			hive3.gameObject.SetActive(false);
		}


		if(stage.key == WorldMap.Instance.SelectedStageKey)
		{
			button.Select();
			WorldMap.Instance.SetCurrentRegion(this.mapRegion);
		}
	}

	public void LoadStage()
	{
		DataManager.LastPlayedStage = stage.key;
		SceneManager.LoadScene(stage.scene);
	}

    public void OnSelect(BaseEventData eventData)
    {
		WorldMap.Instance.MoveCameraToRegion(mapRegion);

        if(stagePanel != null && button.interactable)
			stagePanel.Show(stage, stageInfo);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(stagePanel != null && button.interactable)
			stagePanel.Show(stage, stageInfo);
    }
}
