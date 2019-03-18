using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public enum InputType { KB, GP }

public class PlayerInput : MonoBehaviour, IGameplayActions
{
	public static List<PlayerInput> all = new List<PlayerInput>();

	public bool testing = false;

    public MasterControls controls;
	public InputDevice inputDevice;
	public int playerID = -1;

	private PlayerMovement playerMovement;
	private InputType inputType;

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
	}

	public void Initialize()
	{
		GetPlayerID();
		gameObject.name = "Player " + playerID;
		if(testing)
		{
			inputDevice = InputSystem.GetDevice<Gamepad>();
			inputType = InputType.GP;
		}
		else
		{
			inputType = (inputDevice is Gamepad) ? InputType.GP : InputType.KB;
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

	// void Update()
	// {
		
	// 	if(inputType == InputType.KB)
	// 	{
	// 		Keyboard kb = inputDevice as Keyboard;
	// 		Vector2 moveDirection = new Vector2(kb.aKey.isPressed)
	// 	}
	// 	else
	// 	{
	// 		Gamepad gp = inputDevice as Gamepad;
	// 		playerMovement.Move(gp.leftStick.ReadValue());
	// 	}
	// }

	

	bool IsMyInput(InputAction.CallbackContext ctx)
	{
		if(ctx.action.lastTriggerControl.device == inputDevice)
		{
			return true;
		}
		return false;
	}

	public void OnMovement(InputAction.CallbackContext ctx)
	{	
		if(!IsMyInput(ctx))
		{
			return;
		}
		if(DEBUGFLAGS.DEBUGMOVEMENT) Debug.Log(gameObject.name + " OM ");
		DoMovement(ctx);
	}

	void DoMovement(InputAction.CallbackContext ctx)
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
