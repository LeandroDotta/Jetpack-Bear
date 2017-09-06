using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelection : MonoBehaviour 
{
	public Stage[] defaultStages;

	void Awake()
	{
		foreach(Stage stage in defaultStages)
		{
			if(!DataManager.HasStageInfo(stage))
			{
				StageInfo info = new StageInfo(){ unlocked = true };
				DataManager.SaveStageInfo(stage.key, info);
			}
		}
	}
}
