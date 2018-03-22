using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanelScript : MonoBehaviour {

    //alll buttons go here, for all 3 screens
    public enum MainScreens { Rx, Profiles, Assembly, Off };
    public MainScreens currentScreen;

    public static GameObject RxPanel;
    public static GameObject ProfilesPanel;
    public static GameObject AssemblyPanel;

    private void Awake()
    {
        currentScreen = MainScreens.Off;
        RxPanel = GameObject.FindGameObjectWithTag("RxScreen");
        ProfilesPanel = GameObject.FindGameObjectWithTag("ProfilesScreen");
        //AssemblyPanel = GameObject.FindGameObjectWithTag("AssemblyScreen");
    }

    public void Update()
    {
        switch (currentScreen)
        {
            case MainScreens.Rx:

                RxPanel.SetActive(true);
                ProfilesPanel.SetActive(false);
                //AssemblyPanel.SetActive(false);
                break;

            case MainScreens.Profiles:
                RxPanel.SetActive(false);
                ProfilesPanel.SetActive(true);
                //AssemblyPanel.SetActive(false);
                break;

            case MainScreens.Assembly:
                RxPanel.SetActive(false);
                ProfilesPanel.SetActive(false);
                //AssemblyPanel.SetActive(true);
                break;
            case MainScreens.Off:
                RxPanel.SetActive(false);
                ProfilesPanel.SetActive(false);
                break;
        }
    }

    public void OnRx()
    {
        currentScreen = MainScreens.Rx;
    }
    public void OnProfile()
    {
        currentScreen = MainScreens.Profiles;
    }
    public void OnAssembly()
    {
        //currentScreen = MainScreens.Assembly;
    }
}
