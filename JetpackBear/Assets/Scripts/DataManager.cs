using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager 
{
	public const string KEY_COINS = "coins";
	public const string KEY_UNLOCKED = "_unlocked";
	public const string KEY_HIVE_COUNT = "_hiveCount";
	public const string KEY_POWER_UP = "powerup_";
	public const string KEY_POWERUP_SHIELD = "powerup_shield";
	public const string KEY_POWERUP_MAGNET = "powerup_magnet";
	public const string KEY_LAST_PLAYED_STAGE = "last_played_stage";

	public delegate void SetCoinsAction(int amount);
	public static event SetCoinsAction OnSetCoins;

	public static int Coins 
	{ 
		get
		{
			return PlayerPrefs.GetInt(KEY_COINS, 0);
		} 
		set
		{
			PlayerPrefs.SetInt(KEY_COINS, value);

			if(OnSetCoins != null)
			{
				OnSetCoins.Invoke(value);
			}
		} 
	}

	/// <summary>
	/// 	Chave do último estágio jogado pelo usuário.
	/// </summary>
	public static string LastPlayedStage 
	{ 
		get
		{
			return PlayerPrefs.GetString(KEY_LAST_PLAYED_STAGE, null);
		} 
		set
		{
			PlayerPrefs.SetString(KEY_LAST_PLAYED_STAGE, value);
		} 
	}

	public static PowerUp GetPowerUpData(PowerUpId id)
	{
		PowerUp powerUp = new PowerUp();
		powerUp.id = id;
		powerUp.units = PlayerPrefs.GetInt(KEY_POWER_UP + id, 0);

		return powerUp;
	}

	public static void SetPowerUpData(PowerUp powerUp)
	{
		PlayerPrefs.SetInt(KEY_POWER_UP + powerUp.id, powerUp.units);
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
