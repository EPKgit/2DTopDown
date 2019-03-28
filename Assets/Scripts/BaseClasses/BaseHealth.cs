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
	private StatBlock stats;

    void Awake()
    {
		stats = GetComponent<StatBlock>();
		maxHealth = stats?.GetValue(StatName.Toughness) ?? maxHealth;
		currentHealth = maxHealth;
    }

	void OnEnable()
	{
		stats?.GetStat(StatName.Toughness)?.RegisterStatChangeCallback(UpdateMaxHealth);
		stats?.RegisterInitializationCallback(UpdateMaxHealth);
	}

	void OnDisable()
	{
		stats?.GetStat(StatName.Toughness)?.UnregisterStatChangeCallback(UpdateMaxHealth);	
		stats?.DeregisterInitializationCallback(UpdateMaxHealth);
	}

	public void UpdateMaxHealth(float value)
	{
		currentHealth = currentHealth / maxHealth * value;
		maxHealth = value;
	}

	public void UpdateMaxHealth(StatBlock s)
	{
		float value = s.GetValue(StatName.Toughness);
		if(value == -1)
		{
			return;
		}
		UpdateMaxHealth(value);
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

	public float GetCurrentHealth()
	{
		return currentHealth;
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
