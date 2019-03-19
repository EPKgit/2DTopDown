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

	void OnEnable()
	{
		controls.Enable();
	}
	void OnDisable()
	{
		controls.Disable();
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		controls.Menu.SetCallbacks(this);
		inputDevices = new List<InputDevice>();
		layoutGroup = GameObject.Find("Canvas").transform.Find("PlayerDisplayLayoutGroup");
	}

	void UpdateUI()
	{
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
			switch(x)
			{
				case 1:
					temp.GetComponent<Image>().color = Color.red;
					break;
				case 2:
					temp.GetComponent<Image>().color = Color.green;
					break;
				case 3:
					temp.GetComponent<Image>().color = Color.blue;
					break;
				case 4:
					temp.GetComponent<Image>().color = Color.yellow;
					break;
				default:
					temp.GetComponent<Image>().color = Color.black;
					break;

			}
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
