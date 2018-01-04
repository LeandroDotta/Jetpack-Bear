using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class EndStagePanel : MonoBehaviour 
{
	public int collectedCoins;

	[Range(0, 3)]
	public int collectedHives;

	public Text textCoin;

	// private PlayableDirector timeline;

	public void RunAnimation()
	{

	}
}
