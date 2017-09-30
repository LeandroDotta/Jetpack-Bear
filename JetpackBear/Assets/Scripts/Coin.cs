using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
	public float speed;

	private bool following;

	[HideInInspector]
	public Magnet magnet;

	void OnDestroy()
	{
		if(magnet != null)	
			magnet.CoinCount--;
	}

	public void FollowBear()
	{
		if(!following)
		{
			magnet.CoinCount++;
			StartCoroutine(FollowCoroutine());
		}
	}

	private IEnumerator FollowCoroutine()
	{
		following = true;

		while(true)
		{
			Vector2 direction = (StageManager.Instance.bear.transform.position - transform.position).normalized;
			transform.Translate(direction * speed * Time.deltaTime);

			yield return new WaitForEndOfFrame();
		}
	}
}
