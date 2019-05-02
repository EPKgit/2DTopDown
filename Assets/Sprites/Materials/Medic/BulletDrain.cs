using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class BulletDrain : Ability
{
	public float lifeStealPercentage;

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		foreach(PlayerInput pi in PlayerInput.all)
		{
			Lib.FindInHierarchy<PlayerCallbacks>(pi.gameObject).RegisterDealDamageDelegate(OnDealDamage);
		}	
	}

	public void OnDealDamage(HealthChangeNotificationData hcnd)
	{
		foreach(PlayerInput pi in PlayerInput.all)
		{
			Lib.FindInHierarchy<BaseHealth>(pi.gameObject).Heal(hcnd.value * lifeStealPercentage, playerAbilities.gameObject, playerAbilities.gameObject);
		}
	}

	public override void FinishAbility()
	{
		foreach(PlayerInput pi in PlayerInput.all)
		{
			Lib.FindInHierarchy<PlayerCallbacks>(pi.gameObject).DeregisterDealDamageDelegate(OnDealDamage);
		}	
		base.FinishAbility();
	}
}
