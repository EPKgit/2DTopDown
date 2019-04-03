using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed;

	private Vector2 direction;

	private Rigidbody2D rb;
	private StatBlock stats;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		stats = GetComponent<StatBlock>();
	}

	void OnEnable()
	{
		UpdateSpeed(stats);
		stats?.GetStat(StatName.Agility)?.RegisterStatChangeCallback(UpdateSpeed);
		stats?.RegisterInitializationCallback(UpdateSpeed);
	}

	void OnDisable()
	{
		stats?.GetStat(StatName.Agility)?.UnregisterStatChangeCallback(UpdateSpeed);
		stats?.DeregisterInitializationCallback(UpdateSpeed);
	}

	void Update()
	{
		rb.velocity = direction * movementSpeed;
	}

	public void Move(Vector2 dir)
	{
		direction = dir;
	}

	public void UpdateSpeed(float f)
	{
		movementSpeed = f;
	}

	public void UpdateSpeed(StatBlock s)
	{
		movementSpeed = stats?.GetStat(StatName.Agility)?.value ?? movementSpeed;
	}
}
