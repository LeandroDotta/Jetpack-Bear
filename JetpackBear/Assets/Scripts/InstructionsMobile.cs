using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMobile : MonoBehaviour 
{
	public GameObject instructionsTilt;
	public GameObject instructionsButtons;

	private void OnEnable() 
	{
		SettingsManager.OnChangeMobileControl += OnChangeMobileControl;
	}

	private void OnDisable() 
	{
		SettingsManager.OnChangeMobileControl -= OnChangeMobileControl;
	}

	private void Start() 
	{
		OnChangeMobileControl(SettingsManager.MobileControl);
	}

	private void OnChangeMobileControl(MobileControlMode newMode)
	{
		if(newMode == MobileControlMode.Tilt)
		{
			instructionsTilt.SetActive(true);
			instructionsButtons.SetActive(false);
		}
		else
		{
			instructionsTilt.SetActive(false);
			instructionsButtons.SetActive(true);
		}
	}
}
