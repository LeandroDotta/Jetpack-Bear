using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITests : MonoBehaviour {

	[Header("Velocity")]
	public Text velocityText;
	public Rigidbody2D theRigidbody;

	void Update () {
		velocityText.text = "Velocity: " + theRigidbody.velocity;
	}
}
