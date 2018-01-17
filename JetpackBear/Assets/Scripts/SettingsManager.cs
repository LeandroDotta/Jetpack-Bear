using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager 
{
	public const string KEY_MOBILE_CONTROL = "mobile_control";

	public static MobileControlMode MobileControl 
	{ 
		get
		{
			int value = PlayerPrefs.GetInt(KEY_MOBILE_CONTROL, (int)MobileControlMode.Tilt);
			return (MobileControlMode)value;
		}
		set
		{
			PlayerPrefs.SetInt(KEY_MOBILE_CONTROL, (int)value);

			if(OnChangeMobileControl != null)
				OnChangeMobileControl.Invoke(value);
		}
	}

	// EVENTS
	public delegate void ChangeMobileControlAction(MobileControlMode newMode);
	public static event ChangeMobileControlAction OnChangeMobileControl;
}
