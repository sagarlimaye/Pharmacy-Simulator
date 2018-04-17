using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuideButtonScript : MonoBehaviour
{
    public static GameObject guidePromptsPanel;
    public static GameObject rxContent;

    public static GameObject guideIntro;
    public static GameObject guideRxDataEntry;
    public static GameObject guideDeAddRxPanel;
    public static GameObject guideAssembly1;
    public static GameObject guideAssemblyPanel;
    public static GameObject guideAssembly2;

    public AudioClip correctSound;
    public AudioClip popUpSound;
    public AudioClip wrongSound;

    public void OnGuideClick()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    public void OnRxDataEntry()
    {
        if (SwitchPanelScript.DataEntryPanel.activeInHierarchy)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && SwitchPanelScript.dataEntryScreenIsEnabled)
            {
                guideIntro.SetActive(false);
                guideRxDataEntry.SetActive(true);
                SoundManager.instance.PlaySingle(popUpSound);
                anim = guideRxDataEntry.GetComponent<Animator>();
                anim.SetTrigger("Active");
            }
        }
    }

    public void OnDeAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One
            && EventSystem.current.currentSelectedGameObject.transform.parent == rxContent.transform.GetChild(0).GetChild(0))
        {
            guideRxDataEntry.SetActive(false);
            guideDeAddRxPanel.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideDeAddRxPanel.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnDeAddRxOk()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && RxDataEntryScript.VerifyAddRxPanelInfoCorrect())
        {
            SwitchPanelScript.dataEntryScreenIsEnabled = false;

            guideDeAddRxPanel.SetActive(false);
            guideAssembly1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly1.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssemble()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            guideAssembly1.SetActive(false);
            guideAssemblyPanel.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssemblyPanel.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssemblePanelDone()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            guideAssemblyPanel.SetActive(false);
            guideAssembly2.SetActive(true);
            //SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly2.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    private void Awake()
    {
        guidePromptsPanel = GameObject.FindGameObjectWithTag("GuidePromptsPanel");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
    }

    private void Start()
    {
        //Intro prompt is showing when terminal is opened, it closes when player click on "Rx Data Entry"


        guideIntro = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        guideRxDataEntry = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        guideDeAddRxPanel = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        guideAssembly1 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        guideAssemblyPanel = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(4).gameObject;
        guideAssembly2 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(5).gameObject;

        guideIntro.SetActive(false);
        guideRxDataEntry.SetActive(false);
        guideDeAddRxPanel.SetActive(false);
        guideAssembly1.SetActive(false);
        guideAssemblyPanel.SetActive(false);
        guideAssembly2.SetActive(false);

    }

    private Animator anim;
}
