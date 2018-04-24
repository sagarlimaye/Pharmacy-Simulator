using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour
{

    public static string lastModifiedId;

    #region On Scene Objects
    public static GameObject assemblyScreen;
    public static GameObject profileScreen;
    public static GameObject assemblyContent;
    public static GameObject profilesContent;
    public static GameObject assemblyPanel;
    public static GameObject addRxPanel;
    public static GameObject deAddRxPanel;
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

    public AudioClip printingSound;
    public AudioClip wrongSound;
    public GameObject labelPrintingPrefab;

    public void OnAssemble()
    {
        SwitchPanelScript.panelOpen = true;

        GameObject currentRxEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentRxIDTxt = currentRxEntry.transform.GetChild(4).GetComponent<Text>().text;

        for (int i = 1; i < assemblyScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyClone = assemblyScreen.transform.GetChild(i).gameObject;
            string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

            if (currentRxIDTxt == currentAssemblyIdTxt)
            {
                assemblyScreen.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void OnDone()
    {
        if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge && !VerifyAssemblyPanelCheckedmarked())
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInput();
        }
        else
        {
            SwitchPanelScript.panelOpen = false;
            DestroyRxEntry();
            GameObject PrintingLabel = Instantiate(labelPrintingPrefab, assemblyScreen.transform);
            PrintingLabel.SetActive(true);
            SoundManager.instance.PlaySingle(printingSound);
            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);

            SwitchPanelScript.terminalOffButtonEnabled = true;
        }
    }

    public IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void OnModify()
    {
        if (ScenarioInfoScript.currentScenario != ScenarioInfoScript.Scenario.Challenge)
        {
            SoundManager.instance.PlaySingle(wrongSound);
            GuideButtonScript.OnWrongClickInPanel();
        }

        else
        {
            lastModifiedId = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().text;

            EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
            EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
            profileScreen.SetActive(true);
            addRxPanel.SetActive(true);
            addRxScanPromptPanel.SetActive(false);
            addRxPanel.transform.GetChild(5).GetComponent<Button>().interactable = false;

            RepopulateAddRxPanel();
        }
    }

    public static bool VerifyAssemblyPanelCheckedmarked()
    {
        GameObject currentAssemblyPanel = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

        //Keep variables in case I need them
        //playerCheckPatient = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckDoctor = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(1).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckDrug = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(2).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckQuantity = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(3).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckRefills = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(4).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckBrandOrGeneric = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(5).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckWritten = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(6).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckSig = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(7).GetChild(0).GetComponent<Toggle>().isOn;
        //playerCheckWaiter = currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(8).GetChild(0).GetComponent<Toggle>().isOn;

        List<GameObject> checks = new List<GameObject>();

        for (int i = 0; i < currentAssemblyPanel.transform.GetChild(2).GetChild(1).childCount; i++)
        {
            checks.Add(currentAssemblyPanel.transform.GetChild(2).GetChild(1).GetChild(i).GetChild(0).gameObject);
        }

        //if a toggle is not checked, play highlight animation
        foreach (var playerCheck in checks)
        {
            if (!playerCheck.GetComponent<Toggle>().isOn)
                playerCheck.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Active");
        }

        //if any toggle not checked, return false
        foreach (var playerCheck in checks)
        {
            if (!playerCheck.GetComponent<Toggle>().isOn)
                return false;
        }

        return true;
    }

    private void Awake()
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

    private void RepopulateAddRxPanel()
    {
        //Transfer input info from the assembly panel being modified, back to Add Rx panel
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

        //Loop through assembly panels, find last modified, transfer handwritten Rx image data back to Add Rx panel
        for (int i = 1; i < assemblyScreen.transform.childCount; i++)
        {
            GameObject currentAssemblyClone = assemblyScreen.transform.GetChild(i).gameObject;
            string currentAssemblyIdTxt = currentAssemblyClone.transform.GetChild(1).GetComponent<Text>().text;

            if (lastModifiedId == currentAssemblyIdTxt)
            {
                int assemblyRxImageChildCount = currentAssemblyClone.transform.GetChild(0).childCount;

                //if currentAssemblyClone child count == 21, then we are copying back handwritten Rx
                if (assemblyRxImageChildCount == 21)
                {
                    for (int j = 1; j < 14; j++)
                    {
                        var from = currentAssemblyClone.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text;
                        var to = addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text;

                        addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text =
                            currentAssemblyClone.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text;
                    }
                    break;
                }
                //if currentAssemblyClone child count == 35, then we are copying back eRx
                else if (assemblyRxImageChildCount == 36)
                {
                    //destroy existing handwritten rx that comes by default
                    for (int k = 0; k < 22; k++)
                        Destroy(addRxPanel.transform.GetChild(0).GetChild(k).gameObject);

                    Instantiate(eRxFromInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxFromPhoneInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxFromFaxInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxWrittenInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxPatientInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxDobInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxRxInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxQuantityInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxRefillsInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxSigInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxBrandSignaturePrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxGenericSignaturePrefab, addRxPanel.transform.GetChild(0));

                    Instantiate(eRxToPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxToInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxToPhonePrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxToPhoneInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxToFaxPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxToFaxInputPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxDivider1Prefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxFromTextPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxFromPhonePrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxFromFaxPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxWrittenPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxDivider2Prefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxPatientPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxDobPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxRxPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxQuantityPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxRefillsPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxSigPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxBrandLinePrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxOrPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxGenericLinePrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxBrandTextPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxGenericTextPrefab, addRxPanel.transform.GetChild(0));
                    Instantiate(eRxRxIconPrefab, addRxPanel.transform.GetChild(0));

                    //horrible hack: since handwritten objects are not destroyed until function returns, the indeces for assembly clone and the panel
                    //are offset by 22

                    for (int j = 22; j < 32; j++)
                    {
                        var from = currentAssemblyClone.transform.GetChild(0).GetChild(j-22).GetComponent<TextMeshProUGUI>().text;
                        var to = addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text;

                        addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<TextMeshProUGUI>().text =
                            currentAssemblyClone.transform.GetChild(0).GetChild(j-22).GetComponent<TextMeshProUGUI>().text;
                    }

                    //the signatures are text and not TextMesh
                    for (int j = 32; j < 34; j++)
                    {
                        var from = currentAssemblyClone.transform.GetChild(0).GetChild(j-22).GetComponent<Text>().text;
                        var to = addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<Text>().text;

                        addRxPanel.transform.GetChild(0).GetChild(j).GetComponent<Text>().text =
                            currentAssemblyClone.transform.GetChild(0).GetChild(j-22).GetComponent<Text>().text;
                    }
                    break;
                }
            }
        }
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

    #region Assembly panel input field entries & image info
    private static bool playerCheckPatient;
    private static bool playerCheckDoctor;
    private static bool playerCheckDrug;
    private static bool playerCheckQuantity;
    private static bool playerCheckRefills;
    private static bool playerCheckBrandOrGeneric;
    private static bool playerCheckWritten;
    private static bool playerCheckSig;
    private static bool playerCheckWaiter;
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
}

