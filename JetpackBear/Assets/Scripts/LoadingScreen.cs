using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour 
{
	public static LoadingScreen Instance { get; private set; }
	private GameObject canvas;
	private Button cancelButton;

	void Awake()
	{
		canvas = transform.Find("Canvas").gameObject;
		cancelButton = canvas.transform.Find("BtnCancel").GetComponent<Button>();
		cancelButton.onClick.AddListener(HideCanvas);

		if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);		

        DontDestroyOnLoad(gameObject);
	}

	public void Show(UnityAction cancelAction = null)
	{
		if(canvas != null)
			canvas.SetActive(true);

		if(cancelAction == null)
		{
			cancelButton.gameObject.SetActive(false);
		}
		else
		{
			cancelButton.gameObject.SetActive(true);
			cancelButton.onClick.AddListener(cancelAction);
		}
	}

	public void Hide()
	{
		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener(HideCanvas);

		HideCanvas();		
	}

	private void HideCanvas()
	{
		if(canvas != null)
			canvas.SetActive(false);
	}
}
