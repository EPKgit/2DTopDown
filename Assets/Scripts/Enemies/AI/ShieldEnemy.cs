using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : BaseEnemy
{
	public float blockAngle;
	public float turnSpeed = 0.05f;

	void OnEnable()
	{
		hp.preDamageEvent += CheckIfBlocked;
	}
	void OnDisable()
	{
		hp.preDamageEvent -= CheckIfBlocked;
	}

	void CheckIfBlocked(HealthChangeEventData hced)
	{
		float angle = Vector3.Angle(transform.up, hced.localSource.transform.position - hced.target.transform.position);
		if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log(angle);
		if(angle < blockAngle)
		{
			if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log("cancelling");
			hced.cancelled = true;
			ShieldClankEffect(hced.localSource);
		} else {
			DamageEffect(hced.localSource);
    	}
	}

	private void ShieldClankEffect(GameObject source) {
		GameObject splode = Instantiate(hp.WhiteSplodeEffect, source.transform.position, Quaternion.identity);
		splode.transform.localScale *= 0.5f;
	}

	private void DamageEffect(GameObject source) {
		GameObject splode = Instantiate(hp.RedSplodeEffect, source.transform.position, Quaternion.identity);
		splode.transform.localScale *= 0.5f;
	}

    protected override bool Update()
	{
		if(!base.Update())
		{
			return false;
		}
		//Just walks towards the player
		Vector2 dir = (chosenPlayer.transform.position - transform.position).normalized;
		rb.velocity = dir * speed;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(Mathf.Rad2Deg * -Mathf.Atan2(dir.x, dir.y), Vector3.forward), turnSpeed);
		return true;
	}
}
