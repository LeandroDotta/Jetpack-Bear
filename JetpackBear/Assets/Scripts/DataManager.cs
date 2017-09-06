using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
	public const string KEY_COINS = "coins";
	public const string KEY_UNLOCKED = "_unlocked";
	public const string KEY_HIVE_COUNT = "_hiveCount";

	public static int Coins 
	{ 
		get
		{
			return PlayerPrefs.GetInt(KEY_COINS, 0);
		} 
		set
		{
			PlayerPrefs.SetInt(KEY_COINS, value);
		} 
	}

	public static void SaveStageInfo(string stageKey, StageInfo info)
	{
		PlayerPrefs.SetInt(stageKey + KEY_UNLOCKED, info.unlocked ? 1 : 0);
		PlayerPrefs.SetInt(stageKey + KEY_HIVE_COUNT, info.hiveCount);
		
	}

	public static StageInfo LoadStageInfo(string stageKey)
	{
		StageInfo info = new StageInfo();
		info.unlocked = PlayerPrefs.GetInt(stageKey + KEY_UNLOCKED, 0) == 1;
		info.hiveCount = PlayerPrefs.GetInt(stageKey + KEY_HIVE_COUNT, 0);

		return info;
	}

	public static bool HasStageInfo(Stage stage)
	{
		return PlayerPrefs.HasKey(stage.key + KEY_UNLOCKED);
	}
}
