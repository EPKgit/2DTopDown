using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	//USE ME TO ACCESS CURRENT FINAL VALUE
	//SHOULD BE KEPT UPDATED AS BONUSES ARE ADDED AND REMOVED
    public float value
	{
		get
		{
			return _value;
		}
	}
	private float _value;

	public StatName name;

	public delegate void StatChangeDelegate(float value);
	public event StatChangeDelegate statChangeEvent = delegate {};

    private float baseValue;
    private List<Tuple<float, int>> multiplicativeModifiers = new List<Tuple<float, int>>();
    private List<Tuple<float, int>> additiveModifiers = new List<Tuple<float, int>>();

	public Stat(StatName s, float f)
	{
		name = s;
		SetBaseValue(f);
	}

	public Stat(StatInspectorValue s)
	{
		name = s.name;
		SetBaseValue(s.value);
		if(DEBUGFLAGS.STATS) Debug.Log(name + " created with value:" + baseValue);
	}

    private void UpdateCurrentValue()
    {
        float finalResult = baseValue;
        foreach(Tuple<float, int> t in additiveModifiers)
        {
            finalResult += t.Item1;
        }
        foreach(Tuple<float, int> t in multiplicativeModifiers)
        {
            finalResult *= t.Item1;
        }
        _value = finalResult;
		statChangeEvent(_value);
    }

    public void SetBaseValue(float f)
    {
        baseValue = f;
        UpdateCurrentValue();
    }

    public void AddAdditiveModifier(float f, int i) // float for the value, i for the ID of the effect, ID will get set by the statblock
    {
        additiveModifiers.Add(new Tuple<float, int>(f, i));
        UpdateCurrentValue();
    }

    public void AddMultiplicativeModifier(float f, int i)
    {
        multiplicativeModifiers.Add(new Tuple<float, int>(f, i));
        UpdateCurrentValue();
    }
    
    public void RemoveAdditiveModifier(int i)
    {
        additiveModifiers.RemoveAll( (t) => t.Item2 == i );
        UpdateCurrentValue();
    }

    public void RemoveMultiplicativeModifier(int i)
    {
        multiplicativeModifiers.RemoveAll( (t) => t.Item2 == i );
        UpdateCurrentValue();
    }

	public void RegisterStatChangeCallback(StatChangeDelegate d)
	{
		statChangeEvent += d;
	}
	public void UnregisterStatChangeCallback(StatChangeDelegate d)
	{
		statChangeEvent -= d;
	}


	public override string ToString()
	{
		return string.Format("{0}:{1}", name, _value);
	}
}
