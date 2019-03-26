using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class StatBuff : Ability
{
	public float buffAmount;
	public StatName statToBuff;
	
	private int? bonusHandle;

	public override void Reinitialize()
	{
		base.Reinitialize();
		bonusHandle = null;
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		Debug.Log(string.Format("{0}/{1} tick:{2}", currentDuration, maxDuration, tickingAbility));
		bonusHandle = playerAbilities.stats.GetStat(statToBuff)?.AddAdditiveModifier(buffAmount);
		Debug.Log(string.Format("Buffing {1} by {0}", buffAmount, statToBuff.ToString()));

	}
	public override void FinishAbility()
	{
		if(!bonusHandle.HasValue)
		{
			return;
		}
		playerAbilities.stats.GetStat(statToBuff)?.RemoveAdditiveModifier(bonusHandle.Value);
		Debug.Log(string.Format("Buff of {0} to {1} expiring", buffAmount, statToBuff.ToString()));
	}

	public override string ToString()
	{
		return string.Format("{0}Buff {1}/{2}", statToBuff.ToString(), currentDuration, maxDuration);
	}
}