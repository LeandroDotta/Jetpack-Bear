using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineHives : MonoBehaviour {

	private PlayableDirector timeline;



	void Start () 
	{
		timeline = GetComponent<PlayableDirector>();
	}

	private void Update() 
	{
		print(timeline.time);
		if(Input.anyKeyDown)
			timeline.time = timeline.time + 0.3f;
	}
}
