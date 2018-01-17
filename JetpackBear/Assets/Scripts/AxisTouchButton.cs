using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class AxisTouchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string axis = "Horizontal";
	public float axisValue = 1;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public Sprite pressedSprite;
    public Sprite disabledSprite;

    private Image image;
    private Color enabledColor;
    private Color disabledColor;

    private void Start() 
    {
        image = GetComponent<Image>();

        enabledColor = image.color;
        enabledColor.a = 0.7f;
        
        disabledColor = image.color;
        disabledColor.a = 0.2f;

        image.color = disabledColor;
    }

	public void OnPointerEnter(PointerEventData eventData)
    {
        MobileInputManager.SetAxis(axis, axisValue);
        image.color = enabledColor;
        image.sprite = normalSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MobileInputManager.SetAxis(axis, 0);
        image.color = disabledColor;
        image.sprite = pressedSprite;
    }
}
