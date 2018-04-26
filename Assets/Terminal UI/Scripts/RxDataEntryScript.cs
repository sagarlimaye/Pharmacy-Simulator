using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public static GameObject deAddRxPanel;
    public static GameObject deAddRxImage;
    public static GameObject rxDEAddRxInfoPanel;
    public static Dropdown rxDEaddRxDrugDropdown;
    public static Dropdown rxDEaddRxQuantityDropdown;
    public static Toggle deAddRxBrandToggle;
    public static Toggle deAddRxGenericToggle;
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

    #region eRxImage Public Prefabs
    public GameObject eRxFromInputPrefab;
    public GameObject eRxFromPhoneInputPrefab;
    public GameObject eRxFromFaxInputPrefab;
    public GameObject eRxWrittenInputPrefab;
    public GameObject eRxPatientInputPrefab;
    public GameObject eRxDobInputPrefab;
    public GameObject eRxRxInputPrefab;
    public GameObject eRxQuantityInputPrefab;
    public GameObject eRxRefillsInputPrefab;
    public GameObject eRxSigInputPrefab;
    public GameObject eRxBrandSignaturePrefab;
    public GameObject eRxGenericSignaturePrefab;
    public GameObject eRxToPrefab;
    public GameObject eRxToInputPrefab;
    public GameObject eRxToPhonePrefab;
    public GameObject eRxToPhoneInputPrefab;
    public GameObject eRxToFaxPrefab;
    public GameObject eRxToFaxInputPrefab;
    public GameObject eRxDivider1Prefab;
    public GameObject eRxFromTextPrefab;
    public GameObject eRxFromPhonePrefab;
    public GameObject eRxFromFaxPrefab;
    public GameObject eRxWrittenPrefab;
    public GameObject eRxDivider2Prefab;
    public GameObject eRxPatientPrefab;
    public GameObject eRxDobPrefab;
    public GameObject eRxRxPrefab;
    public GameObject eRxQuantityPrefab;
    public GameObject eRxRefillsPrefab;
    public GameObject eRxSigPrefab;
    public GameObject eRxBrandLinePrefab;
    public GameObject eRxOrPrefab;
    public GameObject eRxGenericLinePrefab;
    public GameObject eRxBrandTextPrefab;
    public GameObject eRxGenericTextPrefab;
    public GameObject eRxRxIconPrefab;
    #endregion

    public delegate void RxDataEntryEvent(GameObject rxContent);
    public static event RxDataEntryEvent RxEntriesPopulated;
    public AudioClip wrongSound;

    public void OnAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One
            && EventSystem.current.currentSelectedGameObject.transform.parent != rxContent.transform.GetChild(0).GetChild(0))
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClick();
        }
        else
        {
            SwitchPanelScript.panelOpen = true;

            deAddRxPanel.SetActive(true);
            deLastAddRxId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(4).GetComponent<Text>().text;
            ResetDEAddRxPanelInputs();
            TransferNameFromRxDataEntryToAddRx();
            GenerateERxImageData();
        }
    }

    public void OnOk()
    {
        deAddRxOkAttempts++;

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One && !VerifyDeAddRxPanelInfoCorrect())
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInput();
        }

        else
        {
            if (AssemblyScript.lastModifiedId != null && deLastAddRxId != AssemblyScript.lastModifiedId)
                deLastAddRxId = AssemblyScript.lastModifiedId;

            InstantiateAssemblyEntry();
            SaveDEAddRxDataToNewAssemblyEntry();
            InstantiateAssemblyPanel();
            SaveDeAddRxDataToNewAssemblyPanel();
            ResetDEAddRxPanelInputs();

            deAddRxPanel.SetActive(false);
            rxScreen.SetActive(false);
            assemblyScreen.SetActive(true);

            AssemblyScript.lastModifiedId = null;

            SwitchPanelScript.panelOpen = false;
        }
    }

    public void OnCancel()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.One)
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInPanel();
        }
        else
        {
            ResetDEAddRxPanelInputs();
            deAddRxPanel.SetActive(false);
        }
    }

    public void OnRxDEAddRxDrugDropdownValueChange()
    {
        UpdateQuantityDropdownValues();
    }

    public static bool VerifyDeAddRxPanelInfoCorrect()
    {   
        //Get player inputs on Add Rx window for Data Entry Panel
        playerInputDoctor = deAddRxPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponentInChildren<InputField>().text;
        int playerInputDrugValue = deAddRxPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Dropdown>().value;
        playerInputDrug = deAddRxPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Dropdown>().options[playerInputDrugValue].text;
        int playerInputQuantityValue = deAddRxPanel.transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Dropdown>().value;
        playerInputQuantity = deAddRxPanel.transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Dropdown>().options[playerInputQuantityValue].text;
        playerInputRefills = deAddRxPanel.transform.GetChild(1).GetChild(4).GetChild(1).GetComponentInChildren<InputField>().text;
        if (deAddRxPanel.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Toggle>().isOn)
        {
            playerInputBrand = true;
            playerInputGeneric = false;
        }
        if (deAddRxPanel.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Toggle>().isOn)
        {
            playerInputBrand = false;
            playerInputGeneric = true;
        }

        playerInputWritten = deAddRxPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponentInChildren<InputField>().text;
        playerInputSig = deAddRxPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponentInChildren<InputField>().text;
        //playerInputWaiter = deAddRxPanel.transform.GetChild(1).GetChild(8).GetChild(0).GetComponent<Toggle>().isOn;

        rxImageDoctor = deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        rxImageWritten = deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
        rxImageDrug = deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text;
        rxImageQuantity = deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text;
        rxImageRefills = deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text;
        rxImageSig = deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text;
        if (deAddRxImage.transform.GetChild(10).GetComponent<Text>().text != "")
            rxImageBrand = true;
        else
            rxImageBrand = false;
        if (deAddRxImage.transform.GetChild(11).GetComponent<Text>().text != "")
            rxImageGeneric = true;
        else
            rxImageGeneric = false;

        //rxImageWaiter = Get waiter status from data entry

        //If any input does not match the Rx image info, play animation highlighting input field red
        if (playerInputDoctor != rxImageDoctor)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            //TODO: Implement red circle and arrows when 2+ incorrect attempts. Need rxImage info string length to size the red circle and place arrow
            //else if (addRxOkAttempts == 2)
            //{
            //    deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            //    deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;

            //    int strLength = deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length;

            //}
        }
        else
        {
            deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputDrug != rxImageDrug)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputQuantity != rxImageQuantity)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(3).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputRefills != rxImageRefills)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(4).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputBrand != rxImageBrand)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                    deAddRxImage.transform.GetChild(33).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                    deAddRxImage.transform.GetChild(33).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(33).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(33).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputGeneric != rxImageGeneric)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(5).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(34).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(34).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(34).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(34).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputWritten != rxImageWritten)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputSig != rxImageSig)
        {
            anim = deAddRxPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponent<Animator>();
            anim.SetTrigger("Active");

            if (deAddRxOkAttempts == 1)
            {
                deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        //if all inputs correct, return true
        if (playerInputDoctor == rxImageDoctor &&
                playerInputDrug == rxImageDrug &&
                playerInputQuantity == rxImageQuantity &&
                playerInputRefills == rxImageRefills &&
                playerInputBrand == rxImageBrand &&
                playerInputWritten == rxImageWritten &&
                playerInputSig == rxImageSig)
            {
                return true;
            }
        else
            {
                return false;
            }
    }

    public void OnGenericOrBrand()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "BrandToggle")
            deAddRxGenericToggle.isOn = false;
        else if (EventSystem.current.currentSelectedGameObject.name == "GenericToggle")
            deAddRxBrandToggle.isOn = false;
    }

    private void Awake()
    {
        rxScreen = GameObject.FindGameObjectWithTag("RxScreen");
        assemblyScreen = GameObject.FindGameObjectWithTag("AssemblyScreen");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
        assemblyContent = GameObject.FindGameObjectWithTag("AssemblyContent");
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        deAddRxPanel = GameObject.FindGameObjectWithTag("DataEntryAddRxPanel");
        deAddRxImage = GameObject.FindGameObjectWithTag("RxDEAddRxImage");
        deAddRxBrandToggle = GameObject.FindGameObjectWithTag("deBrandToggle").GetComponent<Toggle>();
        deAddRxGenericToggle = GameObject.FindGameObjectWithTag("deGenericToggle").GetComponent<Toggle>();
        rxDEAddRxInfoPanel = GameObject.FindGameObjectWithTag("DataEntryAddRxInfoPanel");
        rxDEaddRxDrugDropdown = GameObject.FindGameObjectWithTag("rxDEDrugDropdown").GetComponent<Dropdown>();
        rxDEaddRxQuantityDropdown = GameObject.FindGameObjectWithTag("rxDEQuantityDropdown").GetComponent<Dropdown>();

        deAddRxPanel.SetActive(false);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        InitializeRandomRxDataEntryGeneration();
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

        cloneERxFromInput = Instantiate(eRxFromInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxFromPhoneInput = Instantiate(eRxFromPhoneInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxFromFaxInput = Instantiate(eRxFromFaxInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxWrittenInput = Instantiate(eRxWrittenInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxPatientInput = Instantiate(eRxPatientInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxDobInput = Instantiate(eRxDobInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxRxInput = Instantiate(eRxRxInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxQuantityInput = Instantiate(eRxQuantityInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxRefillsInput = Instantiate(eRxRefillsInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxSigInput = Instantiate(eRxSigInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxBrandSignature = Instantiate(eRxBrandSignaturePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneERxGenericSignature = Instantiate(eRxGenericSignaturePrefab, cloneApAssemblyPanel.transform.GetChild(0));

        Instantiate(eRxToPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxToInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxToPhonePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxToPhoneInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxToFaxPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxToFaxInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxDivider1Prefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxFromTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxFromPhonePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxFromFaxPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxWrittenPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxDivider2Prefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxPatientPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxDobPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxRxPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxQuantityPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxRefillsPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxSigPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxBrandLinePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxOrPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxGenericLinePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxBrandTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxGenericTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(eRxRxIconPrefab, cloneApAssemblyPanel.transform.GetChild(0));
    }

    private void GenerateERxImageData()
    {
        deAddRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = deAddRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;


        //Find matching profile and get doctor name and phone. Generate fax
        for (int i = 3; i < (profilesContent.transform.childCount + 3); i++)
        {
            GameObject currentProfileClone = profileScreen.transform.GetChild(i).gameObject;
            string currentProfileFullName = currentProfileClone.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text + " "
                                        + currentProfileClone.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().text;

            if (currentProfileFullName == deAddRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text)
            {
                deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentProfileClone.transform.GetChild(1).GetChild(8).GetChild(1).GetComponent<InputField>().text;
                deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentProfileClone.transform.GetChild(1).GetChild(9).GetChild(1).GetComponent<InputField>().text;
                deAddRxImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Remove(10) + "0000";
                deAddRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = currentProfileClone.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<InputField>().text;

                break;
            }
        }

        //Find matching rx data entry and get drug name
        for (int i = 0; i < (rxContent.transform.childCount); i++)
        {
            GameObject currentRxDataEntry = rxContent.transform.GetChild(i).gameObject;
            string currentRxDataEntryFullName = currentRxDataEntry.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text;

            if (currentRxDataEntryFullName == deAddRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text)
            {
                deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = currentRxDataEntry.transform.GetChild(0).GetChild(2).GetComponentInChildren<Text>().text;
                break;
            }
        }

        List<string> quantities = DrugDatabase.drugInfo[deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text][0];
        deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = quantities[rnd.Next(quantities.Count)];
        deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.GenerateRefill();
        deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.RandomDay().Date.ToString("MM-dd-yyyy");
        deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.GenerateSig();

        if(PrescriptionDatabase.GenerateGenOrBr())
        {
            deAddRxImage.transform.GetChild(10).GetComponent<Text>().text = deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            deAddRxImage.transform.GetChild(11).GetComponent<Text>().text = "";

        }
        else
        {
            deAddRxImage.transform.GetChild(11).GetComponent<Text>().text = deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            deAddRxImage.transform.GetChild(10).GetComponent<Text>().text = "";

        }
    }

    private void SaveDeAddRxDataToNewAssemblyPanel()
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

        cloneApAssemblyPanel.transform.GetChild(1).GetComponent<Text>().text = deLastAddRxId;

        cloneERxFromInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        cloneERxFromPhoneInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        cloneERxFromFaxInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        cloneERxWrittenInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
        cloneERxPatientInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text;
        cloneERxDobInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;
        cloneERxRxInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text;
        cloneERxQuantityInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text;
        cloneERxRefillsInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text;
        cloneERxSigInput.GetComponent<TextMeshProUGUI>().text = deAddRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text;
        cloneERxBrandSignature.GetComponent<Text>().text = deAddRxImage.transform.GetChild(10).GetComponent<Text>().text;
        cloneERxGenericSignature.GetComponent<Text>().text = deAddRxImage.transform.GetChild(11).GetComponent<Text>().text;
    }

    private void UpdateAssemblyPanelPriceAndId()
    {
        for (int i = 0; i < DrugDatabase.drugNames.Count; i++)
        {
            if (rxDEaddRxDrugDropdown.value == i+1)
            {
                List<List<string>> drugInfo = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[i+1].text];
                List<string> drugPrices = drugInfo[1];

                for (int j = 0; j < drugPrices.Count; j++)
                {
                    if (rxDEaddRxQuantityDropdown.value == j+1)
                    {
                        cloneApPriceBtn.transform.GetComponentInChildren<Text>().text = drugPrices[j];
                        break;
                    }
                }
                break;
            }
        }
    }

    private void InitializeRandomRxDataEntryGeneration()
    {
        List<int> randomUniqueNumbers = new List<int>();
        int number;

        for (int i = 0; i < rxDataEntries; i++)
        {
            do
            {
                number = rnd.Next(3, profileScreen.transform.childCount - 3);
            } while (randomUniqueNumbers.Contains(number));
            randomUniqueNumbers.Add(number);
        }

        foreach (int randomNumber in randomUniqueNumbers)
        {
            InstantiateRxDataEntry();

            GameObject randomProfileEntry = profileScreen.transform.GetChild(randomNumber).gameObject;
        
            cloneRxDataEntryNameBtn.GetComponentInChildren<Text>().text = randomProfileEntry.transform.GetChild(1).GetChild(0).GetComponentInChildren<InputField>().text
                                                + " " + randomProfileEntry.transform.GetChild(1).GetChild(1).GetComponentInChildren<InputField>().text;

            cloneRxDataEntryDrugBtn.GetComponentInChildren<Text>().text = DrugDatabase.drugNames[rnd.Next(DrugDatabase.drugNames.Count)];
            cloneRxDataEntryID.GetComponent<Text>().text = randomProfileEntry.transform.GetChild(4).GetComponent<Text>().text;
        }
        if (RxEntriesPopulated != null)
            RxEntriesPopulated(cloneRxDataEntryObj);
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

            cloneAEntryID.GetComponentInChildren<Text>().text = deLastAddRxId;
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

    private void DestroyPreExistingAssemblyEntry()
    {
        //Destroys any previous existing assembly entry with same "last modified" id to prevent duplication when modifying
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
        //Destroys any previous existing assembly panel with same "last modified" id to prevent duplication when modifying
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
        for (int i = 1; i < deAddRxPanel.transform.GetChild(0).childCount; i++)
        {
            Destroy(deAddRxPanel.transform.GetChild(0).GetChild(i).transform.gameObject);
        }
    }

    private void UpdateQuantityDropdownValues()
    {
        for (int i = 0; i < DrugDatabase.drugNames.Count + 1; i++)
        {
            if (rxDEaddRxDrugDropdown.value == 0)
            {
                rxDEaddRxQuantityDropdown.ClearOptions();
                return;
            }

            else if (rxDEaddRxDrugDropdown.value == i)
            {
                rxDEaddRxQuantityDropdown.ClearOptions();
                rxDEaddRxQuantityDropdown.AddOptions(new List<string>() { "Select quantity" });
                List<List<string>> d1 = DrugDatabase.drugInfo[rxDEaddRxDrugDropdown.options[i].text];
                List<string> dq1 = d1[0];
                rxDEaddRxQuantityDropdown.AddOptions(dq1);
                break;
            }
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
        deAddRxPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<InputField>().text = patientFullName;
    }

    private int rxDataEntries = 6;
    private static Animator anim;
    private static int deAddRxOkAttempts = 0;
    private static string deLastAddRxId;
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

    #region eRx Image clones
    private GameObject cloneERxFromInput;
    private GameObject cloneERxFromPhoneInput;
    private GameObject cloneERxFromFaxInput;
    private GameObject cloneERxWrittenInput;
    private GameObject cloneERxPatientInput;
    private GameObject cloneERxDobInput;
    private GameObject cloneERxRxInput;
    private GameObject cloneERxQuantityInput;
    private GameObject cloneERxRefillsInput;
    private GameObject cloneERxSigInput;
    private GameObject cloneERxBrandSignature;
    private GameObject cloneERxGenericSignature;
    #endregion

    #region Add Rx panel input field entries & image info
    private static string playerInputDoctor;
    private static string playerInputDrug;
    private static string playerInputQuantity;
    private static string playerInputRefills;
    private static bool playerInputBrand;
    private static bool playerInputGeneric;
    private static string playerInputWritten;
    private static string playerInputSig;
    private static string rxImageDoctor;
    private static string rxImageWritten;
    private static string rxImageDrug;
    private static string rxImageQuantity;
    private static string rxImageRefills;
    private static string rxImageSig;
    private static bool rxImageBrand;
    private static bool rxImageGeneric;
    #endregion
}
