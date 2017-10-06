using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour 
{
	public Transform[] regions;

	private int currentRegion = 0;

	private float transitionTime = 0.2f;

	public string SelectedStageKey { get; private set; }
	public static WorldMap Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

		SelectedStageKey = DataManager.LastPlayedStage;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			MoveCameraToRegion(0);

		if(Input.GetKeyDown(KeyCode.Alpha2))
			MoveCameraToRegion(1);

		if(Input.GetKeyDown(KeyCode.Alpha3))
			MoveCameraToRegion(2);

		if(Input.GetKeyDown(KeyCode.Alpha4))
			MoveCameraToRegion(3);
	}

	public void SetCurrentRegion(int regionIndex)
	{
		if(regionIndex >= 0 && regionIndex < regions.Length && regionIndex != currentRegion)
		{
			Camera.main.transform.position = new Vector3(regions[regionIndex].position.x, regions[regionIndex].position.y, Camera.main.transform.position.z);
		}
	}

	public void MoveCameraToRegion(int regionIndex)
	{
		if(regionIndex >= 0 && regionIndex < regions.Length && regionIndex != currentRegion)
		{
			currentRegion = regionIndex;
			StartCoroutine(MoveCameraCoroutine(regionIndex, transitionTime));
		}
	}

	private IEnumerator MoveCameraCoroutine(int regionIndex, float duration)
	{	
		Camera cam = Camera.main;

		Vector2 startPosition = cam.transform.position;
		Vector2 endPosition = regions[regionIndex].position;

		float counter = 0;

		while(true)
		{
			float t = counter / duration;
			Vector3 pos = Vector2.Lerp(startPosition, endPosition, t);
			pos.z = cam.transform.position.z;
			cam.transform.position = pos;

			if(counter >= duration)
				break;

			yield return new WaitForEndOfFrame();
			counter += Time.deltaTime;
		}
	}
}
