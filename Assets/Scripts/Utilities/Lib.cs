using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Lib
{
	// public static T FindInHierarchy<T>(GameObject start) where T : Component
	// {
	// 	GameObject top = start;
	// 	while(top.transform.parent != null)
	// 	{
	// 		top = top.transform.parent.gameObject;
	// 	}
	// 	return Lib.RecursiveHelper<T>(top);
	// }

	// public static T RecursiveHelper<T>(GameObject check) where T : Component
	// {
	// 	T temp = (T)check.GetComponent(typeof(T));
	// 	if(temp != null)
	// 	{
	// 		return temp;
	// 	}
	// 	foreach(Transform t in check.transform)
	// 	{
	// 		temp = Lib.RecursiveHelper<T>(t.gameObject);
	// 		if(temp != null)
	// 		{
	// 			return temp;
	// 		}
	// 	}
	// 	return null;
	// }
	public static T FindInHierarchy<T>(GameObject start)
	{
		GameObject top = start;
		while(top.transform.parent != null)
		{
			top = top.transform.parent.gameObject;
		}
		return Lib.RecursiveHelper<T>(top);
	}

	public static T RecursiveHelper<T>(GameObject check)
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
}
