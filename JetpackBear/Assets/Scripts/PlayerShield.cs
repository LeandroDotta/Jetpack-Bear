using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {
	private Animator anim;

	public GameObject energyShield;

	[HideInInspector]
	public PowerUp powerUp;

	void Awake() 
	{
		anim = GetComponent<Animator>();
		powerUp = DataManager.GetPowerUpData(PowerUpId.Shield);
	}

	public void Show()
	{
		if(powerUp.Enabled)
			energyShield.SetActive(true);
	}

	public void Break()
	{
		powerUp.units = 0;
		DataManager.SetPowerUpData(powerUp);

		anim.SetTrigger("break");
	}

	private void Deactivate()
	{
		energyShield.SetActive(false);
	}
}
