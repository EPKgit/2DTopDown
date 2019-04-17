using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class AndroidGuns : Ability
{
	public GameObject gunPrefab;
	public GameObject bulletPrefab;
	public int numGuns;
	public float damage;
	public float bulletSpeed;
	public float aggroRadius;
	public float maxDisableTime;
	public float shootingCooldown;

	private List<GameObject> spawnedGuns;

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(gunPrefab, numGuns, true);
		PoolManager.instance.AddPoolSize(bulletPrefab, (int)Mathf.Ceil(1 / shootingCooldown) * numGuns * 2, true);
		spawnedGuns = new List<GameObject>();
		base.Initialize(pa);
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		Vector3 dir = new Vector2();
		float x, y;
		float angle;
		for(int i = 0; i < numGuns; ++i)
		{
			angle = i * 2 * Mathf.PI / numGuns;
			x = Mathf.Cos(angle);
			y = Mathf.Sin(angle);
			dir.Set(x, y, 0);
			GameObject temp = PoolManager.instance.RequestObject(gunPrefab);
			temp.GetComponent<AndroidGunController>().Setup
			(
				playerAbilities.transform.position,
				damage * playerAbilities.stats.GetValue(StatName.DamagePercentage),
				bulletSpeed,
				aggroRadius,
				maxDisableTime,
				shootingCooldown,
				playerAbilities.gameObject,
				dir,
				bulletPrefab
			);
			temp.GetComponent<Poolable>().Reset();
			spawnedGuns.Add(temp);
		}
	}

	public override void FinishAbility()
	{
		for(int x = spawnedGuns.Count - 1; x >= 0; --x)
		{
			spawnedGuns[x].GetComponent<Poolable>().DestroySelf();
		}
		spawnedGuns.Clear();
		base.FinishAbility();
	}
}

