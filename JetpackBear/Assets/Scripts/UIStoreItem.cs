using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStoreItem : MonoBehaviour {
	public Text title;
	public Text description;
	public Text price;
	public Image icon;
	public Button buttonBuy;

	public CanvasGroup canvasGroup;

	private PowerUpInfo _info;
	public PowerUpInfo Info { 
		get
		{
			return _info;
		} 
		set
		{
			_info = value;
			UpdateInfo();
		}
	}

	public void UpdateInfo()
	{
		title.text = Localization.currentLanguageStrings[_info.title];
		description.text = Localization.currentLanguageStrings[_info.description];
		price.text = string.Format("{0}: {1}",Localization.currentLanguageStrings["price"], _info.price);
		icon.sprite = _info.icon;
	}

	public void UpdateState(PowerUp powerUp)
	{
		if(!_info.isAccumulative && powerUp.Enabled)
		{
			canvasGroup.interactable = false;
		}
	}
}
