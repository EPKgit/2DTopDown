using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : Poolable
{
	protected float timeLeft;
	protected Collider2D col;
	protected Rigidbody2D rb;
	protected GameObject creator;

	public override void PoolInit(GameObject g)
	{
		base.PoolInit(g);
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	public override void Reset()
	{
		timeLeft = 6f;
	}

	public virtual void Setup(Vector3 pos, Vector3 direction, GameObject p)
	{
		transform.position = pos;
		rb.velocity = direction;
		creator = p;
	}

	protected virtual void Update()
	{
		timeLeft -= Time.deltaTime;
		if(timeLeft <= 0)
		{
			DestroySelf();
		}
	}
}
