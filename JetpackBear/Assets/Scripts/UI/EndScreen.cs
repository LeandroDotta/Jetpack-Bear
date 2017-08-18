using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

	private bool keyEnabled;

	void Start () {
		keyEnabled = false;	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown && keyEnabled)
		{
			SceneManager.LoadScene("LevelSelection");
		}
	}

	public void SetKeyEnabled()
	{
		keyEnabled = true;
	}
}
