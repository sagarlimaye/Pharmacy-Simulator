using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddRxScript : MonoBehaviour
{
    #region On Scene Objects
    public static GameObject assemblyScreen;
    public static GameObject profileScreen;
    public static GameObject addRxPanel;
    public static GameObject addRxImage;
    public static GameObject addRxScanPromptPanel;
    public static GameObject assemblyContent;
    public static GameObject profilesContent;
    public static GameObject addRxInfoPanel;
    public static Dropdown addRxDrugDropdown;
    public static Dropdown addRxQuantityDropdown;
    public static Toggle addRxBrandToggle;
    public static Toggle addRxGenericToggle;
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

    public void OnAddRx()
    {
        lastAddRxId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(7).GetComponentInChildren<Text>().text;

        ResetAddRxPanelInputs();
        TransferNameFromPatientEntryToAddRx();

        addRxPanel.SetActive(true);

        if(addRxImage.transform.childCount < 2)
        {
            addRxScanPromptPanel.SetActive(true);
            DisableAddRxWhenNoImage();
        }
        else
            addRxScanPromptPanel.SetActive(false);

    }

    public void OnOk()
    {
        if (AssemblyScript.lastModifiedId != null && lastAddRxId != AssemblyScript.lastModifiedId)
            lastAddRxId = AssemblyScript.lastModifiedId;

        DestroyPreExistingAssemblyEntry();
        DestroyPreExistingAssemblyPanel();
        InstantiateAssemblyEntry();
        SaveAddRxDataToNewAssemblyEntry();
        InstantiateAssemblyPanel();
        SaveAddRxDataToNewAssemblyPanel();
        ResetAddRxPanelInputs();
        DestroyRxImage();

        addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = true;
        addRxPanel.SetActive(false);
        profileScreen.SetActive(false);
        assemblyScreen.SetActive(true);

        AssemblyScript.lastModifiedId = null;
    }

    public void OnCancel()
    {
        addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = true;
        RxImagePresent = false;

        ResetAddRxPanelInputs();
        DestroyRxImage();

        addRxPanel.SetActive(false); 
    }

    public void OnScan()
    {
        EnableAddRxWhenImagePresent();
        InstantiateRxImage();
        GenerateRxImageData();
    }

    public void OnYesScan()
    {
        EnableAddRxWhenImagePresent();
        InstantiateRxImage();
        GenerateRxImageData();

        addRxScanPromptPanel.SetActive(false);
    }

    public void OnNoScan()
    {
        addRxScanPromptPanel.SetActive(false);
    }

    public void OnGenericOrBrand()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "BrandToggle")
            addRxGenericToggle.isOn = false;
        else if (EventSystem.current.currentSelectedGameObject.name == "GenericToggle")
            addRxBrandToggle.isOn = false;
    }

    public void OnAddRxDrugDropdownValueChange()
    {
        UpdateQuantityDropdownValues();
    }

    private void Start()
    {
        PopulateAddRxDrugDropdownValues();
    }

    private void Awake()
    {
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen"); 
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        addRxPanel = GameObject.FindGameObjectWithTag("AddRxPanel");
        addRxInfoPanel = GameObject.FindGameObjectWithTag("AddRxInfoPanel");
        addRxImage = GameObject.FindGameObjectWithTag("RxImage");
        addRxScanPromptPanel = GameObject.FindGameObjectWithTag("ScanPrompt");
        addRxDrugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        addRxQuantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
        addRxBrandToggle = GameObject.FindGameObjectWithTag("BrandToggle").GetComponent<Toggle>();
        addRxGenericToggle = GameObject.FindGameObjectWithTag("GenericToggle").GetComponent<Toggle>();

        addRxPanel.SetActive(false);
    }

    private void InstantiateRxImage()
    {
        cloneRxImagePatient = Instantiate(rxImagePatientPrefab, addRxImage.transform);
        cloneRxImageDoctor = Instantiate(rxImageDoctorPrefab, addRxImage.transform);
        cloneRxImageDrug = Instantiate(rxImageDrugPrefab, addRxImage.transform);
        cloneRxImageQuantity = Instantiate(rxImageQuantityPrefab, addRxImage.transform);
        cloneRxImageRefills = Instantiate(rxImageRefillsPrefab, addRxImage.transform);
        cloneRxImageGenOrBr = Instantiate(rxImageGenOrBrPrefab, addRxImage.transform);
        cloneRxImageWritten = Instantiate(rxImageWrittenPrefab, addRxImage.transform);
        cloneRxImageExp = Instantiate(rxImageExpPrefab, addRxImage.transform);
        cloneRxImageSig = Instantiate(rxImageSigPrefab, addRxImage.transform);
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

        if (AssemblyScript.lastModifiedId == null)
        {
            //REFACTOR: Delete assignment, just instantiate??
            cloneAssemblyRxImagePatient = Instantiate(cloneRxImagePatient, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageDoctor = Instantiate(cloneRxImageDoctor, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageDrug = Instantiate(cloneRxImageDrug, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageQuantity = Instantiate(cloneRxImageQuantity, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageRefills = Instantiate(cloneRxImageRefills, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageGenOrBr = Instantiate(cloneRxImageGenOrBr, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageWritten = Instantiate(cloneRxImageWritten, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageExp = Instantiate(cloneRxImageExp, cloneApAssemblyPanel.transform.GetChild(0));
            cloneAssemblyRxImageSig = Instantiate(cloneRxImageSig, cloneApAssemblyPanel.transform.GetChild(0));
        }
    }

    private void DestroyRxImage()
    {
        for (int i = 1; i < addRxPanel.transform.GetChild(0).childCount; i++)
        {
            Destroy(addRxPanel.transform.GetChild(0).GetChild(i).transform.gameObject);
        }
    }

    private void DestroyPreExistingAssemblyEntry()
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

    private void DestroyPreExistingAssemblyPanel()
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

    private void GenerateRxImageData()
    {
        cloneRxImagePatient.GetComponentInChildren<Text>().text = addRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;

        for (int i = 3; i < (profilesContent.transform.childCount + 3); i++)
        {
            GameObject currentProfileClone = profileScreen.transform.GetChild(i).gameObject;
            string currentProfileFullName = currentProfileClone.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text + " "
                                        + currentProfileClone.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().text;

            if (currentProfileFullName == cloneRxImagePatient.GetComponentInChildren<Text>().text)
            {
                cloneRxImageDoctor.GetComponentInChildren<Text>().text = profileScreen.transform.GetChild(i).GetChild(1).GetChild(8).GetChild(1).GetComponent<InputField>().text;
                break;
            }
        }

        //Need to replace entirely for handwritten images
        cloneRxImageDrug.GetComponentInChildren<Text>().text = DrugDatabase.drugNames[rnd.Next(DrugDatabase.drugNames.Count)];
        List<string> quantities = DrugDatabase.drugInfo[cloneRxImageDrug.GetComponentInChildren<Text>().text][0];
        cloneRxImageQuantity.GetComponentInChildren<Text>().text = quantities[rnd.Next(quantities.Count)];
        cloneRxImageRefills.GetComponentInChildren<Text>().text = PrescriptionDatabase.GenerateRefill();
        //cloneRxImageGenOrBr.GetComponentInChildren<Text>().text = PrescriptionDatabase.GenerateGenOrBr();
        cloneRxImageWritten.GetComponentInChildren<Text>().text = PrescriptionDatabase.RandomDay().ToString();
        //cloneRxImageExp.GetComponentInChildren<Text>().text = PrescriptionDatabase.FutureRandomDay().ToString();
        cloneRxImageSig.GetComponentInChildren<Text>().text = PrescriptionDatabase.GenerateSig();
    }

    private void SaveAddRxDataToNewAssemblyPanel()
    {
        UpdateAssemblyPanelPriceAndId();

        GameObject patientPanel = addRxInfoPanel.transform.GetChild(0).gameObject;
        string patientTxt = patientPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
        cloneApPatientInputField.GetComponentInChildren<InputField>().text = patientTxt;

        GameObject drPanel = addRxInfoPanel.transform.GetChild(1).gameObject;
        string drTxt = drPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
        cloneApDoctorInputField.GetComponentInChildren<InputField>().text = drTxt;

        GameObject drugPanel = addRxInfoPanel.transform.GetChild(2).gameObject;
        int drugValue = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string drugNameSelected = drugPanel.transform.GetChild(1).GetComponent<Dropdown>().options[drugValue].text;
        cloneApDrugDropdown.ClearOptions();
        cloneApDrugDropdown.value = drugValue;
        cloneApDrugDropdown.AddOptions(new List<string>() { drugNameSelected });

        GameObject quantityPanel = addRxInfoPanel.transform.GetChild(3).gameObject;
        int quantityValue = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().value;
        string quantitySelected = quantityPanel.transform.GetChild(1).GetComponent<Dropdown>().options[quantityValue].text;
        cloneApQuantityDropdown.ClearOptions();
        cloneApQuantityDropdown.value = quantityValue;
        cloneApQuantityDropdown.AddOptions(new List<string>() { quantitySelected });

        GameObject refillPanel = addRxInfoPanel.transform.GetChild(4).gameObject;
        string refillTxt = refillPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
        cloneApRefillInputField.GetComponentInChildren<InputField>().text = refillTxt;

        GameObject bgPanel = addRxInfoPanel.transform.GetChild(5).gameObject;
        bool brand = bgPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        bool generic = bgPanel.transform.GetChild(1).GetComponent<Toggle>().isOn;
        cloneApBrandToggle.GetComponent<Toggle>().isOn = brand;
        cloneApGenericToggle.GetComponent<Toggle>().isOn = generic;

        GameObject writtenPanel = addRxInfoPanel.transform.GetChild(6).gameObject;
        string writtenTxt = writtenPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
        cloneApWrittenInputField.GetComponentInChildren<InputField>().text = writtenTxt;

        GameObject expPanel = addRxInfoPanel.transform.GetChild(7).gameObject;
        string expTxt = expPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
        cloneApExpInputField.GetComponentInChildren<InputField>().text = expTxt;

        GameObject waiterPanel = addRxInfoPanel.transform.GetChild(8).gameObject;
        bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
        cloneApWaiterToggle.GetComponent<Toggle>().isOn = waiter;

        cloneApAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text = lastAddRxId;

        //If RxImage previously created, transfer to assembly panel
        if (AssemblyScript.lastModifiedId != null)
        {
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(1).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(2).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(3).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(4).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(5).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(6).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(7).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
            Instantiate(addRxPanel.transform.GetChild(0).GetChild(8).gameObject, cloneApAssemblyPanel.transform.GetChild(0));
        }
    }

    private void SaveAddRxDataToNewAssemblyEntry()
    {
        {
            GameObject patientPanel = addRxInfoPanel.transform.GetChild(0).gameObject;
            string patientTxt = patientPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
            cloneAEntryNameBtn.GetComponentInChildren<Text>().text = patientTxt;

            GameObject drugPanel = addRxInfoPanel.transform.GetChild(2).gameObject;
            string drugTxt = drugPanel.transform.GetChild(1).GetComponentInChildren<Text>().text;
            cloneAEntryDrugBtn.GetComponentInChildren<Text>().text = drugTxt;

            GameObject waiterPanel = addRxInfoPanel.transform.GetChild(8).gameObject;
            bool waiter = waiterPanel.transform.GetChild(0).GetComponent<Toggle>().isOn;
            cloneAEntryWaiterToggle.GetComponent<Toggle>().isOn = waiter;

            cloneAEntryID.GetComponentInChildren<Text>().text = lastAddRxId;
        }
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
        for (int i = 1; i < DrugDatabase.drugNames.Count; i++)
        {
            if (addRxDrugDropdown.value == i)
            {
                List<List<string>> drugInfo = DrugDatabase.drugInfo[addRxDrugDropdown.options[i].text];
                List<string> drugPrices = drugInfo[1];

                for (int j = 1; j < drugPrices.Count; j++)
                {
                    if (addRxQuantityDropdown.value == j)
                    {
                        cloneApPriceBtn.transform.GetComponentInChildren<Text>().text = drugPrices[j];
                        break;
                    }
                }
                break;
                       
            }
        }
    }

    private void PopulateAddRxDrugDropdownValues()
    {
        List<string> drugNames = new List<string>();
        drugNames.Add("Select drug");
        foreach (var drug in DrugDatabase.drugNames)
        {
            drugNames.Add(drug);
        }
        addRxDrugDropdown.AddOptions(drugNames);
    }

    private void UpdateQuantityDropdownValues()
    {
        //Refactor: Switch to loop
        switch (addRxDrugDropdown.value)
        {
            case 1:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d1 = DrugDatabase.drugInfo[addRxDrugDropdown.options[1].text];
                List<string> dq1 = d1[0];
                addRxQuantityDropdown.AddOptions(dq1);
                break;
            case 2:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d2 = DrugDatabase.drugInfo[addRxDrugDropdown.options[2].text];
                List<string> dq2 = d2[0];
                addRxQuantityDropdown.AddOptions(dq2);
                break;
            case 3:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d3 = DrugDatabase.drugInfo[addRxDrugDropdown.options[3].text];
                List<string> dq3 = d3[0];
                addRxQuantityDropdown.AddOptions(dq3);
                break;
            case 4:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d4 = DrugDatabase.drugInfo[addRxDrugDropdown.options[4].text];
                List<string> dq4 = d4[0];
                addRxQuantityDropdown.AddOptions(dq4);
                break;
            case 5:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d5 = DrugDatabase.drugInfo[addRxDrugDropdown.options[5].text];
                List<string> dq5 = d5[0];
                addRxQuantityDropdown.AddOptions(dq5);
                break;
            case 6:
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select Quantity" });
                List<List<string>> d6 = DrugDatabase.drugInfo[addRxDrugDropdown.options[6].text];
                List<string> dq6 = d6[0];
                addRxQuantityDropdown.AddOptions(dq6);
                break;
        }
    }

    private void EnableAddRxWhenImagePresent()
    {
        //If there is an image already present, or one was just scanned, enable everything and disable Scan button
        addRxPanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Dropdown>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Dropdown>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(4).GetChild(1).GetComponent<InputField>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Toggle>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Toggle>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponent<InputField>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponent<InputField>().interactable = true;
        addRxPanel.transform.GetChild(1).GetChild(8).GetChild(0).GetComponent<Toggle>().interactable = true;

        //disable Scan button
        addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = false;
    }

    private void DisableAddRxWhenNoImage()
    {
        //If there is no Rx image, everything is disabled except the "Cancel" and "Scan" buttons
        addRxPanel.transform.GetChild(2).GetComponent<Button>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Dropdown>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Dropdown>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(4).GetChild(1).GetComponent<InputField>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Toggle>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Toggle>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponent<InputField>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponent<InputField>().interactable = false;
        addRxPanel.transform.GetChild(1).GetChild(8).GetChild(0).GetComponent<Toggle>().interactable = false;
    }

    private void ResetAddRxPanelInputs()
    {
        //REFACTOR: access these through addRxInfoPanel, too many tags
        addRxInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = "";
        addRxInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text = "";
        addRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Dropdown>().value = 0;
        addRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Dropdown>().value = 0;
        addRxInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text = "";
        addRxInfoPanel.transform.GetChild(5).GetChild(0).GetComponent<Toggle>().isOn = false;
        addRxInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<Toggle>().isOn = false;
        addRxInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text = "";
        addRxInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<InputField>().text = "";
        addRxInfoPanel.transform.GetChild(8).GetChild(0).GetComponent<Toggle>().isOn = false;
    }

    private static string lastAddRxId;
    private static bool RxImagePresent = false;
    private static System.Random rnd = new System.Random();

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
