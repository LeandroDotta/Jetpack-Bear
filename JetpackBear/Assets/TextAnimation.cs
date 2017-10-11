using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAnimation : MonoBehaviour {

	public float interval;
	public string[] textArray;

	private Text text;

	void Start () 
	{
		text = GetComponent<Text>();

		StartCoroutine("AnimationCoroutine");
	}
	
	private IEnumerator AnimationCoroutine()
	{
		int i = 0;

		while(true)
		{
			if(i >= textArray.Length)
				i = 0;

			string content = textArray[i];
			text.text = content;

			yield return new WaitForSeconds(interval);
			i++;
		}
	}
}
