using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlantProjectile : MonoBehaviour 
{
	public float speed;
	public float maxRange;
	public Vector2 direction;

	private bool moving = true;

	private Vector2 startPosition;
	private Animator anim;
	private Rigidbody2D rb2d;

	void FixedUpdate()
	{
		if(moving)
		{
			rb2d.velocity = direction * speed;

			if((rb2d.position.x - startPosition.x) >= maxRange)
				DestroySelf();
		}
	}

	void Start()
	{
		direction = direction.normalized;
		startPosition = transform.position;

		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		moving = false;
		anim.SetTrigger("hit");
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
