using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScenarioInfoScript : MonoBehaviour
{
    public enum Scenario { One, Two, Three, Challenge };
    public static Scenario currentScenario;

    public enum Mode { Practice, Challenge };
    
    public Mode currentMode;

    public static GameObject rxScreen;
    public static GameObject profileScreen;
    public static GameObject assemblyScreen;
    public static GameObject rxContent;
    public static GameObject profilesContent;
    public static GameObject assemblyContent;

    public static string scenarioPatientFullName;
    public static string scenarioPatientFirst;
    public static string scenarioPatientLast;
    public static string scenarioPatientDrug;
    public static string scenarioPatientDob;
    public static string scenarioPatientDrugPrice;

    public AudioClip backgroundNoise;
    public AudioClip backgroundJazz;
    public UnityEvent onScenarioInfoReady;
    public delegate void ScenarioInfoEvent(ScenarioInfoScript scenarioInfo);
    public static event ScenarioInfoEvent ScenarioInfoReady;


    private void Start()
    {
        SoundManager.instance.PlayMusic(backgroundNoise);
        SoundManager.instance.PlayMusic2(backgroundJazz);
    }
    void OnEnable()
    {
        RxDataEntryScript.RxEntriesPopulated += OnRxEntriesPopulated;
        CustomerDestroyer.CustomerDestroyed += OnCustomerDestroyed;
    }

    void OnDisable()
    {
        RxDataEntryScript.RxEntriesPopulated -= OnRxEntriesPopulated;
        CustomerDestroyer.CustomerDestroyed -= OnCustomerDestroyed;
    }

    private void OnRxEntriesPopulated(GameObject rxContent)
    {
        OnAddRx();
    }

    void OnCustomerDestroyed()
    {
        OnAddRx();
    }
    public void OnAddRx()
    {
         if (currentScenario == Scenario.One && firstAccess)
        {
            //The first entry on the "Rx Data Entry" panel will always be the subject patient for scenario 1

            GameObject rxEntry = rxContent.transform.GetChild(0).gameObject;

            scenarioPatientFullName = rxEntry.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text;
            scenarioPatientFirst = scenarioPatientFullName.Split(' ')[0];
            scenarioPatientLast = scenarioPatientFullName.Split(' ')[1];
            scenarioPatientDrug = rxEntry.transform.GetChild(0).GetChild(2).GetComponentInChildren<Text>().text;

            for (int i = 0; i < profilesContent.transform.childCount; i++)
            {
                string profileEntryFullName = profilesContent.transform.GetChild(i).GetChild(0).GetChild(0).GetComponentInChildren<Text>().text +
                                                " " + profilesContent.transform.GetChild(i).GetChild(0).GetChild(1).GetComponentInChildren<Text>().text;

                if (profileEntryFullName == scenarioPatientFullName)
                {
                    scenarioPatientDob = profilesContent.transform.GetChild(i).GetChild(0).GetChild(2).GetComponentInChildren<Text>().text;
                    firstAccess = false;
                    break;
                }
            }
        }
        if (ScenarioInfoReady != null)
            ScenarioInfoReady(this);
        onScenarioInfoReady.Invoke();
    }
    public void GetPatientInfoS2()
    {
        if (currentScenario == Scenario.Two)
        {
            GameObject patientProfile = profilesContent.transform.GetChild(3).GetChild(0).gameObject;
            scenarioPatientFirst = patientProfile.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
            scenarioPatientLast = patientProfile.transform.GetChild(1).GetChild(0).GetComponent<Text>().text;
            scenarioPatientFullName = scenarioPatientFirst + " " + scenarioPatientLast;
            scenarioPatientDob = patientProfile.transform.GetChild(2).GetChild(0).GetComponent<Text>().text;
            onScenarioInfoReady.Invoke();
        }
    }
    public void OnOk()
    {
        GameObject assemblyPanel = assemblyScreen.transform.GetChild(1).gameObject;
        scenarioPatientDrugPrice = assemblyPanel.transform.GetChild(5).GetComponentInChildren<Text>().text;
    }

    private void Awake()
    {
        currentScenario = Scenario.One;

        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        
    }   
    private static bool firstAccess = true;
}
