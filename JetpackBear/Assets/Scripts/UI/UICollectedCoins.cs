using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectedCoins : MonoBehaviour 
{
	public Text textCoins;
	private Animator anim;

	private void Start() 
	{
		anim = GetComponent<Animator>();
		textCoins.text = "0";
	}

	private void OnEnable() 
	{
		if(StageManager.Instance != null)
		{
			StageManager.Instance.OnAddedCoin += SetCoins;
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
			StageManager.Instance.OnAddedCoin -= SetCoins;
			StageManager.Instance.OnLose -= Show;
			StageManager.Instance.OnPause -= Show;
			StageManager.Instance.OnResume -= Hide;
			StageManager.Instance.OnWin -= Hide;
		}
	}

	public void SetCoins(int coins)
	{
		textCoins.text = coins.ToString();
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
