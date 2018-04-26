using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPrompts : MonoBehaviour {

    public static GameObject arrow;
    public static GameObject arrowParent;
    public TextMeshProUGUI currentTextMesh;
    public Image currentAvatar;
    public TextMeshProUGUI promptGetBottle, promptGetMed, promptFillBottle, promptPlaceInFileCabinet, 
        promptCustomerArrives, PromptDialog1, PromptDialog2, PromptGetRx, PromptFinished;

    public static Image avatarNormal;
    public static Image avatarWorried;
    public static Image avatarHorrified;

    public Animator anim2;

    private void Awake()
    {
        arrow = GameObject.FindGameObjectWithTag("YellowArrow");
        arrowParent = GameObject.FindGameObjectWithTag("ArrowParent");
    }


    void Start ()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(true);
            arrowParent.GetComponent<Animator>().SetTrigger("On");
        }
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
	}
    private void OnEnable()
    {
        MonitorInteraction.TerminalOpened += OnTerminalOpened;
        SwitchPanelScript.TerminalClosed += OnTerminalClosed;
        PickupObject.PickedUpObject += OnPickedUpObject;
        PillBoxController.TrayFilledFromPillBox += OnTrayFilledFromPillBox;
        FilledTrayController.PrescriptionFilled += OnPrescriptionFilled;
        BottleHolder.BottlePlaced += OnBottlePlaced;
        DialogueController.DialogDisplayed += OnDialogDisplayed;
        DialogueController.DialogCompleted += OnDialogCompleted;
    }


    private void OnDisable()
    {
        MonitorInteraction.TerminalOpened -= OnTerminalOpened;
        SwitchPanelScript.TerminalClosed -= OnTerminalClosed;
        PickupObject.PickedUpObject -= OnPickedUpObject;
        PillBoxController.TrayFilledFromPillBox -= OnTrayFilledFromPillBox;
        FilledTrayController.PrescriptionFilled -= OnPrescriptionFilled;
        BottleHolder.BottlePlaced -= OnBottlePlaced;
        DialogueController.DialogDisplayed -= OnDialogDisplayed;
        DialogueController.DialogCompleted -= OnDialogCompleted;
    }

    private void OnDialogCompleted(GameObject d)
    {
        if (d.tag == "PickupPrescriptionDialog")
        {
            anim.SetBool("Active", true);
            currentTextMesh.text = PromptDialog2.text;
        }
        else if (d.tag == "PrescriptionReadyDialog")
        {
            anim.SetBool("Active", true);
            currentTextMesh.text = PromptFinished.text;
            DialogueController.DialogCompleted -= OnDialogCompleted;
        }
    }

    private void OnDialogDisplayed(GameObject d)
    {
        var dialog = d.GetComponent<Dialog>();
        if (dialog is CustomerDialog && dialog.text.Contains("Pickup"))
        {
            anim.SetBool("Active", true);
            currentTextMesh.text = PromptDialog1.text;
            DialogueController.DialogDisplayed -= OnDialogDisplayed;
        }
    }

    private void OnBottlePlaced(BottleHolder sender, GameObject bottle)
    {
        if(sender.tag == "FileCabinetAnchor")
        {
            anim.SetBool("Active", true);
            currentTextMesh.text = promptCustomerArrives.text;
            BottleHolder.BottlePlaced -= OnBottlePlaced;
        }
    }

    private void OnPrescriptionFilled(FilledTrayController sender)
    {
        anim.SetBool("Active", true);
        currentTextMesh.text = promptPlaceInFileCabinet.text;
        FilledTrayController.PrescriptionFilled -= OnPrescriptionFilled;
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(-0.795f, 2.376f, -2.5f);
            arrow.transform.localEulerAngles = new Vector3(-90f, 0f, 90f);
        }
    }

    private void OnTrayFilledFromPillBox(PillBoxController sender)
    {
        anim.SetBool("Active", true);
        currentTextMesh.text = promptFillBottle.text;
        PillBoxController.TrayFilledFromPillBox -= OnTrayFilledFromPillBox;
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(2.427f, 1.529f, -8.5f);
            arrow.transform.localEulerAngles = new Vector3(-90f, 90f, 90f);
        }
    }

    private void OnPickedUpObject(GameObject obj)
    {
        if (obj.tag == "Prescription/Empty")
        {
            anim.SetBool("Active", true);
            currentTextMesh.text = promptGetMed.text;
        }
        PickupObject.PickedUpObject -= OnPickedUpObject;

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(-3.82f, 2.55f, -7.86f);
            arrow.transform.localEulerAngles = new Vector3(-145f, 90f, 0f);
        }
    }

    private void OnTerminalClosed(GameObject terminal)
    {
        currentTextMesh.enabled = true;
        button.enabled = true;
        image.enabled = true;
        currentAvatar.enabled = true;
        anim.SetBool("Active", true);
        currentTextMesh.text = promptGetBottle.text;
        SwitchPanelScript.TerminalClosed -= OnTerminalClosed;

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(true);
            arrow.transform.localPosition = new Vector3(1.62f, 0.82f, -8.5f);
            arrow.transform.localEulerAngles = new Vector3(-145f, 90f, 0f);
        }
    }

    private void OnTerminalOpened(GameObject terminal)
    {
        button.enabled = false;
        image.enabled = false;
        currentTextMesh.enabled = false;
        currentAvatar.enabled = false;
        MonitorInteraction.TerminalOpened -= OnTerminalOpened;
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            arrow.SetActive(false);
        }
    }

    public void ShowPrompt(string prompt)
    {
        currentTextMesh.text = prompt;
    }

    private Button button;
    private Image image;
    private Animator anim;
}
