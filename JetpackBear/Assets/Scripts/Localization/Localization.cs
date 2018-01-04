using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Localization : MonoBehaviour 
{
	public const string RESOURCES_PATH = "Localization/";
	public static Dictionary<string, string> currentLanguageStrings = new Dictionary<string, string>();
	public static bool IsLanguagesStringsLoaded { get; private set; }

	void Awake()
	{
		LoadStrings(Application.systemLanguage);
	}

	public static void LoadStrings()
	{
		LoadStrings(Application.systemLanguage);
	}

	public static void LoadStrings(SystemLanguage language)
	{
		Debug.Log("Carregando Strings para: " + language.ToString());

		TextAsset text;
		text = Resources.Load(RESOURCES_PATH + language.ToString(), typeof(TextAsset)) as TextAsset;

		if(text == null)
		{
			Debug.Log("Arquivo não encontrado! Carregando arquivo padrão...");
			text = Resources.Load(RESOURCES_PATH + SystemLanguage.English.ToString(), typeof(TextAsset)) as TextAsset;
		}

		Debug.Log("Arquivo carregado! preenchendo dicionário...");
		string[] lines = text.text.Split(new string[]{"\r\n", "\n\r", "\n"}, StringSplitOptions.RemoveEmptyEntries);
		currentLanguageStrings.Clear();
		for(int i = 0; i < lines.Length; i++)
		{
			string line = lines[i];
			string[] pairs = line.Split(new char[]{'='}, 2);

			currentLanguageStrings.Add(pairs[0].Trim(), pairs[1].Trim());
		}

		IsLanguagesStringsLoaded = true;

		Debug.Log("Strings Carregadas para " + language.ToString() + "(Total de " + currentLanguageStrings.Count + " palavras");
	}
}
