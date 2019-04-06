using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStation : BaseInteractable
{
	protected override bool CanDo()
	{
		return true;
	}

	protected override void ToDo(GameObject user)
	{
		if(DEBUGFLAGS.INTERACTABLES) Debug.Log("DamageStation");
		Lib.FindInHierarchy<IDamagable>(user)?.Damage(1, gameObject, gameObject);
	}
}
