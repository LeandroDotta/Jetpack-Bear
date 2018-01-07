using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedHives : MonoBehaviour 
{
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

	private void OnEnable() 
	{
		if(StageManager.Instance != null)
		{
			StageManager.Instance.OnAddedHive += SetHives;
			StageManager.Instance.OnLose += Show;
			StageManager.Instance.OnPause += Show;
			StageManager.Instance.OnResume += Hide;
			StageManager.Instance.OnWin += Hide;
		}
	}

	private void OnDisable() 
	{
		if(StageManager.Instance != null)
		{
			StageManager.Instance.OnAddedHive -= SetHives;
			StageManager.Instance.OnLose -= Show;
			StageManager.Instance.OnPause -= Show;
			StageManager.Instance.OnResume -= Hide;
			StageManager.Instance.OnWin -= Hide;
		}
	}

	public void SetHives(int hiveCount)
	{
		hive1.color = hiveCount >= 1 ? Color.white : defaultColor;
		hive2.color = hiveCount >= 2 ? Color.white : defaultColor;
		hive3.color = hiveCount >= 3 ? Color.white : defaultColor;

		ShowForSeconds(3f);
	}

	public void Show()
	{
		StopAllCoroutines();

		anim.SetBool("show", true);
	}

	public void Hide()
	{
		StopAllCoroutines();

		anim.SetBool("show", false);
	}

	public void ShowForSeconds(float seconds)
	{
		anim.SetBool("show", true);
		HideAfter(seconds);
	}

	public void HideAfter(float seconds)
	{
		StopCoroutine("HideCoroutine");
		StartCoroutine("HideCoroutine", seconds);
	}

	private IEnumerator HideCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);

		anim.SetBool("show", false);
	}
}
