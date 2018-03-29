using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour {

    public static string lastModifiedId;

    #region On Scene Objects
    public static GameObject rxScreen;
    public static GameObject profileScreen;
    public static GameObject rxContent;
    public static GameObject profilesContent;
    public static GameObject assemblyPanel;
    public static GameObject addRxPanel;
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
        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        rxInfoPanel = GameObject.FindGameObjectWithTag("RxInfoPanel");
        addRxPanel = GameObject.FindGameObjectWithTag("AddRxPanel");
        addRxPatientInput = GameObject.FindGameObjectWithTag("PatientInput").GetComponent<InputField>();
        addRxDoctorInput = GameObject.FindGameObjectWithTag("DoctorInput").GetComponent<InputField>();
        addRxDrugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        addRxQuantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
        addRxRefillsInput = GameObject.FindGameObjectWithTag("RefillsInput").GetComponent<InputField>();
        addRxBrandToggle = GameObject.FindGameObjectWithTag("BrandToggle").GetComponent<Toggle>();
        addRxGenericToggle = GameObject.FindGameObjectWithTag("GenericToggle").GetComponent<Toggle>();
        addRxWrittenDateInput = GameObject.FindGameObjectWithTag("WrittenInput").GetComponent<InputField>();
        addRxExpirationDateInput = GameObject.FindGameObjectWithTag("ExpInput").GetComponent<InputField>();
        addRxWaiterToggle = GameObject.FindGameObjectWithTag("WaiterToggle").GetComponent<Toggle>();
    }

    public void OnAssemble()
    {
        GameObject currentRxEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentRxIDTxt = currentRxEntry.transform.GetChild(4).GetComponent<Text>().text;

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

    public void OnModify()
    {
        lastModifiedId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().text;
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
        profileScreen.SetActive(true);
        addRxPanel.SetActive(true);

        RepopulateAddRxPanel();
    }

    private void RepopulateAddRxPanel()
    {
        GameObject currentAssembly = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

        GameObject assemblyRxInfoPanel = currentAssembly.transform.GetChild(2).transform.GetChild(0).gameObject;

        string patientInput = assemblyRxInfoPanel.transform.GetChild(0).transform.GetChild(1).GetComponent<InputField>().text;
        addRxPatientInput.text = patientInput;

        string doctorInput = assemblyRxInfoPanel.transform.GetChild(1).transform.GetChild(1).GetComponent<InputField>().text;
        addRxDoctorInput.text = doctorInput;

        string drugInput = assemblyRxInfoPanel.transform.GetChild(2).transform.GetChild(1).GetComponent<Dropdown>().options[0].text;
        for (int i = 0; i < addRxDrugDropdown.options.Count; i++)
        {
            if (drugInput == addRxDrugDropdown.options[i].text)
                addRxDrugDropdown.value = i;
        }

        string quantityInput = assemblyRxInfoPanel.transform.GetChild(3).transform.GetChild(1).GetComponent<Dropdown>().options[0].text;
        for (int i = 0; i < addRxQuantityDropdown.options.Count; i++)
        {
            if (drugInput == addRxQuantityDropdown.options[i].text)
                addRxQuantityDropdown.value = i;
        }

        string refillInput = assemblyRxInfoPanel.transform.GetChild(4).transform.GetChild(1).GetComponent<InputField>().text;
        addRxRefillsInput.text = refillInput;

        bool brandInput = assemblyRxInfoPanel.transform.GetChild(5).transform.GetChild(0).GetComponent<Toggle>().isOn;
        addRxBrandToggle.GetComponent<Toggle>().isOn = brandInput;

        bool genericInput = assemblyRxInfoPanel.transform.GetChild(5).transform.GetChild(1).GetComponent<Toggle>().isOn;
        addRxGenericToggle.GetComponent<Toggle>().isOn = genericInput;

        string writtenInput = assemblyRxInfoPanel.transform.GetChild(6).transform.GetChild(1).GetComponent<InputField>().text;
        addRxWrittenDateInput.text = writtenInput;

        string expInput = assemblyRxInfoPanel.transform.GetChild(7).transform.GetChild(1).GetComponent<InputField>().text;
        addRxExpirationDateInput.text = expInput;

        bool waiterInput = assemblyRxInfoPanel.transform.GetChild(8).transform.GetChild(0).GetComponent<Toggle>().isOn;
        addRxWaiterToggle.GetComponent<Toggle>().isOn = waiterInput;

    }

    private void DestroyRxEntry()
    {
        GameObject currentAssemblyClone = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

        for (int i = 0; i < rxContent.transform.childCount; i++)
        {
            GameObject currentRxEntryClone = rxContent.transform.GetChild(i).gameObject;
            string currentRxIDTxt = currentRxEntryClone.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text;

            if (currentRxIDTxt == currentAssemblyIdTxt)
            {
                Destroy(rxContent.transform.GetChild(i).transform.gameObject);
                Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
            }
        }
    }
}

