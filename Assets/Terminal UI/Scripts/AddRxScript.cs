using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddRxScript : MonoBehaviour {

    public static GameObject addRxPanel;
    public static GameObject content;
    public static GameObject rxInfoPanel;
    public static InputField patientInput;
    public static InputField doctorInput;
    public static Dropdown drugDropdown;
    public static Dropdown quantityDropdown;
    public static InputField refillsInput;
    public static Toggle brandToggle;
    public static Toggle genericToggle;
    public static InputField writtenDateInput;
    public static InputField expirationDateInput;
    public static Toggle waiterToggle;

    public GameObject rxEntryObjPrefab;
    public GameObject rxEntryPanelPrefab;
    public Toggle waiterBtnPrefab;
    public GameObject waiterToggleBackgroundPrefab;
    public GameObject nameBtnPrefab;
    public GameObject drugBtnPrefab;
    public GameObject assemblyBtnPrefab;
    public GameObject idTxtPrefab;

    public void Awake ()
    {
        content = GameObject.FindGameObjectWithTag("RxContent");
        rxInfoPanel = GameObject.FindGameObjectWithTag("RxInfoPanel");
        addRxPanel = GameObject.FindGameObjectWithTag("AddRxPanel");
        patientInput = GameObject.FindGameObjectWithTag("PatientInput").GetComponent<InputField>();
        doctorInput = GameObject.FindGameObjectWithTag("DoctorInput").GetComponent<InputField>();
        drugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        quantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
        refillsInput = GameObject.FindGameObjectWithTag("RefillsInput").GetComponent<InputField>();
        brandToggle = GameObject.FindGameObjectWithTag("BrandToggle").GetComponent<Toggle>();
        genericToggle = GameObject.FindGameObjectWithTag("GenericToggle").GetComponent<Toggle>();
        writtenDateInput = GameObject.FindGameObjectWithTag("WrittenInput").GetComponent<InputField>();
        expirationDateInput = GameObject.FindGameObjectWithTag("ExpInput").GetComponent<InputField>();
        waiterToggle = GameObject.FindGameObjectWithTag("WaiterToggle").GetComponent<Toggle>();

        addRxPanel.SetActive(false);
    }
	
	// Update is called once per frame
	public void OnAddRx ()
    {
        addRxPanel.SetActive(true);
    }

    public void OnOk()
    {
        InstantiateRxEntry();
        SaveProfileDataToNewRxEntry();
        ResetAddRxPanelInputs();
        addRxPanel.SetActive(false);
    }

    public void OnCancel()
    {
        ResetAddRxPanelInputs();
        addRxPanel.SetActive(false);
    }

    private void InstantiateRxEntry()
    {
        cloneRxEntryObj = Instantiate(rxEntryObjPrefab, content.transform);
        cloneRxEntryPanel = Instantiate(rxEntryPanelPrefab, cloneRxEntryObj.transform);
        cloneWaiterToggle = Instantiate(waiterBtnPrefab, cloneRxEntryPanel.transform) as Toggle;
        cloneWaiterToggleBackground = Instantiate(waiterToggleBackgroundPrefab, cloneWaiterToggle.transform);
        cloneNameBtn = Instantiate(nameBtnPrefab, cloneRxEntryPanel.transform);
        cloneDrugBtn = Instantiate(drugBtnPrefab, cloneRxEntryPanel.transform);
        cloneAssemblyBtn = Instantiate(assemblyBtnPrefab, cloneRxEntryPanel.transform);
        //cloneIdTxtEntry = Instantiate(idTxtPrefab, cloneRxEntryPanel.transform);
    }

    private void SaveProfileDataToNewRxEntry()
    {
        {
            GameObject patientPanel = rxInfoPanel.transform.GetChild(0).gameObject;
            string patientTxt = patientPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneNameBtn.GetComponentInChildren<Text>().text = patientTxt;
            //rxInfoPanel.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = ""; //clear input field

            GameObject drugPanel = rxInfoPanel.transform.GetChild(2).gameObject;
            string drugTxt = drugPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            cloneDrugBtn.GetComponentInChildren<Text>().text = drugTxt;
            //rxInfoPanel.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = ""; //clear input field

            bool waiterStatus = waiter;
            cloneWaiterToggle.isOn = waiterStatus;

        }
    }

    private void ResetAddRxPanelInputs()
    {
        patientInput.text = "";
        doctorInput.text = "";
        drugDropdown.value = 0;
        quantityDropdown.value = 0;
        refillsInput.text = "";
        brandToggle.isOn = false;
        genericToggle.isOn = false;
        writtenDateInput.text = "";
        expirationDateInput.text = "";
        waiterToggle.isOn = false;
    }

    private bool waiter;
    private GameObject cloneRxEntryObj;
    private GameObject cloneRxEntryPanel;
    private Toggle cloneWaiterToggle;
    private GameObject cloneWaiterToggleBackground;
    private GameObject cloneNameBtn;
    private GameObject cloneDrugBtn;
    private GameObject cloneAssemblyBtn;
    private GameObject cloneIdTxtEntry;
}
