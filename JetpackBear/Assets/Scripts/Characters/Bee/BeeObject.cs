using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeeObject : MonoBehaviour {

	public float speed;

	protected Transform beeObj;
	
	protected virtual void Start () 
	{
		beeObj = transform.Find("BeeObj");
	}
}
