using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("collision");
		Lib.FindInHierarchy<IDamagable>(collision.otherCollider.gameObject)?.Damage(1, gameObject);
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger");
		Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(1, gameObject);
		Destroy(gameObject);
	}
}
