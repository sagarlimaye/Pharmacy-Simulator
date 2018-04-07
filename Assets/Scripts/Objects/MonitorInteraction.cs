using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
	public Text notification;
	public GameObject MainTerminalPanel, guideIntro;

	private float distance;
	bool available = true;
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
		guideIntro.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		if ( available && distance < 3 && Input.GetKeyDown(KeyCode.E)) {
			if(MainTerminalPanel.activeInHierarchy)
			{
				SwitchPanelScript.DataEntryPanel.SetActive(false);
				SwitchPanelScript.ProfilesPanel.SetActive(false);
				SwitchPanelScript.AssemblyPanel.SetActive(false);
				MainTerminalPanel.SetActive(false);
				guideIntro.SetActive(false);
			}
			else
			{
				MainTerminalPanel.SetActive(true);
				guideIntro.SetActive(true);
			}
			Cursor.lockState = (MainTerminalPanel.activeInHierarchy) ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}
}
