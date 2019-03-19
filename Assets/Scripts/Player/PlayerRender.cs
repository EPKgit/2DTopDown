using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRender : MonoBehaviour
{
	private PlayerInput playerInput;
	private SpriteRenderer sprite;
    IEnumerator Start()
	{
		sprite = transform.Find("Render").GetComponent<SpriteRenderer>();
		playerInput = GetComponent<PlayerInput>();
		yield return new WaitUntil( () => playerInput.playerID != -1);
		sprite.color = Lib.GetPlayerColorByIndex(playerInput.playerID);
	}
}
