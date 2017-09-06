using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedHives : MonoBehaviour 
{
	private bool isShowing;
	private Image hive1;
	private Image hive2;
	private Image hive3;

	private Color defaultColor;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();

		hive1 = transform.Find("Hive1").GetComponent<Image>();
		hive2 = transform.Find("Hive2").GetComponent<Image>();
		hive3 = transform.Find("Hive3").GetComponent<Image>();

		defaultColor = hive1.color;
	}

	public void SetHives(int hiveCount)
	{
		hive1.color = hiveCount >= 1 ? Color.white : defaultColor;
		hive2.color = hiveCount >= 2 ? Color.white : defaultColor;
		hive3.color = hiveCount >= 3 ? Color.white : defaultColor;
	}

	public void Show()
	{
		anim.SetBool("show", true);
	}

	public void Hide()
	{
		anim.SetBool("show", false);
	}

	public void ShowForSeconds(float seconds)
	{
		anim.SetBool("show", true);
		HideAfter(seconds);
	}

	public void HideAfter(float seconds)
	{
		StartCoroutine(HideCoroutine(seconds));
	}

	private IEnumerator HideCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);

		anim.SetBool("show", false);
	}
}
