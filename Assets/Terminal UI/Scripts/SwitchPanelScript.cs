using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanelScript : MonoBehaviour
{
    public enum Mode { Practice, Challenge };
    public Mode currentMode;

    public static GameObject DataEntryPanel;
    public static GameObject ProfilesPanel;
    public static GameObject AssemblyPanel;

    public void OnRx()
    {
        DataEntryPanel.SetActive(true);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
    }
    public void OnProfile()
    {
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(true);
        AssemblyPanel.SetActive(false);
    }
    public void OnAssembly()
    {
        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(true);
    }

    private void Awake()
    {
        DataEntryPanel = GameObject.FindGameObjectWithTag("RxScreen");
        ProfilesPanel = GameObject.FindGameObjectWithTag("ProfilesScreen");
        AssemblyPanel = GameObject.FindGameObjectWithTag("AssemblyScreen");
    }

    private void Start()
    {
        currentMode = Mode.Practice;
        if(currentMode == Mode.Practice)
        {

        }

        DataEntryPanel.SetActive(false);
        ProfilesPanel.SetActive(false);
        AssemblyPanel.SetActive(false);
    }
}
