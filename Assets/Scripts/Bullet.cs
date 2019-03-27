using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public GameObject bulletEffect;
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("collision");
		Lib.FindInHierarchy<IDamagable>(collision.otherCollider.gameObject)?.Damage(1, gameObject);
    BulletEffect(collision.contacts[0].point);
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if(DEBUGFLAGS.COLLISIONS) Debug.Log("trigger");
		Lib.FindInHierarchy<IDamagable>(col.gameObject)?.Damage(1, gameObject);
    BulletEffect(transform.position);
    Destroy(gameObject);
	}

  void BulletEffect(Vector3 position) {
    Quaternion rot = Quaternion.LookRotation(-GetComponent<Rigidbody2D>().velocity);
    GameObject effect = Instantiate(bulletEffect, position, rot);
		Destroy(effect, 1f);
  }
}
