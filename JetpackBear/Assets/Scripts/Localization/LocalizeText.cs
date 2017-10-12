using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizeText : MonoBehaviour {

	public string localizationKey;
	private Text text;

	void Start () 
	{
		if(!Localization.IsLanguagesStringsLoaded)
			Localization.LoadStrings(Application.systemLanguage);

		text = GetComponent<Text>();
		ApplyText();
	}

	public void ApplyText()
	{
		if(!text) return;

		if(!string.IsNullOrEmpty(localizationKey) && Localization.currentLanguageStrings.ContainsKey(localizationKey))
			text.text = Localization.currentLanguageStrings[localizationKey].Replace(@"\n", System.Environment.NewLine);
	}
}
