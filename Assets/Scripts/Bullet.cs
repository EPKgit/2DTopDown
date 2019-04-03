using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Poolable
{
	public GameObject bulletEffect;

	private float timeLeft;
	private CircleCollider2D collider;
	private Rigidbody2D rb;
	private GameObject player;

	public override void PoolInit(GameObject g)
	{
		base.PoolInit(g);
		collider = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	public override void Reset()
	{
		timeLeft = 6f;
		GetComponent<TrailRenderer>().Clear();
	}

	public void Setup(Vector3 pos, Vector3 direction, GameObject p)
	{
		transform.position = pos;
		rb.velocity = direction;
		player = p;
	}

	void Update()
	{
		timeLeft -= Time.deltaTime;
		if(timeLeft <= 0)
		{
			DestroySelf();
		}
	}

	protected override void DestroySelf()
	{
		base.DestroySelf();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(!Lib.HasTagInHierarchy(collision.otherCollider.gameObject, "Enemy"))
		{
			return;
		}
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("collision");
		Lib.FindInHierarchy<IDamagable>(collision.otherCollider.gameObject)?.Damage(1, player);
		BulletEffect(collision.contacts[0].point);
		DestroySelf();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(!Lib.HasTagInHierarchy(col.gameObject, "Enemy"))
		{
			return;
		}
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger");
		Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(1, player);
    	BulletEffect(transform.position);
   		DestroySelf();
	}

	void BulletEffect(Vector3 position) {
		Quaternion rot = Quaternion.LookRotation(-GetComponent<Rigidbody2D>().velocity);
		GameObject effect = Instantiate(bulletEffect, position, rot);
		Destroy(effect, 1f);
	}
}
