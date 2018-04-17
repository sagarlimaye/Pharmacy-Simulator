using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
    public Text notification;
    public static GameObject MainTerminalPanel;
    public static GameObject profileScreen;
    public static GameObject assemblyScreen;

    private float distance;
    bool available = true;

    public delegate void TerminalEvent(GameObject terminal);
    public static event TerminalEvent TerminalOpened;
    public static event TerminalEvent TerminalClosed;

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
        MainTerminalPanel = GameObject.FindGameObjectWithTag("MainUIPanel");
	}

    private void Start()
    {
        MainTerminalPanel.SetActive(false);
    }

    void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

        if (available && distance < 3 && Input.GetKeyDown(KeyCode.E) && PlayerLook.hitObject == gameObject) {
			if(MainTerminalPanel.activeInHierarchy)
			{
                SwitchPanelScript.TurnOffTerminal();
                if (TerminalClosed != null)
                    TerminalClosed(gameObject);
			}
			else
			{
                SwitchPanelScript.TurnOnTerminal();
                if (TerminalOpened != null)
                    TerminalOpened(gameObject);
            }
			Cursor.lockState = (MainTerminalPanel.activeInHierarchy) ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}
}
