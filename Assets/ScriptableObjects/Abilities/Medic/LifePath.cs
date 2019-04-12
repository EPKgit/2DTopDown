﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class LifePath : Ability
{
	public GameObject bulletPrefab;
	public GameObject pathPrefab;

	public float moveSpeed;
	public float damage;
	public float width;

	public override void Initialize(PlayerAbilities pa)
	{
		PoolManager.instance.AddPoolSize(bulletPrefab, 2, true);
		PoolManager.instance.AddPoolSize(pathPrefab, 10, true);
		base.Initialize(pa);
	}

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		inputDirection = Lib.DefaultDirectionCheck(inputDirection);
		inputDirection *= moveSpeed;
		GameObject temp = PoolManager.instance.RequestObject(bulletPrefab);
		temp.GetComponent<LifePathBullet>().Setup(playerAbilities.transform.position, inputDirection, playerAbilities.gameObject, damage, pathPrefab, moveSpeed, width);
		temp.GetComponent<Poolable>().Reset();
	}
}