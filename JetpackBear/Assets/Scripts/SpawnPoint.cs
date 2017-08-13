using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public Transform bear;

	void Start () 
	{
		Respawn();
	}

	public void Respawn()
	{
		bear.position = transform.position;
	}
}
