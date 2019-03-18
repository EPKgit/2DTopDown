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
		switch(playerInput.playerID)
		{
			case 1:
				sprite.color = Color.red;
				break;
			case 2:
				sprite.color = Color.green;
				break;
			case 3:
				sprite.color = Color.blue;
				break;
			case 4:
				sprite.color = Color.yellow;
				break;
			default:
				sprite.color = Color.black;
				break;

		}
	}
}
