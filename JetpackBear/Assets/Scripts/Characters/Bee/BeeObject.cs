using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeeObject : MonoBehaviour {

	public float speed;

	protected Transform beeObj;
	
	protected void Start () 
	{
		beeObj = transform.Find("BeeObj");
	}
}
