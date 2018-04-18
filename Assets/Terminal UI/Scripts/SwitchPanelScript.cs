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
    public static GameObject powerOffBtn;
    public static GameObject rxDictionaryBtn;

    public AudioClip wrongSound;

    public static void TurnOnTerminal()
    {
        MainTerminalPanel.SetActive(true);
        powerOffBtn.SetActive(true);
        rxDictionaryBtn.SetActive(true);

        var anim = GuideButtonScript.guideIntroS1.GetComponent<Animator>();
        anim.SetTrigger("Active");
        popUpSound.GetComponent<AudioSource>().Play();

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            GuideButtonScript.guideIntroS1.SetActive(true);
            profileScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
        }
        else if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            GuideButtonScript.guideIntroS2.SetActive(true);
            dataEntryScreenIsEnabled = false;
            assemblyScreenIsEnabled = false;
        }
    }
    public void TurnOffTerminal()
    {
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
        MainTerminalPanel.SetActive(false);
        powerOffBtn.SetActive(false);
        rxDictionaryBtn.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
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
