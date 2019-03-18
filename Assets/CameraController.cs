using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float lerpFactor = 0.4f;

	private float baseZ;

	void Awake()
	{
		baseZ = transform.position.z;
	}

	void Update()
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
