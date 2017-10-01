using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlytrapPlant : MonoBehaviour 
{
	public float delay;

	private bool attacking;
	private Animator anim;
	private AudioSource audioSource;
	private Transform inside;


	void Start()
	{
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		inside = transform.Find("Body").Find("Inside");
	}

	private void AttackStart()
	{
		if(!attacking)
		{
			attacking = true;
			StartCoroutine("AttackCoroutine");
		}
	}

	private void AttackEnd()
	{
		attacking = false;
	}

	public void SwallowBear(PlayerController bear)
	{
		StartCoroutine(SwallowCoroutine(bear));
	}

	private IEnumerator SwallowCoroutine(PlayerController bear)
	{
		anim.SetBool("swallowed", true);
		inside.gameObject.SetActive(true);

		float duration = 0.1f;
		float timer = 0;

		Vector2 startPos = bear.transform.position;

		do
		{
			yield return new WaitForEndOfFrame();
			timer += Time.deltaTime;

			Vector2 newPosition = Vector2.Lerp(startPos, inside.position, timer / duration);
			bear.transform.position = newPosition;
		}while(timer <= duration);

		bear.gameObject.SetActive(false);
	}

	private IEnumerator AttackCoroutine()
	{
		anim.SetBool("shaking", true);
		yield return new WaitForSeconds(delay);		
		anim.SetBool("shaking", false);

		anim.SetTrigger("attack");
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			AttackStart();
		}
	}

	public void PlayAudio()
	{
		audioSource.Play();
	}	
}
