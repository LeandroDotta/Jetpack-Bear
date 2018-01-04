using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class TextCounter : MonoBehaviour 
{
	private Text text;
	public int valueToSet;
	public float duration;

	private void Start() 
	{
		text = GetComponent<Text>();
	}

	public void Play()
	{
		StartCoroutine("CountCoroutine");
	}

	public void Stop()
	{
		StopCoroutine("CountCoroutine");
		text.text = valueToSet.ToString();
	}

	private IEnumerator CountCoroutine()
	{
		float timer = 0;
		
		int initialValue = 0;

		if (Int32.TryParse(text.text, out initialValue))
		{
			while (timer <= 1)
			{
				yield return new WaitForEndOfFrame();

				timer += Time.deltaTime / duration;

				int currentValue = (int)Mathf.Lerp(initialValue, valueToSet, timer);
				text.text = currentValue.ToString();
			}
		}
	}	
}
