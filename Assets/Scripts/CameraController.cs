using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
	public float lerpFactor = 0.4f;

  private Vector3 baseOffset;

	protected override void Awake()
	{
		base.Awake();
    baseOffset = new Vector3(0,6,-3);
    transform.rotation = Quaternion.Euler(60,0,0);
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
    Vector3 target = new Vector3(xVal, yVal, 0) + baseOffset;
		transform.position = Vector3.Lerp(transform.position, target, lerpFactor);
	}
}
