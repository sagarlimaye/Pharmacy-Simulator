using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideButtonScript : MonoBehaviour {

    public GameObject GuideBtnTerminalIntro;
    public static GameObject GuideBtnRxScreenPanel;

    public void CloseIntroGuide()
    {
        GuideBtnTerminalIntro.SetActive(false);
    }

    public void OpenPanelGuide()
    {
        GuideBtnRxScreenPanel.SetActive(true);
    }

    public void ClosePanelGuide()
    {
        GuideBtnRxScreenPanel.SetActive(false);
    }
}
