using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	public Text playerName;
	public Text playerDevice;
	public Image readyImage;
	public Text classText;
	public Image background;

	private GameObject UIActive;

	void Awake()
	{
		UIActive = transform.Find("UI").gameObject;
		playerName = transform.GetChild(0).Find("PlayerText").GetComponent<Text>();
		playerDevice = transform.GetChild(0).Find("InputDeviceText").GetComponent<Text>();
		readyImage = transform.GetChild(0).Find("ReadyImage").GetComponent<Image>();
		classText = transform.GetChild(0).Find("ClassSelection").transform.Find("ClassSelectionText").GetComponent<Text>();
		background = GetComponent<Image>();
	}

	public void UpdateUI(int index, PlayerMenuState pms, string className)
	{
		UIActive.SetActive(true);
		background.enabled = true;
		playerName.text = "Player " + index;
		playerDevice.text = pms.device.displayName;
		readyImage.color = Lib.GetColorByMenuState(pms.status);
		classText.text = className;
		background.color = Lib.GetPlayerColorByIndex(index);
	}

	public void HideSelf()
	{
		UIActive.SetActive(false);
		background.enabled = false;
	}
}
