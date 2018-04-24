﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPanelScript : MonoBehaviour
{
    public static GameObject MainTerminalPanel;
    public static GameObject DataEntryPanel;
    public static GameObject ProfilesPanel;
    public static GameObject AssemblyPanel;

    public static bool profileScreenIsEnabled = true;
    public static bool assemblyScreenIsEnabled = true;
    public static bool dataEntryScreenIsEnabled = true;
    public static bool terminalOffButtonEnabled = true;
    
    public delegate void TerminalEvent(GameObject terminal);
    public static event TerminalEvent TerminalClosed;

    public static GameObject popUpSound;
    public static GameObject wrongSound;
    public static GameObject powerOffBtn;
    public static GameObject rxDictionaryBtn;

    public static bool panelOpen = false;
    public static bool s2part1 = true;
    public static bool s2part2 = false;

    public static void TurnOnTerminal()
    {
        MainTerminalPanel.SetActive(true);
        powerOffBtn.SetActive(true);
        rxDictionaryBtn.SetActive(true);

        //var anim = GuideButtonScript.guideIntroS1.GetComponent<Animator>();
        //anim.SetTrigger("Active");

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            GuideButtonScript.guideIntroS1.SetActive(true);
            GuideButtonScript.yellowArrow.SetActive(true);
            GuideButtonScript.yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-366.0935f, 197.9f, 0f);
            popUpSound.GetComponent<AudioSource>().Play();
            profileScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
            terminalOffButtonEnabled = false;
        }
        else if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && s2part1)
        {
            GuideButtonScript.guideIntroS2.SetActive(true);
            GuideButtonScript.yellowArrow.SetActive(true);
            GuideButtonScript.yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-88.1f, 197.9f, 0f);
            popUpSound.GetComponent<AudioSource>().Play();
            dataEntryScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
            terminalOffButtonEnabled = false;
        }
        else if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && s2part2)
        {
            GuideButtonScript.guideIntroS2Part2.SetActive(true);
            GuideButtonScript.yellowArrow.SetActive(true);
            GuideButtonScript.yellowArrow.GetComponent<RectTransform>().localPosition = new Vector3(-88.1f, 197.9f, 0f);
            GuideButtonScript.yellowArrow.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 43.611f));
            popUpSound.GetComponent<AudioSource>().Play();
            dataEntryScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
            terminalOffButtonEnabled = false;
            profileScreenIsEnabled = true;
        }
    }

    public void OnPowerOff()
    {
        if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge && !terminalOffButtonEnabled)
        {
            wrongSound.GetComponent<AudioSource>().Play();

            if (panelOpen)
                GuideButtonScript.OnWrongClickInPanel();
            else
                GuideButtonScript.OnWrongClick();
        }
        else
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && s2part1)
            {
                s2part1 = false;
                s2part2 = true;
                panelOpen = false;
            }

            DataEntryPanel.SetActive(false);
            ProfilesPanel.SetActive(false);
            AssemblyPanel.SetActive(false);
            MainTerminalPanel.SetActive(false);
            powerOffBtn.SetActive(false);
            rxDictionaryBtn.SetActive(false);
            GuideButtonScript.yellowArrow.SetActive(false);
            GuideButtonScript.CloseAllGuidePrompts();

            if (TerminalClosed != null)
                TerminalClosed(gameObject);

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnRx()
    {
        if (DataEntryPanel.activeInHierarchy == false)
        {
            if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge && !dataEntryScreenIsEnabled)
            {
                wrongSound.GetComponent<AudioSource>().Play();

                if (panelOpen)
                    GuideButtonScript.OnWrongClickInPanel();
                else
                    GuideButtonScript.OnWrongClick();
            }
            else
            {
                DataEntryPanel.SetActive(true);
                ProfilesPanel.SetActive(false);
                AssemblyPanel.SetActive(false);
            }
        }
    }
    public void OnProfile()
    {
        if (ProfilesPanel.activeInHierarchy == false)
        {

            if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge && !profileScreenIsEnabled)
            {
                wrongSound.GetComponent<AudioSource>().Play();

                if (panelOpen)
                    GuideButtonScript.OnWrongClickInPanel();
                else
                    GuideButtonScript.OnWrongClick();
            }
            else
            {
                if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && s2part2)
                {
                    //set panelOpen to true because it will jump directly to the open Add Rx panel
                    panelOpen = true;
                    //change drug and signature for Scenario 2
                    AddRxScript.DrugAndGenericChanForS2();
                    //enable input fields
                    AddRxScript.EnableAddRxWhenImagePresent();
                }

                DataEntryPanel.SetActive(false);
                ProfilesPanel.SetActive(true);
                AssemblyPanel.SetActive(false);
            }
        }
    }
    public void OnAssembly()
    {
        if (AssemblyPanel.activeInHierarchy == false)
        {
            if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge && !assemblyScreenIsEnabled)
            {
                wrongSound.GetComponent<AudioSource>().Play();

                if (panelOpen)
                    GuideButtonScript.OnWrongClickInPanel();
                else
                    GuideButtonScript.OnWrongClick();
            }
            else
            {
                DataEntryPanel.SetActive(false);
                ProfilesPanel.SetActive(false);
                AssemblyPanel.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        MainTerminalPanel = GameObject.FindGameObjectWithTag("MainUIPanel");
        DataEntryPanel = GameObject.FindGameObjectWithTag("RxScreen");
        ProfilesPanel = GameObject.FindGameObjectWithTag("ProfilesScreen");
        AssemblyPanel = GameObject.FindGameObjectWithTag("AssemblyScreen");
        popUpSound = GameObject.FindGameObjectWithTag("PopUpSound");
        wrongSound = GameObject.FindGameObjectWithTag("WrongSound");
        powerOffBtn = GameObject.FindGameObjectWithTag("PowerOff");
        rxDictionaryBtn = GameObject.FindGameObjectWithTag("RxDictionary");
    }

    private void Start()
    {
        MainTerminalPanel.SetActive(false);
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
        powerOffBtn.SetActive(false);
        rxDictionaryBtn.SetActive(false);
    }
}
