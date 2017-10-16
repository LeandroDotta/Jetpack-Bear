using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour 
{
	public float speed;
	public Vector2 direction;

	void Update()
	{
		transform.Translate(direction * speed * Time.deltaTime);
	}
}
