using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelection : MonoBehaviour 
{
	public Button[] stageButtons;

	void Awake()
	{
		for(int i = 1; i < stageButtons.Length; ++i)
		{
			Button btn = stageButtons[i];
			int progress = GameManager.Instance.StageProgress;

			if(progress >= i)
				btn.interactable = true;
			else
				btn.interactable = false;
				
		}
	}
}
