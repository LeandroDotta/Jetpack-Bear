using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour {

	public Stage stage;

	void Start()
	{
		DataManager.LastPlayedStage = stage.key;

		MusicManager.Instance.SetMusic(stage.music);
	}
}
