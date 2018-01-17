using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITests : MonoBehaviour {

	[Header("Velocity")]
	public Text velocityText;
	public Rigidbody2D theRigidbody;
	

	public Text accelerometer;
	public Text axisText;

	void Update () {
		velocityText.text = "Velocity: " + theRigidbody.velocity;
		accelerometer.text = "Accelerometer: " + Input.acceleration;

		if(axisText != null)
			axisText.text = string.Format("Axis: ({0:0.00}, {1:0.00})", Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}
}
