using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlock : MonoBehaviour
{	
	public string[] statNames;
	public float[] defaultValues;

	private Dictionary<string, Stat> stats;
	
	void Awake()
	{
		if(statNames.Length != defaultValues.Length)
		{
			throw new System.InvalidOperationException("StatBlock not correctly initialized on " + gameObject.name);
		}
		for(int a = 0; a < statNames.Length; ++a)
		{
			stats.Add(statNames[a], new Stat(statNames[a], defaultValues[a]));
		}
	}
	
	public bool StatExists(string name)
	{
		foreach(string s in statNames)
		{
			if(s.Equals(name))
			{
				return true;
			}
		}
		return false;
	}

	public float GetValue(string name)
	{
		Stat temp;
		stats.TryGetValue(name, out temp);
		return temp == null ? -1f : temp.value;
	}
}