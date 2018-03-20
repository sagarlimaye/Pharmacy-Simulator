using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
	public Text notification;
	public GameObject MainTerminalPanel;

	public SwitchPanelScript switcher;
	private float distance;
	
	void Awake()
	{
		switcher=FindObjectOfType<SwitchPanelScript>();
	}
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		if (distance < 3 && Input.GetKeyDown(KeyCode.E)) {
			switcher.currentScreen = SwitchPanelScript.MainScreens.Rx;
		}
	}
}
