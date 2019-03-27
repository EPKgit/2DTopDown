using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySet", menuName = "Abilities/AbilitySet", order = 1)]
public class AbilitySet : ScriptableObject
{
	public new string name;
	public Ability ability1;
	public Ability ability2;
	public Ability ability3;
	public Ability attack;
}
