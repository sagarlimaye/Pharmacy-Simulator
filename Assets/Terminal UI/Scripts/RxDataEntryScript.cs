using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RxDataEntryScript : MonoBehaviour {

    #region On Scene Objects
    public static GameObject rxScreen;
    public static GameObject assemblyScreen;
    public static GameObject rxContent;
    public static GameObject assemblyContent;
    public static GameObject profilesContent;
    public static GameObject profileScreen;
    public static GameObject rxDEAddRxPanel;
    public static GameObject rxDEAddRxImage;
    public static GameObject rxDEAddRxInfoPanel;
    public static Dropdown rxDEaddRxDrugDropdown;
    public static Dropdown rxDEaddRxQuantityDropdown;
    #endregion

    #region Rx Data Entry Public Prefabs
    public GameObject rxDataEntryObjPrefab;
    public GameObject rxDataEntryPanelPrefab;
    public Toggle rxDataEntryWaiterTogglePrefab;
    public GameObject rxDataEntryWaiterToggleBackgroundPrefab;
    public GameObject rxDataEntryNameBtnPrefab;
    public GameObject rxDataEntryDrugBtnPrefab;
    public GameObject rxDataEntryAddRxBtnPrefab;
    public GameObject rxDataEntryIdPrefab;
    #endregion

    #region Assembly Entry Public Prefabs
    public GameObject aentryObjPrefab;
    public GameObject aentryPanelPrefab;
    public Toggle aentryWaiterTogglePrefab;
    public GameObject aentryWaiterToggleBackgroundPrefab;
    public GameObject aentryNameBtnPrefab;
    public GameObject aentryDrugBtnPrefab;
    public GameObject aentryAssemblyBtnPrefab;
    public GameObject aentryIdPrefab;
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

    #region RxImage Public Prefabs
    public GameObject rxImagePatientPrefab;
    public GameObject rxImageDoctorPrefab;
    public GameObject rxImageDrugPrefab;
    public GameObject rxImageQuantityPrefab;
    public GameObject rxImageRefillsPrefab;
    public GameObject rxImageGenOrBrPrefab;
    public GameObject rxImageWrittenPrefab;
    public GameObject rxImageExpPrefab;
    public GameObject rxImageSigPrefab;
    #endregion

    public void OnAddRx()
    {
        de_LastAddRxId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(4).GetComponent<Text>().text;
        ResetDEAddRxPanelInputs();
        TransferNameFromRxDataEntryToAddRx();
        InstantiateRxImage();
        GenerateRxImageData();
        rxDEAddRxPanel.SetActive(true);
    }

    public void OnOk()
    {
        if (AssemblyScript.lastModifiedId != null && de_LastAddRxId != AssemblyScript.lastModifiedId)
            de_LastAddRxId = AssemblyScript.lastModifiedId;

        DestroyPreExistingRxEntry();
        DestroyPreExistingRxAssemblyPanel();
        InstantiateAssemblyEntry();
        SaveDEAddRxDataToNewAssemblyEntry();
        InstantiateAssemblyPanel();
        SaveDEAddRxDataToNewAssemblyPanel();
        ResetDEAddRxPanelInputs();
        DestroyRxImage();

        rxDEAddRxPanel.SetActive(false);
        rxScreen.SetActive(false);
        assemblyScreen.SetActive(true);
 
        AssemblyScript.lastModifiedId = null;
    }

    public void OnCancel()
    {
        ResetDEAddRxPanelInputs();
        rxDEAddRxPanel.SetActive(false);
    }

    public void OnRxDEAddRxDrugDropdownValueChange()
    {
        UpdateQuantityDropdownValues();
    }

    private void Awake()
    {
        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        rxDEAddRxPanel = GameObject.FindGameObjectWithTag("DataEntryAddRxPanel");
        rxDEAddRxImage = GameObject.FindGameObjectWithTag("RxDEAddRxImage");
        rxDEAddRxInfoPanel = GameObject.FindGameObjectWithTag("DataEntryAddRxInfoPanel");
        rxDEaddRxDrugDropdown = GameObject.FindGameObjectWithTag("rxDEDrugDropdown").GetComponent<Dropdown>();
        rxDEaddRxQuantityDropdown = GameObject.FindGameObjectWithTag("rxDEQuantityDropdown").GetComponent<Dropdown>();

        rxDEAddRxPanel.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        InitializaRandomRxDataEntryGeneration();
        PopulateRxDEAddRxDrugDropdownValues();
    }

    private void InstantiateAssemblyEntry()
    {
        cloneAEntryObj = Instantiate(aentryObjPrefab, assemblyContent.transform);
        cloneAEntryPanel = Instantiate(aentryPanelPrefab, cloneAEntryObj.transform);
        cloneAEntryWaiterToggle = Instantiate(aentryWaiterTogglePrefab, cloneAEntryPanel.transform);
        cloneAEntryWaiterToggleBackground = Instantiate(aentryWaiterToggleBackgroundPrefab, cloneAEntryWaiterToggle.transform);
        cloneAEntryNameBtn = Instantiate(aentryNameBtnPrefab, cloneAEntryPanel.transform);
        cloneAEntryDrugBtn = Instantiate(aentryDrugBtnPrefab, cloneAEntryPanel.transform);
        cloneAEntryAssemblyBtn = Instantiate(aentryAssemblyBtnPrefab, cloneAEntryPanel.transform);
        cloneAEntryID = Instantiate(aentryIdPrefab, cloneAEntryPanel.transform);

        cloneAEntryWaiterToggle.GetComponent<Toggle>().graphic = cloneAEntryWaiterToggleBackground.transform.GetChild(0).GetComponent<Image>();
    }

    private void InstantiateAssemblyPanel()
    {
        cloneApAssemblyPanel = Instantiate(apAssemblyPanelPrefab, assemblyScreen.transform);
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

        cloneAssemblyRxImagePatient = Instantiate(rxDEAddRxImage.transform.GetChild(1).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageDoctor = Instantiate(rxDEAddRxImage.transform.GetChild(2).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageDrug = Instantiate(rxDEAddRxImage.transform.GetChild(3).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageQuantity = Instantiate(rxDEAddRxImage.transform.GetChild(4).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageRefills = Instantiate(rxDEAddRxImage.transform.GetChild(5).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageGenOrBr = Instantiate(rxDEAddRxImage.transform.GetChild(6).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageWritten = Instantiate(rxDEAddRxImage.transform.GetChild(7).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageExp = Instantiate(rxDEAddRxImage.transform.GetChild(8).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        cloneAssemblyRxImageSig = Instantiate(rxDEAddRxImage.transform.GetChild(9).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
    }

    private void InstantiateRxImage()
    {
        cloneRxImagePatient = Instantiate(rxImagePatientPrefab, rxDEAddRxImage.transform);
        cloneRxImageDoctor = Instantiate(rxImageDoctorPrefab, rxDEAddRxImage.transform);
        cloneRxImageDrug = Instantiate(rxImageDrugPrefab, rxDEAddRxImage.transform);
        cloneRxImageQuantity = Instantiate(rxImageQuantityPrefab, rxDEAddRxImage.transform);
        cloneRxImageRefills = Instantiate(rxImageRefillsPrefab, rxDEAddRxImage.transform);
        cloneRxImageGenOrBr = Instantiate(rxImageGenOrBrPrefab, rxDEAddRxImage.transform);
        cloneRxImageWritten = Instantiate(rxImageWrittenPrefab, rxDEAddRxImage.transform);
        cloneRxImageExp = Instantiate(rxImageExpPrefab, rxDEAddRxImage.transform);
        cloneRxImageSig = Instantiate(rxImageSigPrefab, rxDEAddRxImage.transform);
    }

    private void GenerateRxImageData()
    {
        var s = rxDEAddRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;

        cloneRxImagePatient.GetComponentInChildren<Text>().text = rxDEAddRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;

        //Find matching profile and get doctor name
        for (int i = 3; i < (profilesContent.transform.childCount + 3); i++)
        {
            GameObject currentProfileClone = profileScreen.transform.GetChild(i).gameObject;
            string currentProfileFullName = currentProfileClone.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text + " "
                                        + currentProfileClone.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().text;

            if (currentProfileFullName == cloneRxImagePatient.GetComponentInChildren<Text>().text)
            {
                cloneRxImageDoctor.GetComponentInChildren<Text>().text = currentProfileClone.transform.GetChild(1).GetChild(8).GetChild(1).GetComponent<InputField>().text;
                break;
            }
        }

        //Find matching rx data entry and get drug name
        for (int i = 0; i < (rxContent.transform.childCount); i++)
        {
            GameObject currentRxDataEntry = rxContent.transform.GetChild(i).gameObject;
            string currentRxDataEntryFullName = currentRxDataEntry.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text;

            if (currentRxDataEntryFullName == cloneRxImagePatient.GetComponentInChildren<Text>().text)
            {
                cloneRxImageDrug.GetComponentInChildren<Text>().text = currentRxDataEntry.transform.GetChild(0).GetChild(2).GetComponentInChildren<Text>().text;
                break;
            }
        }

        List<string> quantities = DrugDatabase.drugInfo[cloneRxImageDrug.GetComponentInChildren<Text>().text][0];
        cloneRxImageQuantity.GetComponentInChildren<Text>().text = quantities[rnd.Next(quantities.Count)];
        cloneRxImageRefills.GetComponentInChildren<Text>().text = PrescriptionDatabase.generateRefill();
        cloneRxImageGenOrBr.GetComponentInChildren<Text>().text = PrescriptionDatabase.generateGenOrBr();
        cloneRxImageWritten.GetComponentInChildren<Text>().text = PrescriptionDatabase.RandomDay().ToString();
        cloneRxImageExp.GetComponentInChildren<Text>().text = PrescriptionDatabase.FutureRandomDay().ToString();
        cloneRxImageSig.GetComponentInChildren<Text>().text = PrescriptionDatabase.GenerateSig();
    }

    private void SaveDEAddRxDataToNewAssemblyPanel()
    {
        UpdateAssemblyPanelPriceAndId();

        GameObject patientPanel = rxDEAddRxInfoPanel.transform.GetChild(0).gameObject;
        string patientTxt = patientPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApPatientInputField.GetComponentInChildren<InputField>().text = patientTxt;

        GameObject drPanel = rxDEAddRxInfoPanel.transform.GetChild(1).gameObject;
        string drTxt = drPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApDoctorInputField.GetComponentInChildren<InputField>().text = drTxt;

        GameObject drugPanel = rxDEAddRxInfoPanel.transform.GetChild(2).gameObject;
        int drugValue = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string drugNameSelected = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().options[drugValue].text;
        cloneApDrugDropdown.ClearOptions();
        cloneApDrugDropdown.value = drugValue;
        cloneApDrugDropdown.AddOptions(new List<string>() { drugNameSelected });

        GameObject quantityPanel = rxDEAddRxInfoPanel.transform.GetChild(3).gameObject;
        int quantityValue = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string quantitySelected = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().options[quantityValue].text;
        cloneApQuantityDropdown.ClearOptions();
        cloneApQuantityDropdown.value = quantityValue;
        cloneApQuantityDropdown.AddOptions(new List<string>() { quantitySelected });

        GameObject refillPanel = rxDEAddRxInfoPanel.transform.GetChild(4).gameObject;
        string refillTxt = refillPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApRefillInputField.GetComponentInChildren<InputField>().text = refillTxt;

        GameObject bgPanel = rxDEAddRxInfoPanel.transform.GetChild(5).gameObject;
        bool brand = bgPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        bool generic = bgPanel.transform.GetChild(1).GetComponent<Toggle>().isOn;
        cloneApBrandToggle.GetComponent<Toggle>().isOn = brand;
        cloneApGenericToggle.GetComponent<Toggle>().isOn = generic;

        GameObject writtenPanel = rxDEAddRxInfoPanel.transform.GetChild(6).gameObject;
        string writtenTxt = writtenPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApWrittenInputField.GetComponentInChildren<InputField>().text = writtenTxt;

        GameObject expPanel = rxDEAddRxInfoPanel.transform.GetChild(7).gameObject;
        string expTxt = expPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
        cloneApExpInputField.GetComponentInChildren<InputField>().text = expTxt;

        GameObject waiterPanel = rxDEAddRxInfoPanel.transform.GetChild(8).gameObject;
        bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        cloneApWaiterToggle.GetComponent<Toggle>().isOn = waiter;

        cloneApAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text = de_LastAddRxId;

    }

    private void UpdateAssemblyPanelPriceAndId()
    {
        for (int i = 1; i < DrugDatabase.drugNames.Count; i++)
        {
            if (rxDEaddRxDrugDropdown.value == i)
            {
                List<List<string>> drugInfo = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[i].text];
                List<string> drugPrices = drugInfo[1];

                for (int j = 1; j < drugPrices.Count; j++)
                {
                    if (rxDEaddRxDrugDropdown.value == j)
                    {
                        cloneApPriceBtn.transform.GetComponentInChildren<Text>().text = drugPrices[j];
                        break;
                    }
                }
                break;
            }
        }
    }

    private void InitializaRandomRxDataEntryGeneration()
    {
        for (int i = 0; i < rxDataEntries; i++)
        {
            InstantiateRxDataEntry();
            
            //Generate random patient data
            int randomNum = 2 + rnd.Next(profileScreen.transform.childCount - 2);
            GameObject randomProfileEntry = profileScreen.transform.GetChild(randomNum).gameObject;
        
            cloneRxDataEntryNameBtn.GetComponentInChildren<Text>().text = randomProfileEntry.transform.GetChild(1).GetChild(0).GetComponentInChildren<InputField>().text
                                                + " " + randomProfileEntry.transform.GetChild(1).GetChild(1).GetComponentInChildren<InputField>().text;

            cloneRxDataEntryDrugBtn.GetComponentInChildren<Text>().text = DrugDatabase.drugNames[rnd.Next(DrugDatabase.drugNames.Count)];
            cloneRxDataEntryID.GetComponent<Text>().text = randomProfileEntry.transform.GetChild(4).GetComponent<Text>().text;

        }
    }

    private void InstantiateRxDataEntry()
    {
        cloneRxDataEntryObj = Instantiate(rxDataEntryObjPrefab, rxContent.transform);
        cloneRxDataEntryPanel = Instantiate(rxDataEntryPanelPrefab, cloneRxDataEntryObj.transform);
        cloneRxDataEntryWaiterToggle = Instantiate(rxDataEntryWaiterTogglePrefab, cloneRxDataEntryPanel.transform);
        cloneRxDataEntryWaiterToggleBackground = Instantiate(rxDataEntryWaiterToggleBackgroundPrefab, cloneRxDataEntryWaiterToggle.transform);
        cloneRxDataEntryNameBtn = Instantiate(rxDataEntryNameBtnPrefab, cloneRxDataEntryPanel.transform);
        cloneRxDataEntryDrugBtn = Instantiate(rxDataEntryDrugBtnPrefab, cloneRxDataEntryPanel.transform);
        cloneRxDataEntryAddRxBtn = Instantiate(rxDataEntryAddRxBtnPrefab, cloneRxDataEntryPanel.transform);
        cloneRxDataEntryID = Instantiate(rxDataEntryIdPrefab, cloneRxDataEntryPanel.transform);

        cloneRxDataEntryWaiterToggle.GetComponent<Toggle>().graphic = cloneRxDataEntryWaiterToggleBackground.transform.GetChild(0).GetComponent<Image>();
    }

    private void SaveDEAddRxDataToNewAssemblyEntry()
    {
        {
            GameObject patientPanel = rxDEAddRxInfoPanel.transform.GetChild(0).gameObject;
            string patientTxt = patientPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneAEntryNameBtn.GetComponentInChildren<Text>().text = patientTxt;

            GameObject drugPanel = rxDEAddRxInfoPanel.transform.GetChild(2).gameObject;
            string drugTxt = drugPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneAEntryDrugBtn.GetComponentInChildren<Text>().text = drugTxt;

            GameObject waiterPanel = rxDEAddRxInfoPanel.transform.GetChild(8).gameObject;
            bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
            cloneAEntryWaiterToggle.GetComponent<Toggle>().isOn = waiter;

            cloneAEntryID.GetComponentInChildren<Text>().text = de_LastAddRxId;
        }
    }

    private void PopulateRxDEAddRxDrugDropdownValues()
    {
        List<string> drugNames = new List<string>();
        drugNames.Add("Select drug");
        foreach (var drug in DrugDatabase.drugNames)
        {
            drugNames.Add(drug);
        }
        rxDEaddRxDrugDropdown.AddOptions(drugNames);
    }

    private void DestroyPreExistingRxEntry()
    {
        for (int i = 0; i < assemblyContent.transform.childCount; i++)
        {
            GameObject currentRxEntryClone = assemblyContent.transform.GetChild(i).gameObject;
            string currentRxIDTxt = currentRxEntryClone.transform.GetChild(0).transform.GetChild(4).GetComponent<Text>().text;

            if (currentRxIDTxt == AssemblyScript.lastModifiedId)
            {
                Destroy(assemblyContent.transform.GetChild(i).transform.gameObject);
                break;
            }
        }
    }

    private void DestroyPreExistingRxAssemblyPanel()
    {
        for (int i = 1; i < assemblyScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyPanel = assemblyScreen.transform.GetChild(i).gameObject;
            string currentAssemblyPanelIdTxt = currentAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text;

            if (currentAssemblyPanelIdTxt == AssemblyScript.lastModifiedId)
            {
                Destroy(assemblyScreen.transform.GetChild(i).gameObject);
                break;
            }
        }
    }

    private void DestroyRxImage()
    {
        for (int i = 1; i < rxDEAddRxPanel.transform.GetChild(0).childCount; i++)
        {
            Destroy(rxDEAddRxPanel.transform.GetChild(0).GetChild(i).transform.gameObject);
        }
    }

    private void UpdateQuantityDropdownValues()
    {
        //Refactor: Switch to loop
        switch (rxDEaddRxDrugDropdown.value)
        {
            case 1:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d1 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[1].text];
                List<string> dq1 = d1[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq1);
                break;
            case 2:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d2 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[2].text];
                List<string> dq2 = d2[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq2);
                break;
            case 3:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d3 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[3].text];
                List<string> dq3 = d3[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq3);
                break;
            case 4:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d4 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[4].text];
                List<string> dq4 = d4[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq4);
                break;
            case 5:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d5 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[5].text];
                List<string> dq5 = d5[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq5);
                break;
            case 6:
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d6 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[6].text];
                List<string> dq6 = d6[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq6);
                break;
        }
    }

    private void ResetDEAddRxPanelInputs()
    {
        rxDEAddRxInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = "";
        rxDEAddRxInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text = "";
        rxDEAddRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Dropdown>().value = 0;
        rxDEAddRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Dropdown>().value = 0;
        rxDEAddRxInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text = "";
        rxDEAddRxInfoPanel.transform.GetChild(5).GetChild(0).GetComponent<Toggle>().isOn = false;
        rxDEAddRxInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<Toggle>().isOn = false;
        rxDEAddRxInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text = "";
        rxDEAddRxInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<InputField>().text = "";
        rxDEAddRxInfoPanel.transform.GetChild(8).GetChild(0).GetComponent<Toggle>().isOn = false;
    }

    private void TransferNameFromRxDataEntryToAddRx()
    {
        GameObject currentRxDataEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string patientFullName = currentRxDataEntry.transform.GetChild(1).GetComponentInChildren<Text>().text;
        rxDEAddRxPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<InputField>().text = patientFullName;
    }


    private int rxDataEntries = 6;
    private static bool eRx;
    private static string de_LastAddRxId;
    private static System.Random rnd = new System.Random();

    #region Rx Data Entry Clones
    private GameObject cloneRxDataEntryObj;
    private GameObject cloneRxDataEntryPanel;
    private Toggle cloneRxDataEntryWaiterToggle;
    private GameObject cloneRxDataEntryWaiterToggleBackground;
    private GameObject cloneRxDataEntryNameBtn;
    private GameObject cloneRxDataEntryDrugBtn;
    private GameObject cloneRxDataEntryAddRxBtn;
    private GameObject cloneRxDataEntryID;
    #endregion

    #region Assembly Entry Clones
    private GameObject cloneAEntryObj;
    private GameObject cloneAEntryPanel;
    private Toggle cloneAEntryWaiterToggle;
    private GameObject cloneAEntryWaiterToggleBackground;
    private GameObject cloneAEntryNameBtn;
    private GameObject cloneAEntryDrugBtn;
    private GameObject cloneAEntryAssemblyBtn;
    private GameObject cloneAEntryID;
    #endregion

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

    #region Rx Image Clones
    private GameObject cloneRxImagePatient;
    private GameObject cloneRxImageDoctor;
    private GameObject cloneRxImageDrug;
    private GameObject cloneRxImageQuantity;
    private GameObject cloneRxImageRefills;
    private GameObject cloneRxImageGenOrBr;
    private GameObject cloneRxImageWritten;
    private GameObject cloneRxImageExp;
    private GameObject cloneRxImageSig;
    #endregion

    #region Assembly Panel Rx Image Clones
    private GameObject cloneAssemblyRxImagePatient;
    private GameObject cloneAssemblyRxImageDoctor;
    private GameObject cloneAssemblyRxImageDrug;
    private GameObject cloneAssemblyRxImageQuantity;
    private GameObject cloneAssemblyRxImageRefills;
    private GameObject cloneAssemblyRxImageGenOrBr;
    private GameObject cloneAssemblyRxImageWritten;
    private GameObject cloneAssemblyRxImageExp;
    private GameObject cloneAssemblyRxImageSig;
    #endregion
}
