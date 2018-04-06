using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour {

    public static string lastModifiedId;

    #region On Scene Objects
    public static GameObject assemblyScreen;
    public static GameObject profileScreen;
    public static GameObject assemblyContent;
    public static GameObject profilesContent;
    public static GameObject assemblyPanel;
    public static GameObject addRxPanel;
    public static GameObject addRxScanPromptPanel;
    public static GameObject rxInfoPanel;
    public static InputField addRxPatientInput;
    public static InputField addRxDoctorInput;
    public static Dropdown addRxDrugDropdown;
    public static Dropdown addRxQuantityDropdown;
    public static InputField addRxRefillsInput;
    public static Toggle addRxBrandToggle;
    public static Toggle addRxGenericToggle;
    public static InputField addRxWrittenDateInput;
    public static InputField addRxExpirationDateInput;
    public static Toggle addRxWaiterToggle;
    #endregion

    public void Awake()
    {
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        addRxPanel = GameObject.FindGameObjectWithTag("AddRxPanel");
        addRxScanPromptPanel = GameObject.FindGameObjectWithTag("ScanPrompt");
        addRxDrugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        addRxQuantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
    }

    public void OnAssemble()
    {
        GameObject currentRxEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentRxIDTxt = currentRxEntry.transform.GetChild(4).GetComponent<Text>().text;

        for (int i = 1; i < assemblyScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyClone = assemblyScreen.transform.GetChild(i).gameObject;
            string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

            if(currentRxIDTxt == currentAssemblyIdTxt)
            {
                assemblyScreen.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void OnDone()
    {
        DestroyRxEntry();
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }

    public void OnModify()
    {
        lastModifiedId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().text;

        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        profileScreen.SetActive(true);
        addRxPanel.SetActive(true);
        addRxScanPromptPanel.SetActive(false);

        RepopulateAddRxPanel();
    }

    private void RepopulateAddRxPanel()
    {
        GameObject currentAssembly = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        GameObject assemblyRxInfoPanel = currentAssembly.transform.GetChild(2).transform.GetChild(0).gameObject;

        string patientInput = assemblyRxInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text;
        addRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text = patientInput;

        string doctorInput = assemblyRxInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text;
        addRxPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().text = doctorInput;

        string drugInput = assemblyRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Dropdown>().options[0].text;
        for (int i = 0; i < addRxDrugDropdown.options.Count; i++)
        {
            if (drugInput == addRxDrugDropdown.options[i].text)
                addRxDrugDropdown.value = i;
        }

        string quantityInput = assemblyRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Dropdown>().options[0].text;
        for (int i = 0; i < addRxQuantityDropdown.options.Count; i++)
        {
            if (quantityInput == addRxQuantityDropdown.options[i].text)
                addRxQuantityDropdown.value = i;
        }

        string refillInput = assemblyRxInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text;
        addRxPanel.transform.GetChild(1).GetChild(4).GetChild(1).GetComponent<InputField>().text = refillInput;

        bool brandInput = assemblyRxInfoPanel.transform.GetChild(5).GetChild(0).GetComponent<Toggle>().isOn;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Toggle>().isOn = brandInput;

        bool genericInput = assemblyRxInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<Toggle>().isOn;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Toggle>().isOn = genericInput;

        string writtenInput = assemblyRxInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text;
        addRxPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponent<InputField>().text = writtenInput;

        string expInput = assemblyRxInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<InputField>().text;
        addRxPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponent<InputField>().text = expInput;

        bool waiterInput = assemblyRxInfoPanel.transform.GetChild(8).GetChild(0).GetComponent<Toggle>().isOn;
        addRxPanel.transform.GetChild(1).GetChild(8).GetChild(0).GetComponent<Toggle>().isOn = waiterInput;

        //Transfter RxImage back to AddRxPanel
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(0).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(1).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(2).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(3).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(4).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(5).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(6).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
        Instantiate(currentAssembly.transform.GetChild(0).GetChild(7).gameObject, profileScreen.transform.GetChild(2).GetChild(0));
    }

    private void DestroyRxEntry()
    {
        GameObject currentAssemblyClone = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

        for (int i = 0; i < assemblyContent.transform.childCount; i++)
        {
            GameObject currentRxEntryClone = assemblyContent.transform.GetChild(i).gameObject;
            string currentRxIDTxt = currentRxEntryClone.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text;

            if (currentRxIDTxt == currentAssemblyIdTxt)
            {
                Destroy(assemblyContent.transform.GetChild(i).transform.gameObject);
                Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
            }
        }
    }
}

