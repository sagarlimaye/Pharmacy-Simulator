using System.Collections;
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

    public static GameObject popUpSound;

    public AudioClip wrongSound;

    public static void TurnOnTerminal()
    {
        MainTerminalPanel.SetActive(true);

        var anim = GuideButtonScript.guideIntro.GetComponent<Animator>();
        anim.SetTrigger("Active");
        GuideButtonScript.guideIntro.SetActive(true);     
        popUpSound.GetComponent<AudioSource>().Play();

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            profileScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
        }
    }
    public static void TurnOffTerminal()
    {
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
        MainTerminalPanel.SetActive(false);
    }

    public void OnRx()
    {
        if (DataEntryPanel.activeInHierarchy == false)
        {
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && !dataEntryScreenIsEnabled)
            {
                SoundManager.instance.PlaySingle(wrongSound);
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

            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && !profileScreenIsEnabled)
            {
                SoundManager.instance.PlaySingle(wrongSound);
            }
            else
            {
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
            if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && !assemblyScreenIsEnabled)
            {
                SoundManager.instance.PlaySingle(wrongSound);
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
    }

    private void Start()
    {
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
    }
}
