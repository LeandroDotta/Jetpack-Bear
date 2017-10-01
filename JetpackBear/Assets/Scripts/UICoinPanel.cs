using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoinPanel : MonoBehaviour {

	public Text coinText;
	public Text greenText;
	public Text redText;

	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();

		SetCoinValue(DataManager.Coins);
	}

	public void AddCoins(int amount)
	{
		SetCoinValue(DataManager.Coins + amount);
		greenText.text = string.Format("+{0}", amount);
		anim.SetTrigger("add");
	}

	public void RemoveCoins(int amount)
	{
		SetCoinValue(DataManager.Coins - amount);
		redText.text = string.Format("-{0}", amount);
		anim.SetTrigger("remove");
	}

	public void SetCoinValue(int coins)
	{
		coinText.text = coins.ToString();
	}
}