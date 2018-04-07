using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideButtonScript : MonoBehaviour
{
    public static GameObject guidePromptsPanel;

    public static GameObject guideIntro;
    public static GameObject guideRxDataEntry;
    public static GameObject guideDeAddRx;

    public void OnGuideClick()
    {
        EventSystem.current.currentSelectedGameObject.SetActive(false);
    }

    public void OnRxDataEntry()
    {
        guideIntro.SetActive(false);
        guideRxDataEntry.SetActive(true);
    }

    public void OnDeAddRx()
    {
        guideRxDataEntry.SetActive(false);
        guideDeAddRx.SetActive(true);
    }

    private void Awake()
    {
        guidePromptsPanel = GameObject.FindGameObjectWithTag("GuidePromptsPanel");
    }

    private void Start()
    {
        //Intro prompt is showing when terminal is opened, it closes when player click on "Rx Data Entry"
        //The other two panels should be disabled

        guideIntro = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        guideRxDataEntry = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        guideDeAddRx = guidePromptsPanel.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;

        guideIntro.SetActive(true);
        guideRxDataEntry.SetActive(false);
        guideDeAddRx.SetActive(false);
    }

}
