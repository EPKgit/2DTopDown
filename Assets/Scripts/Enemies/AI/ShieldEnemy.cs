using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : BaseEnemy
{
	public float turnSpeed = 0.05f;

    void Update()
	{
		Vector2 dir = (chosenPlayer.transform.position - transform.position).normalized;
		rb.velocity = dir * speed;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(Mathf.Rad2Deg * -Mathf.Atan2(dir.x, dir.y), Vector3.forward), turnSpeed);
		//transform.rota
	}
}
