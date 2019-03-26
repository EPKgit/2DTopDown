using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BaseHealth))]
public class BaseHealthInspector : Editor
{
    public static bool debugFoldout = true;

	private BaseHealth baseHealth;
	private StatBlock statBlock;
	

	void OnEnable()
	{
		baseHealth = target as BaseHealth;
		statBlock = baseHealth.gameObject.GetComponent<StatBlock>();
	}

	public override void OnInspectorGUI()
	{
		if(statBlock == null)
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
