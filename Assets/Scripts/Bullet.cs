using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
	public GameObject bulletEffect;

	public override void Reset()
	{
		base.Reset();
		GetComponent<TrailRenderer>().Clear();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!Lib.HasTagInHierarchy(col.gameObject, "Enemy"))
		{
			return;
		}
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger");
		Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(1, gameObject, creator);
    	BulletEffect(transform.position);
   		DestroySelf();
	}

	void BulletEffect(Vector3 position) {
		Quaternion rot = Quaternion.LookRotation(-GetComponent<Rigidbody2D>().velocity);
		GameObject effect = Instantiate(bulletEffect, position, rot);
		Destroy(effect, 1f);
	}
}
