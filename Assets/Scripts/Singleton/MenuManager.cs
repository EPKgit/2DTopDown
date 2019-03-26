using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Input;

public class MenuManager : Singleton<MenuManager>, IMenuActions
{
    public MasterControls controls;
	public int maxPlayers = 4;

	public GameObject playerPrefab;
	public GameObject playerUIPrefab;

	private List<InputDevice> inputDevices;
	private Transform layoutGroup;
	private bool inMenu;

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
		inputDevices = new List<InputDevice>();
		layoutGroup = GameObject.Find("Canvas").transform.Find("PlayerDisplayLayoutGroup");
		inMenu = true;
	}

	void UpdateUI()
	{
		if(!inMenu)
		{
			return;
		}
		foreach(Transform child in layoutGroup.transform)
		{
			Destroy(child.gameObject);
		}
		int x = 0;
		foreach(InputDevice i in inputDevices)
		{
			++x;
			GameObject temp = Instantiate(playerUIPrefab, Vector3.zero, Quaternion.identity, layoutGroup);
			temp.transform.Find("PlayerText").GetComponent<Text>().text = "Player " + x;
			temp.transform.Find("InputDeviceText").GetComponent<Text>().text = i.displayName;
			temp.GetComponent<Image>().color = Lib.GetPlayerColorByIndex(x);
		}
	}

	#region  INPUTCALLBACKs
	public void OnJoin(InputAction.CallbackContext ctx)
	{
		if(DEBUGFLAGS.MENU) Debug.Log("palyer joined?");
		if(!inputDevices.Contains(ctx.action.lastTriggerControl.device))
		{
			inputDevices.Add(ctx.action.lastTriggerControl.device);
			UpdateUI();
		}
	}

	public void OnBack(InputAction.CallbackContext ctx)
	{
		if(DEBUGFLAGS.MENU) Debug.Log("player left?");
		inputDevices.Remove(ctx.action.lastTriggerControl.device);
		UpdateUI();
	}

	public void OnContinue(InputAction.CallbackContext ctx)
	{
		GoToGameScene();
	}
	#endregion

	#region GAMESCENECHANGE
	public void GoToGameScene()
	{
		if(inputDevices.Count <= 0)
		{
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
		
		foreach(InputDevice i in inputDevices)
		{
			PlayerInput temp = Instantiate(playerPrefab, new Vector3(++x, 0, 0), Quaternion.identity).GetComponent<PlayerInput>();
			temp.inputDevice = i;
			temp.playerID = x;
			temp.Initialize();
		}
	}
	#endregion
}
