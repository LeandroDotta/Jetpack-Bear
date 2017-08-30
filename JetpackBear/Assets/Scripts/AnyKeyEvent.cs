using UnityEngine;
using UnityEngine.Events;

public class AnyKeyEvent : MonoBehaviour {

	public UnityEvent onPressAnyKey;

	void Update()
	{
		if(Input.anyKeyDown || Input.GetMouseButtonDown(0))
			onPressAnyKey.Invoke();
	}
}
