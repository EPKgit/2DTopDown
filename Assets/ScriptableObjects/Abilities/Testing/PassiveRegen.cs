﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PassiveRegen : Ability
{
	public float hpPerSecond;

	private float healInterval;
	private float timeSinceLastHeal;
	private IHealable hp;
	private GameObject owner;

	public override void Initialize(PlayerAbilities pa)
	{
		hp = Lib.FindInHierarchy<IHealable>(pa.gameObject);
		owner = pa.gameObject;
		healInterval = 1f / hpPerSecond;
	}

	public override bool Tick(float deltaTime)
	{
		timeSinceLastHeal += deltaTime;
		if(timeSinceLastHeal >= healInterval)
		{
			hp.Heal(1, owner);
			timeSinceLastHeal = 0;
		}
		return false;
	}   
}
