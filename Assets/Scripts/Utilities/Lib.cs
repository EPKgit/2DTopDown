using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public static class Lib
{
	public static Color GetPlayerColorByIndex(int index)
	{
		switch(index)
		{
			case 1:
				return Color.blue;
			case 2:
				return Color.yellow;
			case 3:
				return new Color(107f / 255, 0, 111f / 255, 1);
			case 4:
				return new Color(222f / 255, 68f / 255 , 0, 1);
			default:
				return Color.black;
		}
	}

	public static T FindInHierarchy<T>(GameObject start)
	{
		GameObject top = start;
		while(top.transform.parent != null)
		{
			top = top.transform.parent.gameObject;
		}
		return Lib.RecursiveHelper<T>(top);
	}

	static T RecursiveHelper<T>(GameObject check)
	{
		if(DEBUGFLAGS.LIB) Debug.Log("checking " + check.name);
		T temp = check.GetComponent<T>();
		if(DEBUGFLAGS.LIB) Debug.Log("found " + temp);
		if(temp != null)
		{
			return temp;
		}
		foreach(Transform t in check.transform)
		{
			temp = Lib.RecursiveHelper<T>(t.gameObject);
			if(temp != null)
			{
				return temp;
			}
		}
		return default(T);
	}

	public static Vector3 GetMouseDirection(Mouse m, GameObject from)
	{
		Vector3 val = m.position.ReadValue();
		val.z = -CameraController.instance.transform.position.z;
		Vector3 worldSpacePosition = CameraController.instance.gameObject.GetComponent<Camera>().ScreenToWorldPoint(val);
		return worldSpacePosition - from.transform.position;
	}
}
