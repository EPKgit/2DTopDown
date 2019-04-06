using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
	public GameObject playerUIPrefab;

	private List<InGamePlayerUI> UIObjects;
	private Transform layoutGroup;

	protected override void Awake()
	{
		UIObjects = new List<InGamePlayerUI>();
		layoutGroup = GameObject.Find("Canvas").transform.Find("PlayerUI");
		for(int x = 0; x < MenuManager.maxPlayers; ++x)
		{
			UIObjects.Add(Instantiate(playerUIPrefab, Vector3.zero, Quaternion.identity, layoutGroup).GetComponent<InGamePlayerUI>());
		}
	}

	void Start()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		int x;
		for(x = 0; x < PlayerInput.all.Count; ++x)
		{
			UIObjects[x].Initialize(PlayerInput.all[x].gameObject);
		}
		for(; x < MenuManager.maxPlayers; ++x)
		{
			UIObjects[x].HideSelf();
		}
	}
}
