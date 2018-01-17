using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsLoader : MonoBehaviour {

	[Header("Prefabs")]
	[Header("Standalone")]
	public GameObject keyboardInstructions;
	public GameObject restartInstructions;
	[Header("Mobile")]
	public GameObject mobileInstructions;

	private void Start() 
	{
		#if UNITY_ANDROID || UNITY_IOS
		Instantiate(mobileInstructions);
		#else
		Instantiate(keyboardInstructions);
		Instantiate(restartInstructions);
		#endif

		Destroy(gameObject);
	}
}
