using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
	public float blockAngle;
	public GameObject SplodeEffect;
	public GameObject RedSplodeEffect;

	void OnEnable()
	{
		preDamageEvent += CheckIfBlocked;
	}
	void OnDisable()
	{
		preDamageEvent -= CheckIfBlocked;
	}

	void CheckIfBlocked(HealthChangeEventData hced)
	{
		float angle = Vector3.Angle(transform.up, hced.source.transform.position - hced.target.transform.position);
		if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log(angle);
		if(angle < blockAngle)
		{
			if(DEBUGFLAGS.ENEMYHEALTH) Debug.Log("cancelling");
			hced.cancelled = true;
			ShieldClankEffect(hced.source);
		} else {
			DamageEffect(hced.source);
    	}
	}

  protected override void Die()
	{
		Destroy(gameObject);
    	DieEffect();
	}

  private void ShieldClankEffect(GameObject source) {
		GameObject splode = Instantiate(SplodeEffect, source.transform.position, Quaternion.identity);
    	splode.transform.localScale *= 0.5f;
  }

  private void DamageEffect(GameObject source) {
		GameObject splode = Instantiate(RedSplodeEffect, source.transform.position, Quaternion.identity);
    	splode.transform.localScale *= 0.5f;
  }

  private void DieEffect() {
		GameObject splode = Instantiate(RedSplodeEffect, transform.position, Quaternion.identity);
  }
}
