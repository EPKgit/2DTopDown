﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseHealth))]
public class BaseHealthInspector : Editor
{
	private BaseHealth baseHealth;
	private StatBlock statBlock;
	

	void OnEnable()
	{
		baseHealth = target as BaseHealth;
		statBlock = baseHealth.gameObject.GetComponent<StatBlock>();
	}

	public override void OnInspectorGUI()
	{
		if(EditorApplication.isPlaying || EditorApplication.isPaused)
		{
			EditorGUILayout.LabelField(string.Format("{0}/{1}", baseHealth.GetCurrentHealth(), baseHealth.GetMaxHealth()));
			return;
		}
		if(statBlock == null || !statBlock.HasStat(StatName.Toughness))
		{
			base.OnInspectorGUI();
		}
		else
		{
			EditorGUILayout.LabelField("Max Health is being set by the StatBlock");
			EditorGUILayout.LabelField("Value: " + statBlock.GetValue(StatName.Toughness));
		}
	}
}
