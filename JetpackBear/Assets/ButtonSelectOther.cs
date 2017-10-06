using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectOther : MonoBehaviour {

	public Selectable other;

	void Start () 
	{
		Button theButton = GetComponent<Button>();

		if(other != null)
		{
			theButton.onClick.AddListener(() => {
				other.Select();
			});
		}
	}
}
