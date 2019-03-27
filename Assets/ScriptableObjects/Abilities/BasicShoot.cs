using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class BasicShoot : Ability
{
	public GameObject bulletPrefab;
	public float moveSpeed;

    protected override void UseAbility(InputAction.CallbackContext ctx, Vector2 inputDirection)
	{
		inputDirection = Lib.DefaultDirectionCheck(inputDirection);
		inputDirection *= moveSpeed;
		GameObject temp = Instantiate(bulletPrefab, playerAbilities.gameObject.transform.position, Quaternion.identity);
		Physics2D.IgnoreCollision(playerAbilities.col, temp.GetComponent<CircleCollider2D>());
		Destroy(temp, 6f);
		temp.GetComponent<Rigidbody2D>().velocity = inputDirection;
	}
}
