using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour 
{
	public GameObject trigger;

	private Animator anim;
	public int CoinCount { get; set; }

	[HideInInspector]
	public PowerUp powerUp;

	void Awake()
	{
		anim = GetComponent<Animator>();
		powerUp = DataManager.GetPowerUpData(PowerUpId.Magnet);
	}

	void Update()
	{
		if(CoinCount > 0)
			anim.SetBool("on", true);
		else
			anim.SetBool("on", false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Coin"))
		{
			Coin coin = other.gameObject.GetComponent<Coin>();
			coin.magnet = this;
			coin.FollowBear();
		}
	}

	public void Show()
	{
		if(powerUp.Enabled)
			trigger.SetActive(true);
	}

	public void Descrease()
	{
		powerUp.units--;
		DataManager.SetPowerUpData(powerUp);
	}
}
