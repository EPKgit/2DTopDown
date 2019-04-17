using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class ShotRain : Ability
{
	public GameObject bulletPrefab;
	public float damage;
	public float bulletSpeed;
	public int numBullets;

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(bulletPrefab, numBullets * 2, true);
		base.Initialize(pa);
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		Vector2 dir = new Vector2();
		float x, y;
		float angle;
		for(int i = 0; i < numBullets; ++i)
		{
			angle = i * 2 * Mathf.PI / numBullets;
			x = Mathf.Cos(angle);
			y = Mathf.Sin(angle);
			dir.Set(x, y);
			dir *= bulletSpeed;
			GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
			temp.GetComponent<Bullet>().Setup
			(
				playerAbilities.transform.position, dir, playerAbilities.gameObject, 
				damage * playerAbilities.stats.GetValue(StatName.DamagePercentage)
			);
			temp.GetComponent<Poolable>().Reset();
		}
		
	}
}
