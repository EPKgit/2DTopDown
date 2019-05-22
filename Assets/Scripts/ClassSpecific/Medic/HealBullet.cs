using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBullet : BaseProjectile
{
	public GameObject bulletEffect;

	private float damage;

	public override void Reset()
	{
		base.Reset();
		Lib.FindInHierarchy<TrailRenderer>(gameObject).Clear();
	}

	public void Setup(Vector3 pos, Vector3 direction, GameObject p, float d)
	{
		transform.position = pos;
		rb.velocity = direction;
		creator = p;
		damage = d;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger " + creator);

    if(creator == null || Lib.HasParent(col.gameObject, creator))
    {
      return;
    }
		if(Lib.HasTagInHierarchy(col.gameObject, "Player")) // Temp for testing, will be "Enemy" later
		{
			Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(damage, gameObject, creator);
			Lib.FindInHierarchy<IHealable>(creator)?.Heal(damage / 2, gameObject, creator);
      BulletEffect(transform.position);
      DestroySelf();
		} else if(col.gameObject.layer!=13) {
      BulletEffect(transform.position);
      DestroySelf();
    }
		/*if(Lib.HasTagInHierarchy(col.gameObject, "Player"))
		{
			Lib.FindInHierarchy<IHealable>(col.gameObject)?.Heal(damage / 2, gameObject, creator);
			BulletEffect(transform.position);
			DestroySelf();
		}*/
	}

	void BulletEffect(Vector3 position) {
		Quaternion rot = Quaternion.LookRotation(-GetComponent<Rigidbody2D>().velocity);
		GameObject effect = Instantiate(bulletEffect, position, rot);
		Destroy(effect, 1f);
	}
}
