using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class LifeOrDeath : Ability
{
	public float percentageHealthCost;
	public float percentageDamageBonus;

	private List<System.Tuple<Stat, int>> bonuses;

	public override void Reinitialize()
	{
		base.Reinitialize();
		bonuses = new List<System.Tuple<Stat, int>>();
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		playerAbilities.hp.SetHealth(playerAbilities.hp.GetCurrentHealth() * -percentageHealthCost);
		StatBlock sb;
		Stat s;
		foreach(PlayerInput pi in PlayerInput.all)
		{
			sb = pi.gameObject.GetComponent<StatBlock>();
			s = sb.GetStat(StatName.DamagePercentage);
			bonuses.Add(new System.Tuple<Stat, int>(s, s.AddMultiplicativeModifier(percentageDamageBonus)));
		}
	}

	public override void FinishAbility()
	{
		foreach(System.Tuple<Stat, int> s in bonuses)
		{
			s.Item1.RemoveMultiplicativeModifier(s.Item2);
		}
		base.FinishAbility();
	}
}
