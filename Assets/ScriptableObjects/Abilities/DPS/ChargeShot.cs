using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class ChargeShot : Ability
{
	public GameObject bulletPrefab;
	public float minSpeed;
	public float maxSpeed;
	public float minSize;
	public float maxSize;
	public float maxChargeTime;
	public float damage;

	private bool started;
	private float chargeTimer;

	public override void Reinitialize()
	{
		started = false;
		base.Reinitialize();
	}

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(bulletPrefab, 3, true);
		base.Initialize(pa);
	}

	public override bool AttemptUseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		if(DEBUGFLAGS.ABILITY) Debug.Log(string.Format("{0} ATTEMPT USE perf:{1} strt:{2} canc:{3}", name, ctx.performed, ctx.started, ctx.cancelled));
		if(!IsCastable())
		{
			return false;
		}
		if(!started) //the first press of the button
		{
			started = true;
			chargeTimer = Time.time;
			return true;
		}
		started = false;
		UseAbility(ctx, inputDirection);
		return false;
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		float chargePercentage = (Time.time - chargeTimer > maxChargeTime) ? 1 : (Time.time - chargeTimer) / maxChargeTime;
		inputDirection = Lib.DefaultDirectionCheck(inputDirection);
		inputDirection *= Mathf.Lerp(minSpeed, maxSpeed, chargePercentage);
		GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
		float size = Mathf.Lerp(minSize, maxSize, chargePercentage);
		temp.transform.localScale = new Vector3(size, size, size);
		temp.GetComponent<Bullet>().Setup
		(
			playerAbilities.transform.position, inputDirection, playerAbilities.gameObject, 
		 	damage * playerAbilities.stats.GetValue(StatName.DamagePercentage)
		);
		temp.GetComponent<Poolable>().Reset();
	}

	public override void FinishAbility()
	{
		if(!started)
		{
			base.FinishAbility();
		}
	}
}
