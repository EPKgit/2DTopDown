using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePathPath : BaseProjectile
{
	public float tickTime = 1f;
	public float amount = 1f;

    private Dictionary<IHealable, float> timers;
	private List<IHealable> inside;

	public override void Reset()
	{
		base.Reset();
		timers = new Dictionary<IHealable, float>();
		inside = new List<IHealable>();
		timeLeft = 10;
	}

	public void Setup(Vector3 pos, Vector3 dir, GameObject p, float w)
	{
		base.Setup(pos, Vector3.zero, p);
		this.transform.up = dir;
		this.transform.localScale = new Vector3(1, w, 1);
	}

	protected override void Update()
	{
		base.Update();
		foreach(IHealable i in inside)
		{
			if(timers.ContainsKey(i))
			{
				timers[i] -= Time.deltaTime;
				if(timers[i] <= 0)
				{
					timers[i] = tickTime;
					i.Heal(amount, gameObject, creator);
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
		IHealable i = Lib.FindInHierarchy<IHealable>(col.gameObject);
		if(i == null)
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
		IHealable i = Lib.FindInHierarchy<IHealable>(col.gameObject);
		if(i == null)
		{
			return;
		}
		inside.Remove(i);
		if(inside.Count <= 0)
		{
			this.enabled = false;
		}
	}
}
