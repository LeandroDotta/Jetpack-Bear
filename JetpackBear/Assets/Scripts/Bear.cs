using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Hazard"))
		{
			StageManager.Instance.Lose();
		}

		if(other.CompareTag("Pickup"))
		{
			StageManager.Instance.HiveCount++;
			Destroy(other.gameObject);
		}

		if(other.CompareTag("Finish"))
		{
			StageManager.Instance.Win();
		}
	}	
}
