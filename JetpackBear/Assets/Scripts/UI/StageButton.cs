using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : MonoBehaviour {

	[Header("Hives")]
	public Image hive1;
	public Image hive2;
	public Image hive3;

	public string stage;

	void Start () 
	{
		int hiveCount = GameManager.Instance.GetSavedHiveCount(stage);

		if(hiveCount >= 1) hive1.color = Color.white;
		if(hiveCount >= 2) hive2.color = Color.white;
		if(hiveCount >= 3) hive3.color = Color.white;
	}

	public void LoadStage()
	{
		SceneManager.LoadScene(stage);
	}
}
