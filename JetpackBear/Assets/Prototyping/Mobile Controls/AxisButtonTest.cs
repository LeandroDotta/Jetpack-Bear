using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisButtonTest : MonoBehaviour 
{
	public Text axisText;

	public MobileControlMode controlMode;

	private void Awake() 
	{
		SettingsManager.MobileControl = controlMode;
	}

	private void Update() {
		axisText.text = string.Format("Axis: {0}", MobileInputManager.GetAxis("Horizontal"));
	}
}
