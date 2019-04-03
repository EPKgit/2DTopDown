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
		float aggroValue = s.GetComponent<StatBlock>()?.GetStat(StatName.AggroPercentage)?.value ?? 1;
		HealthChangeNotificationData notifData = new HealthChangeNotificationData(s, data.delta, aggroValue);
		postDamageEvent(notifData);
		notifData.value *= -1;
		healthChangeEvent(notifData);
		
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
		float aggroValue = s.GetComponent<StatBlock>()?.GetStat(StatName.AggroPercentage)?.value ?? 1;
		HealthChangeNotificationData notifData = new HealthChangeNotificationData(s, data.delta, aggroValue);
		postHealEvent(notifData);
		healthChangeEvent(notifData);
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
public delegate void HealthChangeNotificationDelegate(HealthChangeNotificationData hcnd);

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

public class HealthChangeNotificationData
{
	public GameObject source;
	public float value;
	public float aggroPercentage;

	public HealthChangeNotificationData(GameObject s, float v, float a = 1)
	{
		source = s;
		value = v;
		aggroPercentage = a;
	}
}
