using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAbilities))]
public class PlayerAbilitiesInspector : Editor
{
	private static bool tickingAbilitiesFoldout = true;

	private PlayerAbilities playerAbilities;

	void OnEnable()
	{
		playerAbilities = target as PlayerAbilities;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(!EditorApplication.isPaused && !EditorApplication.isPlaying)
		{
			return;
		}
		if(tickingAbilitiesFoldout = EditorGUILayout.Foldout(tickingAbilitiesFoldout, "TickingAbilities"))
		{
			foreach(string s in playerAbilities.GetCurrentlyTickingAbilities())
			{
				EditorGUILayout.LabelField(s);
			}
		}
	}
}
