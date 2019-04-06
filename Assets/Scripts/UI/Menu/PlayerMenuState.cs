using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public enum PlayerStatus { Joined, Ready, InGame }

public class PlayerMenuState
{
	public InputDevice device;
	public PlayerStatus status;
	public int selectionIndex;
	public PlayerMenuState(InputDevice i, PlayerStatus p, int ci)
	{
		device = i;
		status = p;
		selectionIndex = ci;
	}
}
