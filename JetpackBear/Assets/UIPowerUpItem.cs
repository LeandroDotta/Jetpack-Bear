using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUpItem : MonoBehaviour {

	public PowerUpInfo info;
	public Image icon;
	public Text textUnits;

	private Animator anim;

	void Start () 
	{
		anim = GetComponent<Animator>();

		icon.sprite = info.icon;

		if(!info.isAccumulative)
			textUnits.gameObject.SetActive(false);

		anim.SetTrigger("bounce");
	}

	public void UpdateUnits(int units)
	{
		SetUnitsText(units);
		anim.SetTrigger("bounce");
	}

	public void SetUnitsText(int units)
	{
		textUnits.text = string.Format("x{0}", units);
	}
}
