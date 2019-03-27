using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Input;

[CustomEditor(typeof(MenuManager))]
public class MenuManagerInspector : Editor
{
	private static bool inputDeviceFoldout = true;

	private MenuManager menuManager;

	void OnEnable()
	{
		menuManager = target as MenuManager;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(!EditorApplication.isPaused && !EditorApplication.isPlaying)
		{
			return;
		}
		if(inputDeviceFoldout = EditorGUILayout.Foldout(inputDeviceFoldout, "InputDevices"))
		{
			foreach(PlayerMenuState pms in menuManager.playerStatuses)
			{
				EditorGUILayout.LabelField(pms.device.displayName + ":" + pms.status.ToString() +":"+pms.selectionIndex);
			}
		}
	}
}
