using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GuideButtonScript : MonoBehaviour
{
    public static GameObject guidePromptsPanel;
    public static GameObject rxContent;
    public static GameObject profilesContent;

    public static GameObject guideIntroS1;
    public static GameObject guideRxDataEntry;
    public static GameObject guideDeAddRxPanel;
    public static GameObject guideAssembly1S1;
    public static GameObject guideAssemblyPanelS1;
    public static GameObject guideAssembly2S1;

    public static GameObject guideIntroS2;
    public static GameObject guideProfiles;
    public static GameObject guideProAddRxPanel1;
    public static GameObject guideProAddRxPanel2;
    public static GameObject guideAssembly1S2;
    public static GameObject guideAssemblyPanelS2;
    public static GameObject guideAssembly2S2;

    public AudioClip correctSound;
    public AudioClip popUpSound;
    public AudioClip wrongSound;

    public void OnGuideClick()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    #region Scenario 1 Guide Prompts

    public void OnRxDataEntry()
    {
        if (SwitchPanelScript.DataEntryPanel.activeInHierarchy)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && SwitchPanelScript.dataEntryScreenIsEnabled)
            {
                guideIntroS1.SetActive(false);
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
            guideAssembly1S1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly1S1.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssembleS1()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            guideAssembly1S1.SetActive(false);
            guideAssemblyPanelS1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssemblyPanelS1.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssemblePanelDoneS1()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            guideAssemblyPanelS1.SetActive(false);
            guideAssembly2S1.SetActive(true);
            //SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly2S1.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }
    #endregion

    public void OnProfiles()
    {
        if (SwitchPanelScript.ProfilesPanel.activeInHierarchy)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && SwitchPanelScript.profileScreenIsEnabled)
            {
                guideIntroS2.SetActive(false);
                guideProfiles.SetActive(true);
                SoundManager.instance.PlaySingle(popUpSound);
                anim = guideProfiles.GetComponent<Animator>();
                anim.SetTrigger("Active");
            }
        }
    }

    public void OnProAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
            //&& EventSystem.current.currentSelectedGameObject.transform.parent == profilesContent.transform.GetChild(0).GetChild(0))
        {
            guideProfiles.SetActive(false);
            guideProAddRxPanel1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideProAddRxPanel1.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }


    public void OnProAddRxOk()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)//RxDataEntryScript.VerifyAddRxPanelInfoCorrect())
        {
            SwitchPanelScript.profileScreenIsEnabled = false;

            guideProAddRxPanel2.SetActive(false);
            guideAssembly1S2.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly1S2.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssembleS2()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            guideAssembly1S2.SetActive(false);
            guideAssemblyPanelS2.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssemblyPanelS2.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }

    public void OnAssemblePanelDoneS2()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            guideAssemblyPanelS2.SetActive(false);
            guideAssembly2S2.SetActive(true);
            //SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly2S2.GetComponent<Animator>();
            anim.SetTrigger("Active");
        }
    }


    private void Awake()
    {
        guidePromptsPanel = GameObject.FindGameObjectWithTag("GuidePromptsPanel");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
    }

    private void Start()
    {
        //Intro prompt is showing when terminal is opened, it closes when player click on "Rx Data Entry"


        guideIntroS1 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        guideRxDataEntry = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        guideDeAddRxPanel = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        guideAssembly1S1 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
        guideAssemblyPanelS1 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(4).gameObject;
        guideAssembly2S1 = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(5).gameObject;

        guideIntroS2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        guideProfiles = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(1).gameObject;
        guideProAddRxPanel1 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(2).gameObject;
        guideProAddRxPanel2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(3).gameObject;
        guideAssembly1S2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(4).gameObject;
        guideAssemblyPanelS2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(5).gameObject;
        guideAssembly2S2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(6).gameObject;

        guideIntroS1.SetActive(false);
        guideRxDataEntry.SetActive(false);
        guideDeAddRxPanel.SetActive(false);
        guideAssembly1S1.SetActive(false);
        guideAssemblyPanelS1.SetActive(false);
        guideAssembly2S1.SetActive(false);

        guideIntroS2.SetActive(false);
        guideProfiles.SetActive(false);
        guideProAddRxPanel1.SetActive(false);
        guideProAddRxPanel2.SetActive(false);
        guideAssembly1S2.SetActive(false);
        guideAssemblyPanelS2.SetActive(false);
        guideAssembly2S2.SetActive(false);

    }

    private Animator anim;
}
