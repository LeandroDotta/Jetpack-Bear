using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float flyForce;
	public float moveForce;

	private bool holdFly;
	private float axisHorizontal;

	private Rigidbody2D rb2d;

	private Animator burstAnim1;
	private Animator burstAnim2;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		burstAnim1 = transform.Find("JetpackBurst1").GetComponent<Animator>();
		burstAnim2 = transform.Find("JetpackBurst2").GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if(holdFly)
			rb2d.AddForce(Vector2.up * flyForce);

		rb2d.AddForce(new Vector2(axisHorizontal * moveForce, 0));	
	}

	void Update () {
		holdFly = Input.GetButton("Fly") || Input.GetAxisRaw("Vertical") == 1;
		axisHorizontal = Input.GetAxis("Horizontal");

		burstAnim1.SetBool("on", holdFly);
		burstAnim2.SetBool("on", holdFly);

		Vector3 scale = transform.localScale;
		if(rb2d.velocity.normalized.x > 0)
			scale.x = 1;
		else if(rb2d.velocity.normalized.x < 0)
			scale.x = -1;

		transform.localScale = scale;

		transform.rotation = Quaternion.Euler(0, 0, -(rb2d.velocity.x*3));

		if(Input.GetButtonDown("Cancel"))
		{
			if(StageManager.Instance.IsPaused)
				StageManager.Instance.Resume();
			else
				StageManager.Instance.Pause();
		}
	}
}
