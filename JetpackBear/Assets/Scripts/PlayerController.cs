using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float flyForce;
	public float moveForce;

	private bool holdFly;
	private float axisHorizontal;
	private float accelerationX;

	private Rigidbody2D rb2d;

	private Animator burstAnim1;
	private Animator burstAnim2;

	private AudioSource audio;

	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		burstAnim1 = transform.Find("JetpackBurst1").GetComponent<Animator>();
		burstAnim2 = transform.Find("JetpackBurst2").GetComponent<Animator>();

		audio = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		if(holdFly)
			rb2d.AddForce(Vector2.up * flyForce);

		if(axisHorizontal != 0);
			rb2d.AddForce(new Vector2(axisHorizontal * moveForce, 0));	

		if(accelerationX != 0);
			rb2d.AddForce(new Vector2(accelerationX * (moveForce*2), 0));	
	}

	void Update () {
		holdFly = Input.GetButton("Fly") || Input.GetAxisRaw("Vertical") == 1 || Input.GetMouseButton(0);
		axisHorizontal = Input.GetAxis("Horizontal");
		accelerationX = Input.acceleration.x;

		burstAnim1.SetBool("on", holdFly);
		burstAnim2.SetBool("on", holdFly);

		Vector3 scale = transform.localScale;
		if(rb2d.velocity.normalized.x > 0)
			scale.x = 1;
		else if(rb2d.velocity.normalized.x < 0)
			scale.x = -1;

		transform.localScale = scale;

		transform.rotation = Quaternion.Euler(0, 0, -(rb2d.velocity.x*3));

		if(holdFly && !audio.isPlaying)
		{
			audio.Play();
		}
		else if(!holdFly && audio.isPlaying)
		{
			audio.Stop();
		}

		if(Input.GetButtonDown("Cancel"))
		{
			if(StageManager.Instance.IsPaused)
				StageManager.Instance.Resume();
			else
				StageManager.Instance.Pause();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!enabled) return;
			

		if(other.CompareTag("Hazard"))
		{
			SoundEffects.Instance.Play(SoundEffects.Instance.sfxHit);
			StageManager.Instance.Lose();
		}

		if(other.CompareTag("Pickup"))
		{
			SoundEffects.Instance.Play(SoundEffects.Instance.sfxPickup);
			StageManager.Instance.HiveCount++;
			Destroy(other.gameObject);
		}

		if(other.CompareTag("Finish"))
		{
			StageManager.Instance.Win();
		}
	}
}
