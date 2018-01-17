using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMobileControls : MonoBehaviour 
{
	public RectTransform boostPanel;
	public RectTransform directionPanel;

	void Start () 
	{
		SetControls();
	}

	public void SetControls()
	{
		MobileControlMode controlMode = SettingsManager.MobileControl;
		SetControls(controlMode);
	}
	public void SetControls(MobileControlMode newMode)
	{
		Vector2 leftPivot = new Vector2(0, 0.5f);
		Vector2 rightPivot = new Vector2(1, 0.5f);

		Vector2 leftAnchorMin = new Vector2(0, 0);
		Vector2 rightAnchorMin = new Vector2(1, 0);

		Vector2 leftAnchorMax = new Vector2(0, 1);
		Vector2 rightAnchorMax = new Vector2(1, 1);

		if(newMode == MobileControlMode.ButtonsBoostRight)
		{
			boostPanel.pivot = rightPivot;
			boostPanel.anchorMin = rightAnchorMin;
			boostPanel.anchorMax = rightAnchorMax;

			directionPanel.pivot = leftPivot;
			directionPanel.anchorMin = leftAnchorMin;
			directionPanel.anchorMax = leftAnchorMax;
		}
		else
		{
			boostPanel.pivot = leftPivot;
			boostPanel.anchorMin = leftAnchorMin;
			boostPanel.anchorMax = leftAnchorMax;

			directionPanel.pivot = rightPivot;
			directionPanel.anchorMin = rightAnchorMin;
			directionPanel.anchorMax = rightAnchorMax;
		}
	}
}
