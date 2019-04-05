using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerAbilities : MonoBehaviour
{
	public delegate void AbilityInitializationDelegate(Ability a1, Ability a2, Ability a3, Ability attack);
	public event AbilityInitializationDelegate initializedEvent = delegate { };

    public AbilitySet abilitySet;

	[HideInInspector]
	public Rigidbody2D rb;
	[HideInInspector]
	public CircleCollider2D col;
	[HideInInspector]
	public StatBlock stats;

	private bool init = false;

	private Ability ability1;
	private Ability ability2;
	private Ability ability3;
	private Ability attack;

	private List<Ability> currentlyTicking;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = transform.Find("Colliders").GetComponent<CircleCollider2D>();
		stats = GetComponent<StatBlock>();
		currentlyTicking = new List<Ability>();
	}

	void Start()
	{
		Initialize(abilitySet);
	}

	public void Initialize(AbilitySet _as)
	{
		abilitySet = _as;
		attack = ScriptableObject.Instantiate(abilitySet.attack);
		ability1 = ScriptableObject.Instantiate(abilitySet.ability1);
		ability2 = ScriptableObject.Instantiate(abilitySet.ability2);
		ability3 = ScriptableObject.Instantiate(abilitySet.ability3);
		attack.Initialize(this);
		ability1.Initialize(this);
		ability2.Initialize(this);
		ability3.Initialize(this);
		init = true;
		initializedEvent(ability1, ability2, ability3, attack);
	}

	public void RegisterAbilityCooldownCallbacks(CooldownTickDelegate at, CooldownTickDelegate a1, CooldownTickDelegate a2, CooldownTickDelegate a3)
	{
		if(!init)
		{
			return;
		}
		attack.cooldownTick += at;
		ability1.cooldownTick += a1;
		ability2.cooldownTick += a2;
		ability3.cooldownTick += a3;
	}

	public List<string> GetCurrentlyTickingAbilities()
	{
		List<string> ret = new List<string>();
		foreach(Ability a in currentlyTicking)
		{
			ret.Add(a.ToString());
		}
		return ret;
	}

	void Update()
	{
		for(int x = currentlyTicking.Count - 1; x >= 0; --x)
		{
			if(currentlyTicking[x].Tick(Time.deltaTime))
			{
				currentlyTicking[x].FinishAbility();
				currentlyTicking.RemoveAt(x);
			}
		}
		ability1.Cooldown(Time.deltaTime);
		ability2.Cooldown(Time.deltaTime);
		ability3.Cooldown(Time.deltaTime);
		attack.Cooldown(Time.deltaTime);
	}

	public void Attack(InputAction.CallbackContext ctx, Vector2 dir)
	{
		UseAbility(attack, ctx, dir);
	}

	public void Ability1(InputAction.CallbackContext ctx, Vector2 dir)
	{
		UseAbility(ability1, ctx, dir);
	}

	public void Ability2(InputAction.CallbackContext ctx, Vector2 dir)
	{
		UseAbility(ability2, ctx, dir);
	}

	public void Ability3(InputAction.CallbackContext ctx, Vector2 dir)
	{
		UseAbility(ability3, ctx, dir);
	}

	public void UseAbility(Ability a, InputAction.CallbackContext ctx, Vector2 dir)
	{
		if(a.AttemptUseAbility(ctx, dir))
		{
			if(a.tickingAbility)
			{
				currentlyTicking.Add(a);
			}
			else
			{
				a.FinishAbility();
			}
		}
	}
}
