using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerInput : MonoBehaviour, IGameplayActions
{
	public static List<PlayerInput> all = new List<PlayerInput>();

	public bool testing = false;

    public MasterControls controls;
	public InputDevice inputDevice;
	public int playerID = -1;

	private PlayerMovement playerMovement;

	void OnEnable()
	{
		all.Add(this);
		controls.Enable();
	}
	void OnDisable()
	{
		all.Remove(this);
		controls.Disable();
	}

	void Awake()
	{
		controls.Gameplay.SetCallbacks(this);
		playerMovement = GetComponent<PlayerMovement>();
		GetPlayerID();
		if(testing)
		{
			inputDevice = InputSystem.GetDevice<Gamepad>();
		}
	}

	void GetPlayerID()
	{
		if(playerID != -1)
		{
			return;
		}
		int max = -1;
		for(int x = 0; x < all.Count; ++x)
		{
			max = Mathf.Max(max, all[x].playerID);
		}
		playerID = max + 1;
	}

	bool ValidateMyInput(InputAction.CallbackContext ctx)
	{
		if(ctx.action.lastTriggerControl.device == inputDevice)
		{
			return true;
		}
		return false;
	}

	public void OnMovement(InputAction.CallbackContext ctx)
	{
		//if(DEBUGFLAGS.DEBUGMOVEMENT) Debug.Log("OM " + Time.time);
		if(ValidateMyInput(ctx))
		{
			if(ctx.action.lastTriggerControl.device is Gamepad)
			{
				playerMovement.Move((ctx.action.lastTriggerControl.device as Gamepad).leftStick.ReadValue());
			}
			else
			{
				playerMovement.Move(ctx.ReadValue<Vector2>());
			}
		}
	}
}
