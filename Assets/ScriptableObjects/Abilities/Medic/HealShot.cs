using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class HealShot : Ability
{
	public GameObject bulletPrefab;
	public float moveSpeed;
	public float damage;

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(bulletPrefab, 5, true);
		base.Initialize(pa);
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		inputDirection = Lib.DefaultDirectionCheck(inputDirection);
		inputDirection *= moveSpeed;
		GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
		temp.GetComponent<HealBullet>().Setup
		(
			playerAbilities.transform.position, 
			inputDirection, 
			playerAbilities.gameObject, 
			damage * playerAbilities.stats.GetValue(StatName.DamagePercentage)
		);
		temp.GetComponent<Poolable>().Reset();
	}
}
