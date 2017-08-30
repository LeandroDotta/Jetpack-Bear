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

	void Start()
	{
		anim = GetComponent<Animator>();
		rect = GetComponent<RectTransform>();
		startPos = rect.anchoredPosition;

		defaultColor = hive1.color;
	}

	public void Show(string name, int hiveCount)
	{
		rect.anchoredPosition = startPos;

		textName.text = name;

		hive1.color = hiveCount >= 1 ? Color.white : defaultColor;
		hive2.color = hiveCount >= 2 ? Color.white : defaultColor;
		hive3.color = hiveCount >= 3 ? Color.white : defaultColor;

		anim.Play("ShowStagePanel", -1, 0);
	}
}
