using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UICoinText : MonoBehaviour {
	private Text coinText;

	void Awake()
	{
		coinText = GetComponent<Text>();
	}

	void Start () 
	{
		coinText.text = DataManager.Coins.ToString();
	}

	void OnEnable()
	{
		DataManager.OnSetCoins += HandleOnSetCoins;
	}

	void OnDisable()
	{
		DataManager.OnSetCoins -= HandleOnSetCoins;
	}

	private void HandleOnSetCoins(int amount)
	{
		coinText.text = DataManager.Coins.ToString();
	}
}
