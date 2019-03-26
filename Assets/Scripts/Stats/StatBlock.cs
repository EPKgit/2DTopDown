using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatName { Strength, Agility, Toughness }

public class StatBlock : MonoBehaviour
{	
	/// <summary>
	/// List of structs that let us set the values of the StatBlock in the inspector
	/// </summary>
	public List<StatInspectorValue> inspectorValues;

	private Dictionary<StatName, Stat> stats;
	
	void OnValidate()
	{
		stats = new Dictionary<StatName, Stat>();
		for(int a = 0; a < inspectorValues.Count; ++a)
		{
			if(!HasStat(inspectorValues[a].name))
			{
				stats.Add(inspectorValues[a].name, new Stat(inspectorValues[a]));
			}
			
		}
	}

	void Awake()
	{
		stats = new Dictionary<StatName, Stat>();
		for(int a = 0; a < inspectorValues.Count; ++a)
		{
			if(HasStat(inspectorValues[a].name))
			{
				throw new System.InvalidOperationException("StatBlock incorrectly initialized with duplicate stats on " + gameObject.name);
			}
			stats.Add(inspectorValues[a].name, new Stat(inspectorValues[a]));
		}
		
	}
	
	/// <summary>
	/// Checks if the StatBlock contains a certain stat
	/// </summary>
	/// <param name="name">The name of the stat to check</param>
	/// <returns>Returns true if the stat is in the block, false if not</returns>
	public bool HasStat(StatName name)
	{
		return stats.ContainsKey(name);
	}

	/// <summary>
	/// Gives us access to the value of a certain stat
	/// </summary>
	/// <param name="name">The name of the stat to check</param>
	/// <returns>Returns the final value of the stat if it exists, -1 if it doesn't</returns>
	public float GetValue(StatName name)
	{
		Stat temp;
		stats.TryGetValue(name, out temp);
		return temp == null ? -1f : temp.value;
	}

	/// <summary>
	/// Gives us access to a certain stat. Useful for registering callbacks on a stat, or adding bonuses
	/// </summary>
	/// <param name="name">The name of the stat to get</param>
	/// <returns>Returns the stat if it exists, null otherwise</returns>
	public Stat GetStat(StatName name)
	{
		Stat temp = null;
		stats.TryGetValue(name, out temp);
		return temp;
	}
}