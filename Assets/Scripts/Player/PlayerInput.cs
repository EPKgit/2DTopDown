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

	private InputType inputType;
	private Gamepad gamepad;
	private Keyboard keyboard;
	private Mouse mouse;

	private PlayerMovement playerMovement;
	private PlayerAttack playerAttack;

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
		playerAttack = GetComponent<PlayerAttack>();
		if(testing)
		{
			Initialize();
		}
	}

	public void Initialize()
	{
		GetPlayerID();
		gameObject.name = "Player " + playerID;
		if(testing)
		{
			inputDevice = InputSystem.GetDevice<Keyboard>();
			keyboard = inputDevice as Keyboard;
			mouse = InputSystem.GetDevice<Mouse>();
			inputType = InputType.KB;
			// inputDevice = InputSystem.GetDevice<Gamepad>();
			// gamepad = inputDevice as Gamepad;
			// inputType = InputType.GP;
		}
		else
		{
			if(inputDevice is Gamepad)
			{
				gamepad = inputDevice as Gamepad;
				inputType = InputType.GP;
			}
			else
			{
				keyboard = inputDevice as Keyboard;
				mouse = InputSystem.GetDevice<Mouse>();
				inputType = InputType.KB;
			}
			
		}
	}

	void GetPlayerID()
	{
		if(playerID != -1)
		{
			return;
		}
		int max = 0;
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
		if(ctx.action.lastTriggerControl.device == inputDevice || (inputType == InputType.KB && mouse == ctx.action.lastTriggerControl.device ))
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
		if(DEBUGFLAGS.DEBUGMOVEMENT) Debug.Log(gameObject.name + " MOVING ");
		DoMovement(ctx);
	}

	void DoMovement(InputAction.CallbackContext ctx)
	{
		if(inputType == InputType.GP)
		{
			playerMovement.Move(gamepad.leftStick.ReadValue());
		}
		else
		{
			playerMovement.Move(ctx.ReadValue<Vector2>());
		}
	}

	public void OnAttack(InputAction.CallbackContext ctx)
	{
		if(!IsMyInput(ctx))
		{
			return;
		}
		if(DEBUGFLAGS.DEBUGMOVEMENT) Debug.Log(gameObject.name + " ATTACKING ");
		DoAttack(ctx);
	}

	void DoAttack(InputAction.CallbackContext ctx)
	{
		if(inputType == InputType.GP)
		{
			playerAttack.Shoot(gamepad.rightStick.ReadValue());
		}
		else
		{
			Vector3 val = mouse.position.ReadValue();
			val.z = -CameraController.instance.transform.position.z;
			Vector3 worldSpacePosition = CameraController.instance.gameObject.GetComponent<Camera>().ScreenToWorldPoint(val);
			playerAttack.Shoot(worldSpacePosition - transform.position);
		}
	}
}
