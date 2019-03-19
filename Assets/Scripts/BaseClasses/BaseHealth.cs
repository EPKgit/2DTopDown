using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IHealable, IDamagable
{

	public float maxHealth;
	
	private float currentHealth;

    void Start()
    {
		currentHealth = maxHealth;
    }

	public void Damage(float delta)
	{
		if(DEBUGFLAGS.HEALTH) Debug.Log("taking damage");
		currentHealth -= delta;
		if(currentHealth <= 0)
		{
			Die();
		}
	}

	public void Heal(float delta)
	{
		currentHealth += delta;
		if(currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	}

	protected virtual void Die()
	{
		transform.position = Vector3.zero;
		currentHealth = maxHealth;
	}
}
