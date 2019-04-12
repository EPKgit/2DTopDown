﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
	public float lerpFactor = 0.4f;

	private float baseZ;

	protected override void Awake()
	{
		base.Awake();
		baseZ = transform.position.z;
	}

	void LateUpdate()
	{
		float xVal = 0;
		float yVal = 0;
		if(PlayerInput.all.Count == 0)
		{
			return;
		}
		for(int x = 0; x < PlayerInput.all.Count; ++x)
		{
			xVal += PlayerInput.all[x].transform.position.x;
			yVal += PlayerInput.all[x].transform.position.y;
		}
		xVal /= PlayerInput.all.Count;
		yVal /= PlayerInput.all.Count;
		transform.position = Vector3.Lerp(transform.position, new Vector3(xVal, yVal, baseZ), lerpFactor);
	}
}