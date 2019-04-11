using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePathBullet : BaseProjectile
{
	private float width;
	private float interval;
	private GameObject pathPrefab;
	private float damage;
	private Vector3 startPosition;
	private float timer;

	public void Setup(Vector3 pos, Vector3 direction, GameObject p, float d, GameObject g, float speed, float w)
	{
		transform.position = pos;
		startPosition = pos;
		rb.velocity = direction;
		creator = p;
		damage = d;
		pathPrefab = g;
		interval = 1 / speed;
		width = w;
		timer = interval;
	}

	protected override void Update()
	{
		base.Update();
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			GameObject temp = PoolManager.instance.RequestObject(pathPrefab);
			temp.GetComponent<LifePathPath>().Setup(transform.position, rb.velocity, creator, width);
			temp.GetComponent<Poolable>().Reset();
			timer = interval;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!Lib.HasTagInHierarchy(col.gameObject, "Enemy"))
		{
			return;
		}
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger");
		Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(1, gameObject, creator);
   		DestroySelf();
	}
}
