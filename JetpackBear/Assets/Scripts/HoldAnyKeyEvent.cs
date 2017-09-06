using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldAnyKeyEvent : MonoBehaviour {

	public float holdTime;

	public Animator animator;
	public UnityEvent onHoldEnd;

	

	private float timer;

	void Update()
	{
		bool holding = Input.anyKey || Input.GetMouseButton(0);

		timer = holding ? timer + Time.deltaTime : 0;
		
		if(animator != null)
			animator.SetBool("holding", holding);

		animator.SetFloat("speed", 1 / holdTime);

		if(timer >= holdTime && onHoldEnd != null)
		{
			onHoldEnd.Invoke();
			timer = 0;
		}
	}
}

