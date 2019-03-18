﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed;

	public Vector2 direction;

	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		rb.velocity = direction;
	}

	public void Move(Vector2 dir)
	{
		direction = dir;
	}
}
