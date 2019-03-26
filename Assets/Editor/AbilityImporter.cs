using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AbilityImporter : EditorWindow
{
    
    [MenuItem("Window/AbilityImporter")]
    public static void Init()
    {
        GetWindow(typeof(AbilityImporter));
    }

    void OnGUI()
    {
		if(GUILayout.Button("Reimport"))
		{
			string[] abilityPaths = AssetDatabase.FindAssets("t:Ability");
			List<string> alreadyCreatedAssets = new List<string>();
			foreach(string s in abilityPaths)
			{
				alreadyCreatedAssets.Add(((Ability)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Ability))).name);
			}

			string[] allAbilityScriptsNames = AssetDatabase.FindAssets("", new [] {"Assets/ScriptableObjects/Abilities"});//ScriptableObjects/Abilities"});
			foreach(string s in allAbilityScriptsNames)
			{
				string abilityName = AssetDatabase.GUIDToAssetPath(s).Substring(AssetDatabase.GUIDToAssetPath(s).LastIndexOf('/') + 1);
				abilityName = abilityName.Substring(0, abilityName.LastIndexOf('.'));
				if(alreadyCreatedAssets.Contains(abilityName))
				{
					continue;
				}
				ScriptableObject newEffectAsset = ScriptableObject.CreateInstance(abilityName);
				AssetDatabase.CreateAsset(newEffectAsset, "Assets/ScriptableObjects/Abilities/" + abilityName +".asset");
				Debug.Log("Creating new asset: " + abilityName + ".asset");
			}
			Debug.Log("Tool running complete");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			return;
		}
	}
}
