using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddRxScript : MonoBehaviour
{
    #region On Scene Objects
    public static GameObject assemblyScreen;
    public static GameObject profileScreen;
    public static GameObject rxContent;
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
    public GameObject rxImageHwTemplatePrefab;
    public GameObject rxImageHwDrInputPrefab;
    public GameObject rxImageHwDrAddressInputPrefab;
    public GameObject rxImageHwDrPhoneInputPrefab;
    public GameObject rxImageHwDrFaxInputPrefab;
    public GameObject rxImageHwPatientInputPrefab;
    public GameObject rxImageHwWrittenInputPrefab;
    public GameObject rxImageHwPatientAddressInputPrefab;
    public GameObject rxImageHwDrugInputPrefab;
    public GameObject rxImageHwQuantityInputPrefab;
    public GameObject rxImageHwSigInputPrefab;
    public GameObject rxImageHwRefillsInputPrefab;
    public GameObject rxImageHwGenericInputPrefab;
    public GameObject rxImageHwBrandInputPrefab;
    public GameObject rxImageHwDotPrefab;
    public GameObject rxImageHwPatientTextPrefab;
    public GameObject rxImageHwAddressTextPrefab;
    public GameObject rxImageHwWrittenTextPrefab;
    public GameObject rxImageHwGenericTextPrefab;
    public GameObject rxImageHwBrandTextPrefab;
    public GameObject rxImageHwRefillsTextPrefab;
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

    public AudioClip wrongSound;

    public void OnAddRx()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two
            && EventSystem.current.currentSelectedGameObject.transform.parent != profilesContent.transform.GetChild(3).GetChild(0))
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClick();
        }

        else
        {
            SwitchPanelScript.panelOpen = true;

            //If adding Rx to a different profile, change Id and disable image. Once image generated for a profile it will persist throughout session
            if (lastAddRxId != EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(7).GetComponentInChildren<Text>().text)
            {
                lastAddRxId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(7).GetComponentInChildren<Text>().text;
                DeactivateRxImage();
            }

            ResetAddRxPanelInputs();
            TransferNameFromPatientEntryToAddRx();

            addRxPanel.SetActive(true);

            if (addRxImage.transform.GetChild(addRxImage.transform.childCount - 1).gameObject.activeInHierarchy)
            {
                addRxScanPromptPanel.SetActive(true);
                DisableAddRxWhenNoImage();
            }
            else
                addRxScanPromptPanel.SetActive(false);
        }
    }

    public void OnOk()
    {
        proAddRxOkAttempts++;

        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && SwitchPanelScript.s2part1)
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInPanel("Where are you going ? Please follow the bouncy yellow arrow...");
            proAddRxOkAttempts--;
        }

        else if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && SwitchPanelScript.s2part2 && !VerifyProAddRxPanelInfoCorrect())
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInput();
        }

        else
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

            addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = true;
            addRxPanel.SetActive(false);
            profileScreen.SetActive(false);
            assemblyScreen.SetActive(true);

            AssemblyScript.lastModifiedId = null;

            SwitchPanelScript.panelOpen = false;
        }
    }

    public void OnCancel()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInPanel();
        }

        else
        {
            addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = true;
            ResetAddRxPanelInputs();

            addRxPanel.SetActive(false);
        }
    }

    public void OnScan()
    {
        EnableAddRxWhenImagePresent();
        ActivateRxImage();
        GenerateRxImageData();
    }

    public void OnYesScan()
    {
        SwitchPanelScript.terminalOffButtonEnabled = true;
        
        EnableAddRxWhenImagePresent();
        ActivateRxImage();
        GenerateRxImageData();

        addRxScanPromptPanel.SetActive(false);
    }

    public void OnNoScan()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInPanel();
        }

        else
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

    public static bool VerifyProAddRxPanelInfoCorrect()
    {
        playerInputPatient = addRxInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text;
        playerInputDoctor = addRxInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text;
        int playerInputDrugValue = addRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Dropdown>().value;
        playerInputDrug = addRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Dropdown>().options[playerInputDrugValue].text;
        int playerInputQuantityValue = addRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Dropdown>().value;
        playerInputQuantity = addRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Dropdown>().options[playerInputQuantityValue].text;
        playerInputRefills = addRxInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text;
        if (addRxInfoPanel.transform.GetChild(5).GetChild(0).GetComponent<Toggle>().isOn)
        {
            playerInputBrand = true;
            playerInputGeneric = false;
        }
        if (addRxInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<Toggle>().isOn)
        {
            playerInputBrand = false;
            playerInputGeneric = true;
        }
        playerInputWritten = addRxInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text;
        playerInputSig = addRxInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<InputField>().text;

        rxImageDoctor = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        rxImagePatient = addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;
        rxImageWritten = addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text;
        rxImageDrug = addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text;
        rxImageQuantity = addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text;
        rxImageSig = addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text;
        rxImageRefills = addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text;
        if (addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text != "")
            rxImageBrand = true;
        else
            rxImageBrand = false;
        if (addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text != "")
            rxImageGeneric = true;
        else
            rxImageGeneric = false;

        //rxImageWaiter = Get waiter status from data entry
        if (playerInputPatient != rxImagePatient)
        {
            addRxInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        //If any input does not match the Rx image info, play animation highlighting input field red
        if (playerInputDoctor != rxImageDoctor)
        {
            addRxInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            //TODO: Implement red circle and arrows when 2+ incorrect attempts. Need rxImage info string length to size the red circle and place arrow
            //else if (addRxOkAttempts == 2)
            //{
            //    deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            //    deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;

            //    int strLength = deAddRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Length;

            //}
        }
        else
        {
            addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputDrug != rxImageDrug)
        {
            addRxInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputQuantity != rxImageQuantity)
        {
            addRxInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputRefills != rxImageRefills)
        {
            addRxInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputBrand != rxImageBrand)
        {
            addRxInfoPanel.transform.GetChild(5).GetChild(0).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputGeneric != rxImageGeneric)
        {
            addRxInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputWritten != rxImageWritten)
        {
            addRxInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = Color.black;
        }

        if (playerInputSig != rxImageSig)
        {
            addRxInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<Animator>().SetTrigger("Active");

            if (proAddRxOkAttempts == 1)
            {
                addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
                addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
        else
        {
            addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().color = Color.black;
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

    public static void DrugAndGenericChanForS2()
    {
        addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = DrugDatabase.drugNames[4];
        addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = "";
        addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
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
        rxContent = GameObject.FindGameObjectWithTag("RxContent");
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

    private void ActivateRxImage()
    {
        
        //Turn all elements of the handwritten rx image
        for (int i = 0; i < addRxImage.transform.childCount; i++)
        {
            addRxImage.transform.GetChild(i).gameObject.SetActive(true);
        }

        addRxImage.transform.GetChild(addRxImage.transform.childCount-1).gameObject.SetActive(false);
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

        cloneRxImageHwTemplate = Instantiate(rxImageHwTemplatePrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwDrInput = Instantiate(rxImageHwDrInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwDrAddressInput = Instantiate(rxImageHwDrAddressInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwDrPhoneInput = Instantiate(rxImageHwDrPhoneInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwDrFaxInput = Instantiate(rxImageHwDrFaxInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwPatientInput = Instantiate(rxImageHwPatientInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwWrittenInput = Instantiate(rxImageHwWrittenInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwPatientAddressInput = Instantiate(rxImageHwPatientAddressInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwDrugInput = Instantiate(rxImageHwDrugInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwQuantityInput = Instantiate(rxImageHwQuantityInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwSigInput = Instantiate(rxImageHwSigInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwRefillsInput = Instantiate(rxImageHwRefillsInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwGenericInput = Instantiate(rxImageHwGenericInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        cloneRxImageHwBrandInput = Instantiate(rxImageHwBrandInputPrefab, cloneApAssemblyPanel.transform.GetChild(0));

        Instantiate(rxImageHwDotPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwAddressTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwPatientTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwWrittenTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwGenericTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwBrandTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));
        Instantiate(rxImageHwRefillsTextPrefab, cloneApAssemblyPanel.transform.GetChild(0));

        for (int i = 0; i < cloneApAssemblyPanel.transform.GetChild(0).childCount; i++)
        {
            cloneApAssemblyPanel.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }
    }

    private void DeactivateRxImage()
    {
        for (int i = 0; i < addRxImage.transform.childCount; i++)
        {
            addRxImage.transform.GetChild(i).gameObject.SetActive(false);
        }

        addRxImage.transform.GetChild(addRxImage.transform.childCount - 1).gameObject.SetActive(true);
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
        //Get name from input field on AddRx panel (prepopulated), assign to patient name in rx image
        string currentPatientName = addRxPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text;
        addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = currentPatientName;

        for (int i = 3; i < (profilesContent.transform.childCount + 3); i++)
        {
            //Loop through all profiles and find the one belonging to patient in rx image
            GameObject profileClone = profileScreen.transform.GetChild(i).gameObject;
            string profileFullName = profileClone.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<InputField>().text + " "
                                        + profileClone.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<InputField>().text;

            if (addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text == profileFullName)
            {
                //Once found use profile info to populate rx image data
                string doctorName = profileClone.transform.GetChild(1).GetChild(8).GetChild(1).GetComponent<InputField>().text;
                string doctorPhone = profileClone.transform.GetChild(1).GetChild(9).GetChild(1).GetComponent<InputField>().text;
                string patientAddress = profileClone.transform.GetChild(1).GetChild(4).GetChild(1).GetComponent<InputField>().text;
                string doctorFax = doctorPhone.Remove(10) + "0000";

                addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = doctorName;
                addRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = doctorPhone;
                addRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = doctorFax;
                addRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = patientAddress;
                break;
            }
        }

        //Populate the remaining rx image data
        addRxImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PatientDatabase.addresses[rnd.Next(PatientDatabase.addresses.Count)];
        addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.RandomDay().Date.ToString("yyyy-MM-dd");

        //For scenario 2, patient #3 from the top, the drug will be Oxycodone/Acetaminophen initially, else drug is picked at random
        //also, the signature must be on "Generic Substitution"
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two && lastAddRxId == "3")
        {
            addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = DrugDatabase.drugNames[5];
            addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
            addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = DrugDatabase.drugNames[rnd.Next(DrugDatabase.drugNames.Count)];

            //If prescription is generic, place Dr. signature on the generic line and make brand line blank, and viceversa
            if (PrescriptionDatabase.GenerateGenOrBr())
            {
                addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
                addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = "";
                addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
            }
        }
        List<string> quantities = DrugDatabase.drugInfo[addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text][0];
        addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = quantities[rnd.Next(quantities.Count)];
        addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.GenerateSig();
        addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = PrescriptionDatabase.GenerateRefill();
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
        var a = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwDrInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwDrAddressInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwDrPhoneInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwDrFaxInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwPatientInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwWrittenInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwPatientAddressInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwDrugInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwQuantityInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwSigInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwRefillsInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwGenericInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text;
        cloneRxImageHwBrandInput.GetComponent<TextMeshProUGUI>().text = addRxImage.transform.GetChild(13).GetComponent<TextMeshProUGUI>().text;

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
        for (int i = 0; i < DrugDatabase.drugNames.Count + 1; i++)
        {
            if (addRxDrugDropdown.value == 0)
            {
                addRxQuantityDropdown.ClearOptions();
                return;
            }
            else if (addRxDrugDropdown.value == i)
            {
                addRxQuantityDropdown.ClearOptions();
                addRxQuantityDropdown.AddOptions(new List<string>() { "Select quantity" });
                List<List<string>> d1 = DrugDatabase.drugInfo[addRxDrugDropdown.options[i].text];
                List<string> dq1 = d1[0];
                addRxQuantityDropdown.AddOptions(dq1);
                break;
            }
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
    private static System.Random rnd = new System.Random();
    private static int proAddRxOkAttempts = 0;

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

    #region Handwritten Rx Image Clones
    private GameObject cloneRxImageHwTemplate;
    private GameObject cloneRxImageHwDrInput;
    private GameObject cloneRxImageHwDrAddressInput;
    private GameObject cloneRxImageHwDrPhoneInput;
    private GameObject cloneRxImageHwDrFaxInput;
    private GameObject cloneRxImageHwPatientInput;
    private GameObject cloneRxImageHwWrittenInput;
    private GameObject cloneRxImageHwPatientAddressInput;
    private GameObject cloneRxImageHwDrugInput;
    private GameObject cloneRxImageHwQuantityInput;
    private GameObject cloneRxImageHwSigInput;
    private GameObject cloneRxImageHwRefillsInput;
    private GameObject cloneRxImageHwGenericInput;
    private GameObject cloneRxImageHwBrandInput;
    #endregion

    #region Add Rx panel input field entries & image info
    private static string playerInputPatient;
    private static string playerInputDoctor;
    private static string playerInputDrug;
    private static string playerInputQuantity;
    private static string playerInputRefills;
    private static bool playerInputBrand;
    private static bool playerInputGeneric;
    private static string playerInputWritten;
    private static string playerInputSig;
    private static string rxImagePatient;
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
