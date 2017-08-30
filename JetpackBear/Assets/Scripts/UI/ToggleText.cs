using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour {

	public string textOn;
	public string textOff;

	public Text textTarget;

	private Toggle toggleButton;

	void Start () {

		toggleButton = GetComponent<Toggle>();

		toggleButton.graphic = null;
		toggleButton.onValueChanged.AddListener(ChangeText);

		ChangeText(toggleButton.isOn);
	}

	private void ChangeText(bool on)
	{
		textTarget.text = on ? textOn : textOff;
	}
}
