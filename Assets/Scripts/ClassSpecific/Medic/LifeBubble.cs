using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBubble : MonoBehaviour
{
	public float tickTime = 1f;

	private bool _isActive;
	private bool isActive
	{
		set 
		{
			if (_isActive != value)
			{
				GetComponent<SpriteRenderer>().enabled = value;
				_isActive = value;
			}
		}

		get
		{
			return _isActive;
		}
	}

    private Dictionary<BaseHealth, float> timers;
	private List<BaseHealth> inside;

	private BaseHealth self;

	public float healRate = 2f;
	public float selfDamageRatio = .25f;
	public float healthMinRatio = .25f;

	protected void Start()
	{
		self = Lib.FindInHierarchy<BaseHealth>(gameObject);
		timers = new Dictionary<BaseHealth, float>();
		inside = new List<BaseHealth>();
	}

	protected void Update()
	{
		isActive = self.GetCurrentHealth() > self.GetMaxHealth() * healthMinRatio;

		foreach(BaseHealth i in inside)
		{
			if (!isActive)
				return;
			if(timers.ContainsKey(i))
			{
				timers[i] -= Time.deltaTime;
				if(timers[i] <= 0)
				{
					timers[i] = tickTime;

					float amountToHeal = (i.GetCurrentHealth() + healRate >= i.GetMaxHealth()) ? i.GetMaxHealth() - i.GetCurrentHealth() : healRate; 
					
					i.Heal(healRate, gameObject, self.gameObject);
					
					self.Damage(amountToHeal * selfDamageRatio, gameObject, i.gameObject);
				}
			}
			else
			{
				timers.Add(i, tickTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		BaseHealth i = Lib.FindInHierarchy<BaseHealth>(col.gameObject);
		if(i == null || !Lib.HasTagInHierarchy(col.gameObject, "Player"))
            {
			return;
		}
		this.enabled = true;
		if(!inside.Contains(i))
		{
			inside.Add(i);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		BaseHealth i = Lib.FindInHierarchy<BaseHealth>(col.gameObject);
		if(i == null)
		{
			return;
		}
		inside.Remove(i);
	}
}
