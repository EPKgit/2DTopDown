using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class ShotDash : Ability
{
	public GameObject bulletPrefab;
	public float damage;
	public float bulletSpeed;
	public float dashForce;

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(bulletPrefab, 3, true);
		base.Initialize(pa);
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		inputDirection = Lib.DefaultDirectionCheck(inputDirection);
		playerAbilities.rb.velocity = -inputDirection * dashForce;
		inputDirection *= bulletSpeed;
		GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
		temp.GetComponent<Bullet>().Setup
		(
			playerAbilities.transform.position, inputDirection, playerAbilities.gameObject, 
		 	damage * playerAbilities.stats.GetValue(StatName.DamagePercentage)
		);
		temp.GetComponent<Poolable>().Reset();
		
	}
}
