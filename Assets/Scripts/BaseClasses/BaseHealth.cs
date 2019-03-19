using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IHealable, IDamagable
{
	public float maxHealth;

	public event MutableHealthChangeDelegate preDamageEvent = delegate { };
	public event MutableHealthChangeDelegate preHealEvent = delegate { };
	public event HealthChangeNotificationDelegate postDamageEvent = delegate { };
	public event HealthChangeNotificationDelegate postHealEvent = delegate { };
	public event HealthChangeNotificationDelegate healthChangeEvent = delegate { };
	
	private float currentHealth;

    void Start()
    {
		currentHealth = maxHealth;
    }

	public void Damage(float delta, GameObject s)
	{
		if(DEBUGFLAGS.HEALTH) Debug.Log("taking damage");
		HealthChangeEventData data = new HealthChangeEventData(s, gameObject, delta);
		if(DEBUGFLAGS.HEALTH) Debug.Log("pre damage");
		preDamageEvent(data);
		if(data.cancelled)
		{
			if(DEBUGFLAGS.HEALTH) Debug.Log("cancelled");
			return;
		}
		if(DEBUGFLAGS.HEALTH) Debug.Log("not cancelled");
		currentHealth -= data.delta;
		postDamageEvent(data.delta);
		healthChangeEvent(-data.delta);
		
		if(currentHealth <= 0)
		{
			Die();
		}
	}

	public void Heal(float delta, GameObject s)
	{
		if(DEBUGFLAGS.HEALTH) Debug.Log("healing");
		HealthChangeEventData data = new HealthChangeEventData(s, gameObject, delta);
		preHealEvent(data);
		if(data.cancelled)
		{
			return;
		}
		currentHealth += data.delta;
		postHealEvent(data.delta);
		healthChangeEvent(data.delta);
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

public delegate void MutableHealthChangeDelegate(HealthChangeEventData hced);
public delegate void HealthChangeNotificationDelegate(float delta);

public class HealthChangeEventData
{
	public GameObject source;
	public GameObject target;
	public float delta;
	public bool cancelled;
	public HealthChangeEventData(GameObject s, GameObject t, float d)
	{
		source = s;
		target = t;
		delta = d;
		cancelled = false;
	}
}
