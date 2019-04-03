using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseHealth), typeof(Rigidbody2D))]
public abstract class BaseEnemy : MonoBehaviour
{
  	public float speed = 1f;

	/// <summary>
	/// The player that the AI will use to plan its pathing, up to the AI
	/// to use this value
	/// </summary>
	protected GameObject chosenPlayer;

	/// <summary>
	/// A sorted set of all aggrodata that the enemy has recieved from damage events. 
	/// </summary>
	protected PriorityQueue<AggroData> aggro;
	
	protected Rigidbody2D rb;
	protected StatBlock stats;
	protected BaseHealth hp;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		stats = GetComponent<StatBlock>();
		hp = GetComponent<BaseHealth>();
		aggro = new PriorityQueue<AggroData>(MenuManager.maxPlayers, new MaxAggroComparator());
		
		hp.postDamageEvent += AddAggroEvent;
	}

	protected virtual void Start()
	{
		UpdateChosenPlayer();
		if(chosenPlayer == null)
		{
			Debug.Log("Enemy cannot find target! " + gameObject.name);
			this.enabled = false;
		}
	}

	protected virtual void AddAggroEvent(HealthChangeNotificationData hcnd)
	{
		AggroData temp = aggro.GetValue( (t) => t.source == hcnd.source);
		if(temp == null)
		{
			aggro.Push(new AggroData(hcnd));
			return;
		}
		temp.value += hcnd.value * hcnd.aggroPercentage;
	}

	protected virtual void UpdateChosenPlayer()
	{
		chosenPlayer = aggro?.Peek()?.source ?? PlayerInput.all[Random.Range(0, PlayerInput.all.Count)].gameObject;;
	}

	public AggroData[] GetAggroDataArray()
	{
		return aggro.ToArray();
	}
}
