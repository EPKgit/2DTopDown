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
		Debug.Log("updato speedo");
		movementSpeed = f;
	}

	public void UpdateSpeed(StatBlock s)
	{
		stats = s;
		s.GetStat(StatName.Agility)?.RegisterStatChangeCallback(UpdateSpeed);
		movementSpeed = s?.GetStat(StatName.Agility)?.value ?? movementSpeed;
	}
}
