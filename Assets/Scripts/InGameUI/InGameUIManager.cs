using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIManager : Singleton<InGameUIManager>
{
	public GameObject playerUIPrefab;

	private List<InGamePlayerUI> UIObjects;

	protected override void Awake()
	{
		for(int x = 0; x < MenuManager.maxPlayers; ++x)
		{

		}
	}
}
