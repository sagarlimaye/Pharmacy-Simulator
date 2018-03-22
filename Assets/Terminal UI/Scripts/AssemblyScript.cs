using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour {

    #region On Scene Objects
    public static GameObject rxScreen;
    public static GameObject profileScreen;
    public static GameObject rxContent;
    public static GameObject profilesContent;
    public static GameObject assemblyPanel;
    #endregion

    public void Awake()
    {
        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
    }

    public void OnAssemble()
    {
        currentRxEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentRxIDTxt = currentRxEntry.transform.GetChild(5).GetComponent<Text>().text;

        for (int i = 1; i < rxScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyClone = rxScreen.transform.GetChild(i).gameObject;
            string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

            if(currentRxIDTxt == currentAssemblyIdTxt)
            {
                rxScreen.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void OnDone()
    {
        DestroyRxEntry();
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    private void DestroyRxEntry()
    {
        for (int i = 0; i < rxContent.transform.childCount; i++)
        {
            Toggle toggle = rxContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(4).GetComponent<Toggle>();
            if (toggle.isOn)
            {
                Destroy(rxContent.transform.GetChild(i).transform.gameObject);
            }
        }
    }

    private GameObject currentRxEntry;


}

