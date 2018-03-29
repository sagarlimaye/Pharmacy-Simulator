using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddRxScript : MonoBehaviour {

    public enum Scenario { One, Two, Three, Off };
    public Scenario currentScenario;

    #region On Scene Objects
    public static GameObject rxScreen;
    public static GameObject addRxPanel;
    public static GameObject rxContent;
    public static GameObject rxInfoPanel;
    public static InputField addRxPatientInput;
    public static InputField addRxDoctorInput;
    public static Dropdown addRxDrugDropdown;
    public static Dropdown addRxQuantityDropdown;
    public static InputField addRxRefillsInput;
    public static Toggle addRxBrandToggle;
    public static Toggle addRxGenericToggle;
    public static InputField addRxwrittenDateInput;
    public static InputField addRxExpirationDateInput;
    public static Toggle addRxWaiterToggle;
    #endregion

    #region RxEntry Public Prefabs
    public GameObject rxEntryObjPrefab;
    public GameObject rxEntryPanelPrefab;
    public Toggle rxEntryWaiterTogglePrefab;
    public GameObject rxEntryWaiterToggleBackgroundPrefab;
    public GameObject rxEntryNameBtnPrefab;
    public GameObject rxEntryDrugBtnPrefab;
    public GameObject rxEntryAssemblyBtnPrefab;
    public GameObject rxEntryIdPrefab;
    #endregion

    #region Assembly Panel Prefabs
    public GameObject apAssemblyPanelPrefab;
    public Button apDoneBtnPrefab;
    public Button apModifyBtnPrefab;
    public Button apRxIdBtnPrefab;
    public Button apPriceBtnPrefab;
    public GameObject apDataPanelPrefab;

    public GameObject apRxInfoPanelPrefab;
    public GameObject apPatientPanelPrefab;
    public InputField apPatientInputFieldPrefab;
    public GameObject apDoctorPanelPrefab;
    public InputField apDoctorInputFieldPrefab;
    public GameObject apDrugPanelPrefab;
    public Dropdown apDrugDropdownPrefab;
    public GameObject apQuantityPanelPrefab;
    public Dropdown apQuantityDropdownPrefab;
    public GameObject apRefillPanelPrefab;
    public InputField apRefillInputFieldPrefab;
    public GameObject apBgPanelPrefab;
    public Toggle apBrandTogglePrefab;
    public GameObject apBrandToggleBackgroundPrefab;
    public Toggle apGenericTogglePrefab;
    public GameObject apGenericToggleBackgroundPrefab;
    public GameObject apWrittenPanelPrefab;
    public InputField apWrittenInputFieldPrefab;
    public GameObject apExpPanelPrefab;
    public InputField apExpInputFieldPrefab;
    public GameObject apWaiterPanelPrefab;
    public Toggle apWaiterTogglePrefab;
    public GameObject apWaiterToggleBackgroundPrefab;

    public GameObject apRxCheckPanelPrefab;
    public GameObject apPatientCheckPanelPrefab;
    public GameObject apDoctorCheckPanelPrefab;
    public GameObject apDrugCheckPanelPrefab;
    public GameObject apQuantityCheckPanelPrefab;
    public GameObject apRefillCheckPanelPrefab;
    public GameObject apBgCheckPanelPrefab;
    public GameObject apWrittenCheckPanelPrefab;
    public GameObject apExpCheckPanelPrefab;
    public GameObject apWaiterCheckPanelPrefab;
    public Toggle apPatientCheckTogglePrefab;
    public Toggle apDoctorCheckTogglePrefab;
    public Toggle apDrugCheckTogglePrefab;
    public Toggle apQuantityCheckTogglePrefab;
    public Toggle apRefillCheckTogglePrefab;
    public Toggle apBgCheckTogglePrefab;
    public Toggle apWrittenCheckTogglePrefab;
    public Toggle apExpCheckTogglePrefab;
    public Toggle apWaiterCheckTogglePrefab;
    public GameObject apPatientCheckToggleBackgroundPrefab;
    public GameObject apDoctorCheckToggleBackgroundPrefab;
    public GameObject apDrugCheckToggleBackgroundPrefab;
    public GameObject apQuantityCheckToggleBackgroundPrefab;
    public GameObject apRefillCheckToggleBackgroundPrefab;
    public GameObject apBgCheckToggleBackgroundPrefab;
    public GameObject apWrittenCheckToggleBackgroundPrefab;
    public GameObject apExpCheckToggleBackgroundPrefab;
    public GameObject apWaiterCheckToggleBackgroundPrefab;
    #endregion

    public void Start()
    {
        switch (currentScenario)
        {
            case Scenario.Off:

                GenerateRxEntry();
                break;

            case Scenario.One:

                break;

            case Scenario.Two:

                break;

            case Scenario.Three:

                break;
        }

        PopulateAddRxDrugDropdownValues();
    }

    public void OnAddRxDrugDropdownValueChange()
    {
        UpdateQuantityDropdownValues();
    }


    private void PopulateAddRxDrugDropdownValues()
    {
        List<string> drugNames = new List<string>();
        drugNames.Add("Select drug");
        foreach (var drug in Drug.drugNames)
        {
            drugNames.Add(drug);
        }
        addRxDrugDropdown.AddOptions(drugNames);
    }

    private void GenerateRxEntry()
    {
        throw new NotImplementedException();
    }

    public void Awake()
    {
        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        rxInfoPanel = GameObject.FindGameObjectWithTag("RxInfoPanel");
        addRxPanel = GameObject.FindGameObjectWithTag("AddRxPanel");
        addRxPatientInput = GameObject.FindGameObjectWithTag("PatientInput").GetComponent<InputField>();
        addRxDoctorInput = GameObject.FindGameObjectWithTag("DoctorInput").GetComponent<InputField>();
        addRxDrugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        addRxQuantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
        addRxRefillsInput = GameObject.FindGameObjectWithTag("RefillsInput").GetComponent<InputField>();
        addRxBrandToggle = GameObject.FindGameObjectWithTag("BrandToggle").GetComponent<Toggle>();
        addRxGenericToggle = GameObject.FindGameObjectWithTag("GenericToggle").GetComponent<Toggle>();
        addRxwrittenDateInput = GameObject.FindGameObjectWithTag("WrittenInput").GetComponent<InputField>();
        addRxExpirationDateInput = GameObject.FindGameObjectWithTag("ExpInput").GetComponent<InputField>();
        addRxWaiterToggle = GameObject.FindGameObjectWithTag("WaiterToggle").GetComponent<Toggle>();

        addRxPanel.SetActive(false);
    }

    public void OnAddRx()
    {
        lastAddRxId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(7).GetComponentInChildren<Text>().text;
        TransferNameFromPatientEntryToAddRx();
        addRxPanel.SetActive(true);
    }

    public void OnOk()
    {
        if (AssemblyScript.lastModifiedId != null && lastAddRxId != AssemblyScript.lastModifiedId)
            lastAddRxId = AssemblyScript.lastModifiedId;

        DestroyPreExistingRxEntry();
        DestroyPreExistingRxAssemblyPanel();
        InstantiateRxEntry();
        SaveAddRxDataToNewRxEntry();
        InstantiateAssemblyPanel();
        SaveAddRxDataToNewAssemblyPanel();
        ResetAddRxPanelInputs();
        addRxPanel.SetActive(false);
    }

    public void OnCancel()
    {
        ResetAddRxPanelInputs();
        addRxPanel.SetActive(false);
    }

    private void DestroyPreExistingRxEntry()
    {
        for (int i = 0; i < rxContent.transform.childCount; i++)
        {
            GameObject currentRxEntryClone = rxContent.transform.GetChild(i).gameObject;
            string currentRxIDTxt = currentRxEntryClone.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text;

            if (currentRxIDTxt == AssemblyScript.lastModifiedId)
            {
                Destroy(rxContent.transform.GetChild(i).transform.gameObject);
            }
        }
    }

    private void DestroyPreExistingRxAssemblyPanel()
    {
        for (int i = 1; i < rxScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyPanel = rxScreen.transform.GetChild(i).gameObject;
            string currentAssemblyPanelIdTxt = currentAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text;

            if (currentAssemblyPanelIdTxt == AssemblyScript.lastModifiedId)
            {
                Destroy(rxScreen.transform.GetChild(i).gameObject);
            }
        }
    }

    private void InstantiateRxEntry()
    {
        cloneRxEntryObj = Instantiate(rxEntryObjPrefab, rxContent.transform);
        cloneRxEntryPanel = Instantiate(rxEntryPanelPrefab, cloneRxEntryObj.transform);
        cloneWaiterToggle = Instantiate(rxEntryWaiterTogglePrefab, cloneRxEntryPanel.transform);
        cloneWaiterToggleBackground = Instantiate(rxEntryWaiterToggleBackgroundPrefab, cloneWaiterToggle.transform);
        cloneNameBtn = Instantiate(rxEntryNameBtnPrefab, cloneRxEntryPanel.transform);
        cloneDrugBtn = Instantiate(rxEntryDrugBtnPrefab, cloneRxEntryPanel.transform);
        cloneAssemblyBtn = Instantiate(rxEntryAssemblyBtnPrefab, cloneRxEntryPanel.transform);
        cloneRxID = Instantiate(rxEntryIdPrefab, cloneRxEntryPanel.transform);

        cloneWaiterToggle.GetComponent<Toggle>().graphic = cloneWaiterToggleBackground.transform.GetChild(0).GetComponent<Image>();

    }

    private void SaveAddRxDataToNewRxEntry()
    {
        {
            GameObject patientPanel = rxInfoPanel.transform.GetChild(0).gameObject;
            string patientTxt = patientPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneNameBtn.GetComponentInChildren<Text>().text = patientTxt;

            GameObject drugPanel = rxInfoPanel.transform.GetChild(2).gameObject;
            string drugTxt = drugPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneDrugBtn.GetComponentInChildren<Text>().text = drugTxt;

            GameObject waiterPanel = rxInfoPanel.transform.GetChild(8).gameObject;
            bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
            cloneWaiterToggle.GetComponent<Toggle>().isOn = waiter;

            cloneRxID.GetComponentInChildren<Text>().text = lastAddRxId;
        }
    }

    private void InstantiateAssemblyPanel()
    {
        cloneApAssemblyPanel = Instantiate(apAssemblyPanelPrefab, rxScreen.transform);
        cloneApDataPanel = Instantiate(apDataPanelPrefab, cloneApAssemblyPanel.transform);
        cloneApDoneBtn = Instantiate(apDoneBtnPrefab, cloneApAssemblyPanel.transform);
        cloneApModifyBtn = Instantiate(apModifyBtnPrefab, cloneApAssemblyPanel.transform);
        cloneApPriceBtn = Instantiate(apPriceBtnPrefab, cloneApAssemblyPanel.transform);
        cloneApRxIdBtn = Instantiate(apRxIdBtnPrefab, cloneApAssemblyPanel.transform);

        cloneApRxInfoPanel = Instantiate(apRxInfoPanelPrefab, cloneApDataPanel.transform);
        cloneApRxCheckPanel = Instantiate(apRxCheckPanelPrefab, cloneApDataPanel.transform);

        cloneApPatientPanel = Instantiate(apPatientPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApDoctorPanel = Instantiate(apDoctorPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApDrugPanel = Instantiate(apDrugPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApQuantityPanel = Instantiate(apQuantityPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApRefillPanel = Instantiate(apRefillPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApBgPanel = Instantiate(apBgPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApWrittenPanel = Instantiate(apWrittenPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApExpPanel = Instantiate(apExpPanelPrefab, cloneApRxInfoPanel.transform);
        cloneApWaiterPanel = Instantiate(apWaiterPanelPrefab, cloneApRxInfoPanel.transform);

        cloneApPatientInputField = Instantiate(apPatientInputFieldPrefab, cloneApPatientPanel.transform);
        cloneApDoctorInputField = Instantiate(apDoctorInputFieldPrefab, cloneApDoctorPanel.transform);
        cloneApDrugDropdown = Instantiate(apDrugDropdownPrefab, cloneApDrugPanel.transform);
        cloneApQuantityDropdown = Instantiate(apQuantityDropdownPrefab, cloneApQuantityPanel.transform);
        cloneApRefillInputField = Instantiate(apRefillInputFieldPrefab, cloneApRefillPanel.transform);
        cloneApBrandToggle = Instantiate(apBrandTogglePrefab, cloneApBgPanel.transform);
        cloneApGenericToggle = Instantiate(apGenericTogglePrefab, cloneApBgPanel.transform);
        cloneApWrittenInputField = Instantiate(apWrittenInputFieldPrefab, cloneApWrittenPanel.transform);
        cloneApExpInputField = Instantiate(apExpInputFieldPrefab, cloneApExpPanel.transform);
        cloneApWaiterToggle = Instantiate(apWaiterTogglePrefab, cloneApWaiterPanel.transform);

        cloneApBrandToggleBackground = Instantiate(apBrandToggleBackgroundPrefab, cloneApBrandToggle.transform);
        cloneApGenericToggleBackground = Instantiate(apGenericToggleBackgroundPrefab, cloneApGenericToggle.transform);
        cloneApWaiterToggleBackground = Instantiate(apWaiterToggleBackgroundPrefab, cloneApWaiterToggle.transform);

        cloneApPatientCheckPanel = Instantiate(apPatientCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApDoctorCheckPanel = Instantiate(apDoctorCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApDrugCheckPanel = Instantiate(apDrugCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApQuantityCheckPanel = Instantiate(apQuantityCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApRefillCheckPanel = Instantiate(apRefillCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApBgCheckPanel = Instantiate(apBgCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApWrittenCheckPanel = Instantiate(apWrittenCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApExpCheckPanel = Instantiate(apExpCheckPanelPrefab, cloneApRxCheckPanel.transform);
        cloneApWaiterCheckPanel = Instantiate(apWaiterCheckPanelPrefab, cloneApRxCheckPanel.transform);

        cloneApPatientCheckToggle = Instantiate(apPatientCheckTogglePrefab, cloneApPatientCheckPanel.transform);
        cloneApDoctorCheckToggle = Instantiate(apDoctorCheckTogglePrefab, cloneApDoctorCheckPanel.transform);
        cloneApDrugCheckToggle = Instantiate(apDrugCheckTogglePrefab, cloneApDrugCheckPanel.transform);
        cloneApQuantityCheckToggle = Instantiate(apQuantityCheckTogglePrefab, cloneApQuantityCheckPanel.transform);
        cloneApRefillCheckToggle = Instantiate(apRefillCheckTogglePrefab, cloneApRefillCheckPanel.transform);
        cloneApBgCheckToggle = Instantiate(apBgCheckTogglePrefab, cloneApBgCheckPanel.transform);
        cloneApWrittenCheckToggle = Instantiate(apWrittenCheckTogglePrefab, cloneApWrittenCheckPanel.transform);
        cloneApExpCheckToggle = Instantiate(apExpCheckTogglePrefab, cloneApExpCheckPanel.transform);
        cloneApWaiterCheckToggle = Instantiate(apWaiterCheckTogglePrefab, cloneApWaiterCheckPanel.transform);

        cloneApPatientCheckToggleBackground = Instantiate(apPatientCheckToggleBackgroundPrefab, cloneApPatientCheckToggle.transform);
        cloneApDoctorCheckToggleBackground = Instantiate(apDoctorCheckToggleBackgroundPrefab, cloneApDoctorCheckToggle.transform);
        cloneApDrugCheckToggleBackground = Instantiate(apDrugCheckToggleBackgroundPrefab, cloneApDrugCheckToggle.transform);
        cloneApQuantityCheckToggleBackground = Instantiate(apQuantityCheckToggleBackgroundPrefab, cloneApQuantityCheckToggle.transform);
        cloneApRefillCheckToggleBackground = Instantiate(apRefillCheckToggleBackgroundPrefab, cloneApRefillCheckToggle.transform);
        cloneApBgCheckToggleBackground = Instantiate(apBgCheckToggleBackgroundPrefab, cloneApBgCheckToggle.transform);
        cloneApWrittenCheckToggleBackground = Instantiate(apWrittenCheckToggleBackgroundPrefab, cloneApWrittenCheckToggle.transform);
        cloneApExpCheckToggleBackground = Instantiate(apExpCheckToggleBackgroundPrefab, cloneApExpCheckToggle.transform);
        cloneApWaiterCheckToggleBackground = Instantiate(apWaiterCheckToggleBackgroundPrefab, cloneApWaiterCheckToggle.transform);

        cloneApPatientCheckToggle.GetComponent<Toggle>().graphic = cloneApPatientCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApDoctorCheckToggle.GetComponent<Toggle>().graphic = cloneApDoctorCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApDrugCheckToggle.GetComponent<Toggle>().graphic = cloneApDrugCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApQuantityCheckToggle.GetComponent<Toggle>().graphic = cloneApQuantityCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApRefillCheckToggle.GetComponent<Toggle>().graphic = cloneApRefillCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApBgCheckToggle.GetComponent<Toggle>().graphic = cloneApBgCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApWrittenCheckToggle.GetComponent<Toggle>().graphic = cloneApWrittenCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApExpCheckToggle.GetComponent<Toggle>().graphic = cloneApExpCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApWaiterCheckToggle.GetComponent<Toggle>().graphic = cloneApWaiterCheckToggleBackground.transform.GetChild(0).GetComponent<Image>();

        cloneApBrandToggle.GetComponent<Toggle>().graphic = cloneApBrandToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApGenericToggle.GetComponent<Toggle>().graphic = cloneApGenericToggleBackground.transform.GetChild(0).GetComponent<Image>();
        cloneApWaiterToggle.GetComponent<Toggle>().graphic = cloneApWaiterToggleBackground.transform.GetChild(0).GetComponent<Image>();

    }

    private void SaveAddRxDataToNewAssemblyPanel()
    {
        List<List<string>> d1 = Drug.drugInfo[addRxDrugDropdown.options[1].text];
        List<string> dq1 = d1[0];
        addRxQuantityDropdown.AddOptions(dq1);


        GameObject patientPanel = rxInfoPanel.transform.GetChild(0).gameObject;
        string patientTxt = patientPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApPatientInputField.GetComponentInChildren<InputField>().text = patientTxt;

        GameObject drPanel = rxInfoPanel.transform.GetChild(1).gameObject;
        string drTxt = drPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApDoctorInputField.GetComponentInChildren<InputField>().text = drTxt;

        GameObject drugPanel = rxInfoPanel.transform.GetChild(2).gameObject;
        int drugValue = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string drugNameSelected = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().options[drugValue].text;
        cloneApDrugDropdown.ClearOptions();
        cloneApDrugDropdown.value = drugValue;
        cloneApDrugDropdown.AddOptions(new List<string>() { drugNameSelected });

        GameObject quantityPanel = rxInfoPanel.transform.GetChild(3).gameObject;
        int quantityValue = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string quantitySelected = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().options[quantityValue].text;
        cloneApQuantityDropdown.ClearOptions();
        cloneApQuantityDropdown.value = quantityValue;
        cloneApQuantityDropdown.AddOptions(new List<string>() { quantitySelected });

        GameObject refillPanel = rxInfoPanel.transform.GetChild(4).gameObject;
        string refillTxt = refillPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApRefillInputField.GetComponentInChildren<InputField>().text = refillTxt;

        GameObject bgPanel = rxInfoPanel.transform.GetChild(5).gameObject;
        bool brand = bgPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        bool generic = bgPanel.transform.GetChild(1).GetComponent<Toggle>().isOn;
        cloneApBrandToggle.GetComponent<Toggle>().isOn = brand;
        cloneApGenericToggle.GetComponent<Toggle>().isOn = generic;

        GameObject writtenPanel = rxInfoPanel.transform.GetChild(6).gameObject;
        string writtenTxt = writtenPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApWrittenInputField.GetComponentInChildren<InputField>().text = writtenTxt;

        GameObject expPanel = rxInfoPanel.transform.GetChild(7).gameObject;
        string expTxt = expPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApExpInputField.GetComponentInChildren<InputField>().text = expTxt;

        GameObject waiterPanel = rxInfoPanel.transform.GetChild(8).gameObject;
        bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        cloneApWaiterToggle.GetComponent<Toggle>().isOn = waiter;

        cloneApAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text = lastAddRxId;

    }

    private void TransferNameFromPatientEntryToAddRx()
    {
        GameObject currentPatientEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string patientFullName = currentPatientEntry.transform.GetChild(0).GetComponentInChildren<Text>().text + " " +
                                    currentPatientEntry.transform.GetChild(1).GetComponentInChildren<Text>().text;
        addRxPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<InputField>().text = patientFullName;
    }

    private void UpdateAssemblyPanelPriceAndId()
    {
        for (int i = 0; i < Drug.drugNames.Count; i++)
        {
            if(addRxDrugDropdown.value == i+1)
            {
                List<List<string>> drugInfo = Drug.drugInfo[addRxDrugDropdown.options[i+1].text];
                List<string> drugPrices = drugInfo[1];
                //CONTINUE HERE, find quantity selected, match with correct price
            }
        }
    }

    private void UpdateQuantityDropdownValues()
    {
        //Refactor: Switch to loop
        switch (addRxDrugDropdown.value)
        {
            case 1:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d1 = Drug.drugInfo[addRxDrugDropdown.options[1].text];
                List<string> dq1 = d1[0];
                addRxQuantityDropdown.AddOptions(dq1);
                break;
            case 2:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d2 = Drug.drugInfo[addRxDrugDropdown.options[2].text];
                List<string> dq2 = d2[0];
                addRxQuantityDropdown.AddOptions(dq2);
                break;
            case 3:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d3 = Drug.drugInfo[addRxDrugDropdown.options[3].text];
                List<string> dq3 = d3[0];
                addRxQuantityDropdown.AddOptions(dq3);
                break;
            case 4:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d4 = Drug.drugInfo[addRxDrugDropdown.options[4].text];
                List<string> dq4 = d4[0];
                addRxQuantityDropdown.AddOptions(dq4);
                break;
            case 5:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d5 = Drug.drugInfo[addRxDrugDropdown.options[5].text];
                List<string> dq5 = d5[0];
                addRxQuantityDropdown.AddOptions(dq5);
                break;
            case 6:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d6 = Drug.drugInfo[addRxDrugDropdown.options[6].text];
                List<string> dq6 = d6[0];
                addRxQuantityDropdown.AddOptions(dq6);
                break;
        }
    }

    private void ResetAddRxPanelInputs()
    {
        addRxPatientInput.text = "";
        addRxDoctorInput.text = "";
        addRxDrugDropdown.value = 0;
        addRxQuantityDropdown.value = 0;
        addRxRefillsInput.text = "";
        addRxBrandToggle.isOn = false;
        addRxGenericToggle.isOn = false;
        addRxwrittenDateInput.text = "";
        addRxExpirationDateInput.text = "";
        addRxWaiterToggle.isOn = false;
    }

    private static string lastAddRxId;

    #region Assembly Panel Clones
    private GameObject cloneApAssemblyPanel;
    private Button cloneApDoneBtn;
    private Button cloneApModifyBtn;
    private Button cloneApRxIdBtn;
    private Button cloneApPriceBtn;
    private GameObject cloneApDataPanel;

    private GameObject cloneApRxInfoPanel;
    private GameObject cloneApPatientPanel;
    private InputField cloneApPatientInputField;
    private GameObject cloneApDoctorPanel;
    private InputField cloneApDoctorInputField;
    private GameObject cloneApDrugPanel;
    private Dropdown cloneApDrugDropdown;
    private GameObject cloneApQuantityPanel;
    private Dropdown cloneApQuantityDropdown;
    private GameObject cloneApRefillPanel;
    private InputField cloneApRefillInputField;
    private GameObject cloneApBgPanel;
    private Toggle cloneApBrandToggle;
    private GameObject cloneApBrandToggleBackground;
    private Toggle cloneApGenericToggle;
    private GameObject cloneApGenericToggleBackground;
    private GameObject cloneApWrittenPanel;
    private InputField cloneApWrittenInputField;
    private GameObject cloneApExpPanel;
    private InputField cloneApExpInputField;
    private GameObject cloneApWaiterPanel;
    private Toggle cloneApWaiterToggle;
    private GameObject cloneApWaiterToggleBackground;

    private GameObject cloneApRxCheckPanel;
    private GameObject cloneApPatientCheckPanel;
    private GameObject cloneApDoctorCheckPanel;
    private GameObject cloneApDrugCheckPanel;
    private GameObject cloneApQuantityCheckPanel;
    private GameObject cloneApRefillCheckPanel;
    private GameObject cloneApBgCheckPanel;
    private GameObject cloneApWrittenCheckPanel;
    private GameObject cloneApExpCheckPanel;
    private GameObject cloneApWaiterCheckPanel;
    private Toggle cloneApPatientCheckToggle;
    private Toggle cloneApDoctorCheckToggle;
    private Toggle cloneApDrugCheckToggle;
    private Toggle cloneApQuantityCheckToggle;
    private Toggle cloneApRefillCheckToggle;
    private Toggle cloneApBgCheckToggle;
    private Toggle cloneApWrittenCheckToggle;
    private Toggle cloneApExpCheckToggle;
    private Toggle cloneApWaiterCheckToggle;
    private GameObject cloneApPatientCheckToggleBackground;
    private GameObject cloneApDoctorCheckToggleBackground;
    private GameObject cloneApDrugCheckToggleBackground;
    private GameObject cloneApQuantityCheckToggleBackground;
    private GameObject cloneApRefillCheckToggleBackground;
    private GameObject cloneApBgCheckToggleBackground;
    private GameObject cloneApWrittenCheckToggleBackground;
    private GameObject cloneApExpCheckToggleBackground;
    private GameObject cloneApWaiterCheckToggleBackground;
    #endregion

    #region Rx Entry Clones
    private GameObject cloneRxEntryObj;
    private GameObject cloneRxEntryPanel;
    private Toggle cloneWaiterToggle;
    private GameObject cloneWaiterToggleBackground;
    private GameObject cloneNameBtn;
    private GameObject cloneDrugBtn;
    private GameObject cloneAssemblyBtn;    
    private GameObject cloneRxID;
    #endregion
}
