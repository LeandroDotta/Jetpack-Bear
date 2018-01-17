using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MobileInputManager
{
	private static Dictionary<string, float> axis = new Dictionary<string, float>{
		{ "Horizontal", 0 },
		{ "Vertical", 0 }
	};

	public static float GetAxis(string axisName)
	{
		return axis[axisName];
	}

	public static void SetAxis(string axisName, float value)
	{
		axis[axisName] = value;
	}
}
