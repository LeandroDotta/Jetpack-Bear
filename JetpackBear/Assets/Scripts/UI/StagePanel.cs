using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour {

	public Text textName;

	public Image hive1;
	public Image hive2;
	public Image hive3;
	
	private Animator anim;
	private RectTransform rect;
	private Vector2 startPos;

	private Color defaultColor;

	void Awake()
	{
		anim = GetComponent<Animator>();
		rect = GetComponent<RectTransform>();
		startPos = rect.anchoredPosition;

		defaultColor = hive1.color;
	}

	public void Show(Stage stage, StageInfo info)
	{
		rect.anchoredPosition = startPos;

		textName.text = stage.displayName;

		if(stage.type == StageType.Normal)
		{
			hive1.gameObject.SetActive(true);
			hive2.gameObject.SetActive(true);
			hive3.gameObject.SetActive(true);

			hive1.color = info.hiveCount >= 1 ? Color.white : defaultColor;
			hive2.color = info.hiveCount >= 2 ? Color.white : defaultColor;
			hive3.color = info.hiveCount >= 3 ? Color.white : defaultColor;

			textName.alignment = TextAnchor.MiddleLeft;
		}
		else if(stage.type == StageType.Cinematic)
		{
			hive1.gameObject.SetActive(false);
			hive2.gameObject.SetActive(false);
			hive3.gameObject.SetActive(false);

			textName.alignment = TextAnchor.MiddleCenter;
		}

		anim.Play("ShowStagePanel", -1, 0);
	}
}
