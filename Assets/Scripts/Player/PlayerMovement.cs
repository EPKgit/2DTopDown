using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class PlayerMovement : MonoBehaviour
{
	private Vector2 direction;

	private Rigidbody2D rb;
	private StatBlock stats;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		stats = GetComponent<StatBlock>();
	}

	void Start()
	{
		if(!stats.HasStat(StatName.Agility))
		{
			throw new System.InvalidOperationException("PlayerMovement on " + gameObject.name + " doesn't have an agility value to work with");
		}
	}

	void Update()
	{
		rb.velocity = direction * stats.GetValue(StatName.Agility);
	}

	public void Move(Vector2 dir)
	{
		direction = dir;
	}
}
