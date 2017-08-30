using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModalPanel : MonoBehaviour {

	public Selectable firstItem;
	public Selectable caller;

	void OnEnable()
	{
		// Selecionar o item diretamente pelo método OnEnable por algum motivo
		// Não ativa o estado correto do item selecionado. 
		// Para contornar esse problema, A co-rotina abaixo espera 1 frame para 
		// selecionar o item, garantido que ele fique em destaque na interface.
		if(firstItem != null)
			StartCoroutine(EnableFirstCoroutine());
	}

	void OnDisable()
	{
		if(caller != null)
			caller.Select();
		else if(EventSystem.current != null)
			EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
	}

	private IEnumerator EnableFirstCoroutine()
	{
		yield return new WaitForEndOfFrame();

		firstItem.Select();
	}	
}