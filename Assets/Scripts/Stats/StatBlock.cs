using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatName { Strength, Agility, Toughness }

public class StatBlock : MonoBehaviour
{	
	public List<StatInspectorValue> inspectorValues;

	private Dictionary<StatName, Stat> stats;
	
	void OnValidate()
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

	void Awake()
	{
		OnValidate();
	}
	
	public bool HasStat(StatName name)
	{
		return stats.ContainsKey(name);
	}

	public float GetValue(StatName name)
	{
		Stat temp;
		stats.TryGetValue(name, out temp);
		return temp == null ? -1f : temp.value;
	}

	public Stat GetStat(StatName name)
	{
		Stat temp = null;
		stats.TryGetValue(name, out temp);
		return temp;
	}
}