using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class ChargeShot : Ability
{
	protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
	}

	public override bool AttemptUseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		return base.AttemptUseAbility(ctx, inputDirection);
	}
}
