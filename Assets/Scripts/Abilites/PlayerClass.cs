using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerGameplay")]
public class PlayerClass : ScriptableObject
{
	public new string name;
    public AbilitySet abilities;
	public List<StatInspectorValue> stats;
}