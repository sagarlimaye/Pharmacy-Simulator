using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioInfoScript : MonoBehaviour
{
    public enum Scenario { One, Two, Three, Off };
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

    public delegate void ScenarioInfoEvent(ScenarioInfoScript scenarioInfo);
    public static event ScenarioInfoEvent ScenarioInfoReady;

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
    }

    public void OnOk()
    {
        GameObject assemblyPanel = assemblyScreen.transform.GetChild(1).gameObject;
        scenarioPatientDrugPrice = assemblyPanel.transform.GetChild(5).GetComponentInChildren<Text>().text;
    }

    private void Awake()
    {
        currentScenario = Scenario.Two;

        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        
    }   
    private static bool firstAccess = true;
}
