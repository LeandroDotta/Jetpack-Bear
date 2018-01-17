using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileControlsMenu : MonoBehaviour 
{
	public SelectionButton buttonTilt;
	public SelectionButton buttonBoostLeft;
	public SelectionButton buttonBoosRight;

	public GameObject panelTilt;
	public GameObject panelBoostLeft;
	public GameObject panelBoostRight;

	private void Start() 
	{
		buttonTilt.button.onClick.AddListener(OnClickTilt);
		buttonBoostLeft.button.onClick.AddListener(OnClickBoostLeft);
		buttonBoosRight.button.onClick.AddListener(OnClickBoostRight);

		MobileControlMode currentMode = SettingsManager.MobileControl;
		switch (currentMode)
		{
			case MobileControlMode.Tilt:
				buttonTilt.IsSelected = true;
				panelTilt.SetActive(true);
				break;
			
			case MobileControlMode.ButtonsBoostLeft:
				buttonBoostLeft.IsSelected = true;
				panelBoostLeft.SetActive(true);
				break;
			
			case MobileControlMode.ButtonsBoostRight:
				buttonBoosRight.IsSelected = true;
				panelBoostRight.SetActive(true);
				break;
		}
	}

	private void OnClickTilt()
	{
		SettingsManager.MobileControl = MobileControlMode.Tilt;

		buttonBoostLeft.IsSelected = false;
		buttonBoosRight.IsSelected = false;

		DeactivateGameObjects(panelBoostLeft, panelBoostRight);
		panelTilt.SetActive(true);
	}

	private void OnClickBoostLeft()
	{
		SettingsManager.MobileControl = MobileControlMode.ButtonsBoostLeft;

		buttonTilt.IsSelected = false;
		buttonBoosRight.IsSelected = false;	

		DeactivateGameObjects(panelTilt, panelBoostRight);
		panelBoostLeft.SetActive(true);
	}

	private void OnClickBoostRight()
	{
		SettingsManager.MobileControl = MobileControlMode.ButtonsBoostRight;

		buttonTilt.IsSelected = false;
		buttonBoostLeft.IsSelected = false;

		DeactivateGameObjects(panelTilt, panelBoostLeft);
		panelBoostRight.SetActive(true);
	}

	private void DeactivateGameObjects(params GameObject[] objects)
	{
		foreach(GameObject obj in objects)
		{
			obj.SetActive(false);
		}
	}
}
