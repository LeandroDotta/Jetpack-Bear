using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
	public enum HazardType
	{
		Spike,
		Bee,
		Flytrap
	}

	public HazardType type;

	void Start () 
	{
		if(gameObject.tag != "Hazard")
			gameObject.tag = "Hazard";
	}
}
