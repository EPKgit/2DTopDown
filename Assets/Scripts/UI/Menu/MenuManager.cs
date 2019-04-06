using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Input;

public class MenuManager : Singleton<MenuManager>, IMenuActions
{
    public MasterControls controls;
	public static int maxPlayers = 4;

	public GameObject playerPrefab;
	public GameObject playerUIPrefab;

	public List<PlayerClass> classes;
	public List<PlayerMenuState> playerStatuses;


	private List<PlayerMenuUI> UIObjects;
	private Transform layoutGroup;
	private bool inMenu;

	#region INIT

	void OnEnable()
	{
		controls.Enable();
	}
	void OnDisable()
	{
		controls.Disable();
	}

	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
		controls.Menu.SetCallbacks(this);
		playerStatuses = new List<PlayerMenuState>();
		layoutGroup = GameObject.Find("Canvas").transform.Find("PlayerDisplayLayoutGroup");
		UIObjects = new List<PlayerMenuUI>();
		for(int x = 0; x < maxPlayers; ++x)
		{
			
			UIObjects.Add(Instantiate(playerUIPrefab, Vector3.zero, Quaternion.identity, layoutGroup).GetComponent<PlayerMenuUI>());
		}
		inMenu = true;
		UpdateUI();
	}

	#endregion

	void UpdateUI()
	{
		if(!inMenu)
		{
			return;
		}
		int x = 0;
		foreach(PlayerMenuState pms in playerStatuses)
		{
			UIObjects[x].UpdateUI(x + 1, pms, classes[pms.selectionIndex].name);			
			++x;
		}
		for(; x < maxPlayers; ++x)
		{
			UIObjects[x].HideSelf();
		}
	}
	/// <summary>
	/// Checks if the player is currently in a menu. The cases for this if the player does not exist or they
	/// are either just joined, or readied in the menu.
	/// </summary>
	/// <param name="i">The device of the player to check for</param>
	/// <returns>True if the player is in a menu (currently only the starting menu), false otherwise</returns>
	bool PlayerInMenu(InputDevice i)
	{
		int index = playerStatuses.FindIndex( (t) => i == t.device);
		if(index != -1)
		{
			PlayerMenuState pms = playerStatuses[index];
			if(pms.status == PlayerStatus.Joined || pms.status == PlayerStatus.Ready)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		return true;
	}

	#region  INPUTCALLBACKs

	/// <summary>
	/// Handles when the player pushes the Join button (space / {A}). If the player hasn't already joined
	/// they are given a handle, if they have they are updated to being ready. Should only be called by the
	/// input system.
	/// </summary>
	/// <param name="ctx">Callback Context for the input event</param>
	public void OnJoin(InputAction.CallbackContext ctx)
	{
		if(!PlayerInMenu(ctx.action.lastTriggerControl.device) || playerStatuses.Count >= maxPlayers)
		{
			return;
		}
		if(DEBUGFLAGS.MENU) Debug.Log("player joined?");
		int index = playerStatuses.FindIndex( (t) => ctx.action.lastTriggerControl.device == t.device);
		if(index  == -1)
		{
			playerStatuses.Add(new PlayerMenuState(ctx.action.lastTriggerControl.device, PlayerStatus.Joined, 0));
		}
		else
		{
			playerStatuses[index].status = PlayerStatus.Ready;
		}
		UpdateUI();
	}

	/// <summary>
	/// Handles when the player pushes the Back button (esc / {B}). If the player has already joined
	/// their handle is removed, if they were ready, they unready. Should only be called by the
	/// input system.
	/// </summary>
	/// <param name="ctx">Callback Context for the input event</param>
	public void OnBack(InputAction.CallbackContext ctx)
	{
		if(!PlayerInMenu(ctx.action.lastTriggerControl.device))
		{
			return;
		}
		if(DEBUGFLAGS.MENU) Debug.Log("player left?");
		int index = playerStatuses.FindIndex( (t) => ctx.action.lastTriggerControl.device == t.device);
		if(index != -1)
		{
			if(playerStatuses[index].status == PlayerStatus.Joined)
			{
				playerStatuses.RemoveAt(index);
			}
			else
			{
				playerStatuses[index].status = PlayerStatus.Joined;
			}
		}
		UpdateUI();
	}

	/// <summary>
	/// Moves to the gamescene. Should only be called by the input system.
	/// </summary>
	/// <param name="ctx">Callback Context for the input event</param>
	public void OnContinue(InputAction.CallbackContext ctx)
	{
		if(!PlayerInMenu(ctx.action.lastTriggerControl.device))
		{
			return;
		}
		if(DEBUGFLAGS.MENU) Debug.Log("OnContinue");
		GoToGameScene();
	}

	/// <summary>
	/// Handles left right movement in the menu (left stick / WASD / Dpad / Arrows).
	/// Used to select class currently, may later handle an inventory system.
	/// </summary>
	/// <param name="ctx">Callback Context for the input event</param>
	public void OnSelect(InputAction.CallbackContext ctx)
	{
		if(!PlayerInMenu(ctx.action.lastTriggerControl.device) || playerStatuses.Count == 0)
		{
			return;
		}
		int index = playerStatuses.FindIndex( (t) => ctx.action.lastTriggerControl.device == t.device);
		if(playerStatuses[index].status == PlayerStatus.Ready)
		{
			return;
		}
		float val = ctx.ReadValue<float>();
		if(DEBUGFLAGS.MENU) Debug.Log("OnSelect " + val);
		val = val > 0 ? 1 : -1;
		if(index != -1)
		{
			playerStatuses[index].selectionIndex += (int)val;
			playerStatuses[index].selectionIndex = (playerStatuses[index].selectionIndex + classes.Count) % classes.Count;
			UpdateUI();
		}
	}
	#endregion

	#region GAMESCENECHANGE

	/// <summary>
	/// Goes to the game scene. Checks if there are players present and that there are no players who
	/// haven't readied up. Calls spawn players once the other scene is loaded.
	/// </summary>
	public void GoToGameScene()
	{
		if(DEBUGFLAGS.MENU) Debug.Log(new System.Diagnostics.StackTrace());
		if(DEBUGFLAGS.MENU) Debug.Log("GoToGameScene?");
		if(playerStatuses.Count <= 0 || playerStatuses.Exists( (t) => t.status == PlayerStatus.Joined))
		{
			if(DEBUGFLAGS.MENU) Debug.Log("FailingToGoToGameScne");
			return;
		}
		SceneManager.sceneLoaded += SpawnPlayers;
		SceneManager.LoadScene("GameScene");
		inMenu = false;
	}

	void SpawnPlayers(Scene scene, LoadSceneMode lsm)
	{
		SceneManager.sceneLoaded -= SpawnPlayers;
		int x = 0;
		
		GameObject temp;
		PlayerInput pi;
		PlayerAbilities pa;
		StatBlock stats;
		foreach(PlayerMenuState pms in playerStatuses)
		{
			pms.status = PlayerStatus.InGame;
			temp = Instantiate(playerPrefab, new Vector3(++x, 0, 0), Quaternion.identity);
			pi = temp.GetComponent<PlayerInput>();
			pa = temp.GetComponent<PlayerAbilities>();
			stats = temp.GetComponent<StatBlock>();
			pi.inputDevice = pms.device;
			pi.playerID = x;
			pi.Initialize();
			pa.Initialize(classes[pms.selectionIndex].abilities);
			stats.Initialize(classes[pms.selectionIndex].stats);
		}
	}
	#endregion
}