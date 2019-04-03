using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseEnemy), true)]
public class BaseEnemyInspector : Editor
{
	private static bool aggroFoldout = true;

	private BaseEnemy baseEnemy;

	void OnEnable()
	{
		baseEnemy = target as BaseEnemy;
	}
	

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(!EditorApplication.isPlaying)
		{
			return;
		}
		if(aggroFoldout = EditorGUILayout.Foldout(aggroFoldout, "Aggro"))
		{
			foreach(AggroData ad in baseEnemy.GetAggroDataArray())
			{
				EditorGUILayout.LabelField(ad.source.name + " " + ad.value);
			}
		}
	}
}
