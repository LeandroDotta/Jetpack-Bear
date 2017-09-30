using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Add Data.../Power Up")]
public class PowerUpInfo : ScriptableObject 
{
	public PowerUpId id;
	public string title;
	
	[TextArea]
	public string description;
	public int price;
	public Sprite icon;

	[Header("Behavior")]
	public bool isAccumulative;
	public int units;
	
}

public enum PowerUpId
{
	Shield = 1,
	Magnet = 2
}

public class PowerUp
{
	public PowerUpId id;
	public int units;

	public bool Enabled { 
		get
		{
			return units > 0;
		} 
	}

	public PowerUpInfo Info { get; set; }
}