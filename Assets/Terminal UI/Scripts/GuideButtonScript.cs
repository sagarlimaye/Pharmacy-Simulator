using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public static GameObject guideProfilesPart2;
    public static GameObject guideProAddRxPanelScan;
    public static GameObject guideProAddRxPanel1;
    public static GameObject guideIntroS2Part2;
    public static GameObject guideProAddRxPanel2;
    public static GameObject guideAssembly1S2;
    public static GameObject guideAssemblyPanelS2;
    public static GameObject guideAssembly2S2;

    public static GameObject guideWrongClick;
    public static GameObject guideWrongClickInPanel;
    public static GameObject guideWrongClickInput;

    public AudioClip correctSound;
    public AudioClip popUpSound;
    public AudioClip wrongSound;
    public static GameObject yellowArrow;

    public void OnGuideClick()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    public static void OnWrongClick()
    {
        if (!guideWrongClick.activeInHierarchy)
        {
            CloseAllGuidePrompts();
            guideWrongClick.SetActive(true);
            guideWrongClick.GetComponent<Animator>().SetTrigger("Active");
        }
    }

    public static void OnWrongClickInPanel()
    {
        if (!guideWrongClickInPanel.activeInHierarchy)
        {
            CloseAllGuidePrompts();
            guideWrongClickInPanel.SetActive(true);
            guideWrongClickInPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Where are you going? Please fill in all the inputs correctly...";
            guideWrongClickInPanel.GetComponent<Animator>().SetTrigger("Active");
        }
    }

    public static void OnWrongClickInPanel(string text)
    {
        if (!guideWrongClickInPanel.activeInHierarchy)
        {
            CloseAllGuidePrompts();
            guideWrongClickInPanel.SetActive(true);
            guideWrongClickInPanel.GetComponentInChildren<TextMeshProUGUI>().text = text;
            guideWrongClickInPanel.GetComponent<Animator>().SetTrigger("Active");
        }
    }

    public static void OnWrongClickInput()
    {
        if (!guideWrongClickInput.activeInHierarchy)
        {
            CloseAllGuidePrompts();
            guideWrongClickInput.SetActive(true);
            guideWrongClickInput.GetComponent<Animator>().SetTrigger("Active");
        }
    }

    #region Scenario 1 Guide Prompts

    public void OnRxDataEntry()
    {
        if (SwitchPanelScript.DataEntryPanel.activeInHierarchy)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && SwitchPanelScript.dataEntryScreenIsEnabled)
            {
                CloseAllGuidePrompts();
                guideRxDataEntry.SetActive(true);
                SoundManager.instance.PlaySingle(popUpSound);
                anim = guideRxDataEntry.GetComponent<Animator>();
                anim.SetTrigger("Active");
                yellowArrow.SetActive(true);
                yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(161f, 89.1f, 0f);
            }
        }
    }

    public void OnDeAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One
            && EventSystem.current.currentSelectedGameObject.transform.parent == rxContent.transform.GetChild(0).GetChild(0))
        {
            SwitchPanelScript.dataEntryScreenIsEnabled = false;

            CloseAllGuidePrompts();
            guideDeAddRxPanel.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideDeAddRxPanel.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(false);
        }


    }

    public void OnDeAddRxOk()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && RxDataEntryScript.VerifyDeAddRxPanelInfoCorrect())
        {
            SwitchPanelScript.dataEntryScreenIsEnabled = false;

            CloseAllGuidePrompts();
            guideAssembly1S1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly1S1.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(161f, 89.1f, 0f);
        }


    }

    public void OnAssembleS1()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            CloseAllGuidePrompts();
            guideAssemblyPanelS1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssemblyPanelS1.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(false);
        }
    }

    public void OnAssemblePanelDoneS1()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && AssemblyScript.VerifyAssemblyPanelCheckedmarked())
        {
            CloseAllGuidePrompts();
            guideAssembly2S1.SetActive(true);
            anim = guideAssembly2S1.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-373.4f, 148.8f, 0f);
            yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -43.611f));
        }
    }
    #endregion

    #region Scenario 2 Guide Prompts
    public void OnProfiles()
    {
        if (SwitchPanelScript.ProfilesPanel.activeInHierarchy)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && SwitchPanelScript.profileScreenIsEnabled)
            { 
                CloseAllGuidePrompts();
                if (SwitchPanelScript.s2part1)
                {
                    guideProfiles.SetActive(true);
                    anim = guideProfiles.GetComponent<Animator>();
                    yellowArrow.SetActive(true);
                    yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(365.7f, 3f, 0f);
                    yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -135f));
                }
                else if (SwitchPanelScript.s2part2)
                {
                    guideProfilesPart2.SetActive(true);
                    anim = guideProfilesPart2.GetComponent<Animator>();
                    yellowArrow.SetActive(false);
                }
                SoundManager.instance.PlaySingle(popUpSound);
                anim.SetTrigger("Active");
            }
        }
    }

    public void OnProAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two
            && EventSystem.current.currentSelectedGameObject.transform.parent == profilesContent.transform.GetChild(3).GetChild(0))
        {
            SwitchPanelScript.profileScreenIsEnabled = false;

            CloseAllGuidePrompts();
            guideProAddRxPanelScan.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideProAddRxPanelScan.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(37.643f, 20.709f, 0f);
            yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 135f));
        }
    }

    public void OnProAddRxScanYes()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            CloseAllGuidePrompts();
            guideProAddRxPanel1.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideProAddRxPanel1.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-373.4f, 148.8f, 0f);
            yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -43.611f));
        }
    }

    public void OnProAddRxOk()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && SwitchPanelScript.s2part2 && AddRxScript.VerifyProAddRxPanelInfoCorrect())
        {
            SwitchPanelScript.profileScreenIsEnabled = false;

            CloseAllGuidePrompts();
            guideAssembly1S2.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssembly1S2.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(161f, 89.1f, 0f);
            yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 0f));
        } 
    }

    public void OnAssembleS2()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            CloseAllGuidePrompts();
            guideAssemblyPanelS2.SetActive(true);
            SoundManager.instance.PlaySingle(popUpSound);
            anim = guideAssemblyPanelS2.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(false);
        }
    }

    public void OnAssemblePanelDoneS2()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && AssemblyScript.VerifyAssemblyPanelCheckedmarked())
        {
            CloseAllGuidePrompts();
            guideAssembly2S2.SetActive(true);
            anim = guideAssembly2S2.GetComponent<Animator>();
            anim.SetTrigger("Active");
            yellowArrow.SetActive(true);
            yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-373.4f, 148.8f, 0f);
            yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, -43.611f));
        }
    }
    #endregion

    private void Awake()
    {
        guidePromptsPanel = GameObject.FindGameObjectWithTag("GuidePromptsPanel");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        guideWrongClick = GameObject.FindGameObjectWithTag("WrongClickPrompt");
        guideWrongClickInPanel = GameObject.FindGameObjectWithTag("WrongClickPromptInPanel");
        guideWrongClickInput = GameObject.FindGameObjectWithTag("WrongClickPromptInput");
        yellowArrow = GameObject.FindGameObjectWithTag("Yellow Arrow UI");
    }

    private void Start()
    {
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
        guideProAddRxPanelScan = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(7).gameObject;
        guideIntroS2Part2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(8).gameObject;
        guideProfilesPart2 = guidePromptsPanel.transform.GetChild(1).GetChild(0).GetChild(9).gameObject;

        CloseAllGuidePrompts();
        yellowArrow.SetActive(false);
    }

    public static void CloseAllGuidePrompts()
    {
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
        guideProAddRxPanelScan.SetActive(false);
        guideIntroS2Part2.SetActive(false);
        guideProfilesPart2.SetActive(false);

        guideWrongClick.SetActive(false);
        guideWrongClickInPanel.SetActive(false);
        guideWrongClickInput.SetActive(false);

    }

    private Animator anim;
}
