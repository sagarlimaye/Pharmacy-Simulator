using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
    public Text notification;
    public GameObject MainTerminalPanel;

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
		MainTerminalPanel.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

        if (available && distance < 3 && Input.GetKeyDown(KeyCode.E) && PlayerLook.hitObject == gameObject) {
			if(MainTerminalPanel.activeInHierarchy)
			{
				SwitchPanelScript.DataEntryPanel.SetActive(false);
				SwitchPanelScript.ProfilesPanel.SetActive(false);
				SwitchPanelScript.AssemblyPanel.SetActive(false);
				MainTerminalPanel.SetActive(false);
				GuideButtonScript.guideIntro.SetActive(false);
                GuideButtonScript.guideAssembly2.SetActive(false);
                if (TerminalClosed != null)
                    TerminalClosed(gameObject);
			}
			else
			{
				MainTerminalPanel.SetActive(true);
                var anim = GuideButtonScript.guideIntro.GetComponent<Animator>();
                anim.SetTrigger("Active");
                GuideButtonScript.guideIntro.SetActive(true);
                SoundManager.instance.PlaySingle(GuideButtonScript.popUpSound);
                if (TerminalOpened != null)
                    TerminalOpened(gameObject);
            }
			Cursor.lockState = (MainTerminalPanel.activeInHierarchy) ? CursorLockMode.None : CursorLockMode.Locked;
		}
	}
}
