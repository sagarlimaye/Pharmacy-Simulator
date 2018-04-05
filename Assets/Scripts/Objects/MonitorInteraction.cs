using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
	public Text notification;
	public GameObject MainTerminalPanel;

	public SwitchPanelScript switcher;
	private float distance;
	bool available;
	void OnEnable()
	{
		DialogueController.DialogStarted += OnDialogStarted;
		DialogueController.DialogCompleted += OnDialogCompleted;
	}

	void OnDisable()
	{
		DialogueController.DialogStarted -= OnDialogStarted;
		DialogueController.DialogCompleted -= OnDialogCompleted;
	}

	void OnDialogStarted(GameObject d)
	{
		available = false;
	}
	void OnDialogCompleted(GameObject d)
	{
		available = true;
	}

	void Awake()
	{
		MainTerminalPanel.SetActive(false);
		switcher=FindObjectOfType<SwitchPanelScript>();
	}
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		if ( available && distance < 3 && Input.GetKeyDown(KeyCode.E)) {
			if(switcher.currentScreen == SwitchPanelScript.MainScreens.Off)
			{
				MainTerminalPanel.SetActive(true);
				switcher.currentScreen = SwitchPanelScript.MainScreens.Rx;
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				MainTerminalPanel.SetActive(false);
				switcher.currentScreen = SwitchPanelScript.MainScreens.Off;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}
}
