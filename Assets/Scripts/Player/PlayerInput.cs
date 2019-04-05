using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public enum InputType { KB, GP }

public class PlayerInput : MonoBehaviour, IGameplayActions
{
	public static List<PlayerInput> all = new List<PlayerInput>();

	public bool testingController = false;
	public bool testingMouseAndKeyboard = false;

    public MasterControls controls;
	public InputDevice inputDevice;
	public int playerID = -1;

	private InputType inputType;
	private Gamepad gamepad;
	private Keyboard keyboard;
	private Mouse mouse;

	private PlayerMovement playerMovement;
	private PlayerAttack playerAttack;
	private PlayerAbilities playerAbilities;

	#region INIT

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
		//controls.MakePrivateCopyOfActions();
		controls.Gameplay.SetCallbacks(this);
		playerMovement = GetComponent<PlayerMovement>();
		playerAbilities = GetComponent<PlayerAbilities>();
		if(testingController || testingMouseAndKeyboard)
		{
			if(testingController && testingMouseAndKeyboard)
			{
				throw new System.InvalidOperationException("Cannot test both mouse+KB and controller on " + gameObject.name);
			}
			Initialize();
		}
	}

	public void Initialize()
	{
		if(playerID == -1)
		{
			GetPlayerID();
		}
		gameObject.name = "Player " + playerID;
		if(testingController)
		{
			inputDevice = InputSystem.GetDevice<Gamepad>();
			gamepad = inputDevice as Gamepad;
			inputType = InputType.GP;
		}
		else if(testingMouseAndKeyboard)
		{
			inputDevice = InputSystem.GetDevice<Keyboard>();
			keyboard = inputDevice as Keyboard;
			mouse = InputSystem.GetDevice<Mouse>();
			inputType = InputType.KB;
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
		InGameUIManager.instance?.RegisterPlayer(gameObject);
	}

	void GetPlayerID()
	{
		int max = 0;
		for(int x = 0; x < all.Count; ++x)
		{
			max = Mathf.Max(max, all[x].playerID);
		}
		playerID = max + 1;
	}

	#endregion

	bool IsMyInput(InputAction.CallbackContext ctx)
	{
		if(ctx.action.lastTriggerControl.device == inputDevice || (inputType == InputType.KB && mouse == ctx.action.lastTriggerControl.device ))
		{
			return true;
		}
		return false;
	}

	#region MOVEMENT

	public void OnMovement(InputAction.CallbackContext ctx)
	{	
		if(!IsMyInput(ctx))
		{
			return;
		}
		if(DEBUGFLAGS.MOVEMENT) Debug.Log(gameObject.name + " MOVING ");
		DoMovement(ctx);
	}

	void DoMovement(InputAction.CallbackContext ctx)
	{
		playerMovement.Move(Lib.GetInputDirection(gamepad, mouse, ctx, inputType, gameObject, true));
	}

	#endregion

	#region ATTACK

	public void OnAttack(InputAction.CallbackContext ctx)
	{
		if(!IsMyInput(ctx))
		{
			return;
		}
		DoAttack(ctx);
	}

	void DoAttack(InputAction.CallbackContext ctx)
	{
		playerAbilities.Attack(ctx, Lib.GetInputDirection(gamepad, mouse, ctx, inputType, gameObject));
	}

	#endregion

	#region ABILITIES

	public void OnAbility1(InputAction.CallbackContext ctx)
	{
		if(!IsMyInput(ctx))
		{
			return;
		}
		playerAbilities.Ability1(ctx, Lib.GetInputDirection(gamepad, mouse, ctx, inputType, gameObject));
	}
	public void OnAbility2(InputAction.CallbackContext ctx)
	{
		if(!IsMyInput(ctx))
		{
			return;
		}
		playerAbilities.Ability2(ctx, Lib.GetInputDirection(gamepad, mouse, ctx, inputType, gameObject));
	}
	public void OnAbility3(InputAction.CallbackContext ctx)
	{
		if(!IsMyInput(ctx))
		{
			return;
		}
		playerAbilities.Ability3(ctx, Lib.GetInputDirection(gamepad, mouse, ctx, inputType, gameObject));
	}

	#endregion
}
