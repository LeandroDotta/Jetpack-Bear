using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectionButton : MonoBehaviour
{
	public Sprite selectedSprite;

	[HideInInspector]
	public Button button;
	private Sprite normalSprite;

	private bool _selected;
	public bool IsSelected 
	{ 
		get
		{
			return _selected;
		} 
		set
		{
			_selected = value;
			button.image.sprite = value ? selectedSprite : normalSprite;
		} 
	}

	private void Awake() 
	{
		button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);

		normalSprite = button.image.sprite;
	}

	private void OnClick()
	{
		IsSelected = true;
	}
}
