using UnityEngine;
using UnityEngine.Events;

public class TimelineEvent : MonoBehaviour {

	//public UnityAction action;
	public UnityEvent action;

	void OnEnable()
	{
		if(action != null)
			action.Invoke();
	}
}
