using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;

	private Rigidbody2D rb;
	private CircleCollider2D col;
	private StatBlock stats;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = transform.Find("Colliders").GetComponent<CircleCollider2D>();
		stats = GetComponent<StatBlock>();
	}

	public void Shoot(Vector2 dir)
	{	
		if(dir.x == 0 && dir.y == 0)
		{
			dir = rb.velocity;
			if(dir.x == 0 && dir.y == 0)
			{
				dir = Vector2.right;
			}
		}
		dir.Normalize();
		dir *= 3;
		GameObject temp = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		Physics2D.IgnoreCollision(col, temp.GetComponent<CircleCollider2D>());
		Destroy(temp, 6f);
		temp.GetComponent<Rigidbody2D>().velocity = dir;
	}
}
