using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;

	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
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
		GameObject temp = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		Destroy(temp, 6f);
		temp.GetComponent<Rigidbody2D>().velocity = dir;
	}
}
