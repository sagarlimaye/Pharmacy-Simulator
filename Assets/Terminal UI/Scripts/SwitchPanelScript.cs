using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanelScript : MonoBehaviour
{
    public static GameObject RxPanel;
    public static GameObject ProfilesPanel;

    public void OnRx()
    {
        RxPanel.SetActive(true);
        ProfilesPanel.SetActive(false);
    }
    public void OnProfile()
    {
        RxPanel.SetActive(false);
        ProfilesPanel.SetActive(true);
    }

    private void Awake()
    {
        RxPanel = GameObject.FindGameObjectWithTag("RxScreen");
        ProfilesPanel = GameObject.FindGameObjectWithTag("ProfilesScreen");
    }
}
