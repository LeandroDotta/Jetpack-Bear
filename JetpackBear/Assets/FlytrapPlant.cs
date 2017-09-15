using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlytrapPlant : MonoBehaviour 
{
	public float delay;

	private bool attacking;
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
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
}
