using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
	public float blockAngle;

	void OnEnable()
	{
		preDamageEvent += CheckIfBlocked;
	}
	void OnDisable()
	{
		preDamageEvent += CheckIfBlocked;
	}

	void CheckIfBlocked(HealthChangeEventData hced)
	{
		float angle = Vector3.Angle(transform.up, hced.source.transform.position - hced.target.transform.position);
		if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log(angle);
		if(angle < blockAngle)
		{
			if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log("cancelling");
			hced.cancelled = true;
		}
	}

    protected override void Die()
	{
		Destroy(gameObject);
	}
}
