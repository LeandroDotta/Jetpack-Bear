using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, ISelectHandler, IPointerClickHandler
{
	private Button button;

	void Start()
	{
		button = GetComponent<Button>();
	}

    public void OnSelect(BaseEventData eventData)
    {
        SoundEffects.Instance.Play(SoundEffects.Instance.sfxUINavigation);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundEffects.Instance.Play(SoundEffects.Instance.sfxUIActivation);
    }
}