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
	private AggroData[] arr;

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
		if(GUILayout.Button("SetupEvent"))
		{
			baseEnemy.gameObject.GetComponent<BaseHealth>().postDamageEvent += UpdateUI;
		}
		if(aggroFoldout = EditorGUILayout.Foldout(aggroFoldout, "Aggro"))
		{
			arr = baseEnemy.GetAggroDataArray();
			if(arr == null)
			{
				return;
			}
			foreach(AggroData ad in arr)
			{
				EditorGUILayout.LabelField(string.Format("{0}:{1}", ad.source?.name ?? "NULL", ad.value));
			}
		}
	}

	public void UpdateUI(HealthChangeNotificationData hcnd)
	{
		EditorUtility.SetDirty(baseEnemy);
	}
}
