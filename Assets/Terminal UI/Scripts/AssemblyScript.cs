using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour {

    #region On Scene Objects
    public static GameObject content;
    public static GameObject assemblyPanel;
    //public static InputField patientInput;
    //public static InputField doctorInput;
    //public static Dropdown drugDropdown;
    //public static Dropdown quantityDropdown;
    //public static InputField refillsInput;
    //public static Toggle brandToggle;
    //public static Toggle genericToggle;
    //public static InputField writtenDateInput;
    //public static InputField expirationDateInput;
    //public static Toggle waiterToggle;
    #endregion

    

    #region AddRx Public Prefabs
    //public GameObject rxEntryObjPrefab;
    //public GameObject rxEntryPanelPrefab;
    //public Toggle waiterBtnPrefab;
    //public GameObject waiterToggleBackgroundPrefab;
    //public GameObject nameBtnPrefab;
    //public GameObject drugBtnPrefab;
    //public GameObject assemblyBtnPrefab;
    //public GameObject idTxtPrefab;
    #endregion

    public void Awake()
    {
        content = GameObject.FindGameObjectWithTag("RxContent");
        assemblyPanel = GameObject.FindGameObjectWithTag("AssemblyPanel");

        //patientInput = GameObject.FindGameObjectWithTag("PatientInput").GetComponent<InputField>();
        //doctorInput = GameObject.FindGameObjectWithTag("DoctorInput").GetComponent<InputField>();
        //drugDropdown = GameObject.FindGameObjectWithTag("DrugDropdown").GetComponent<Dropdown>();
        //quantityDropdown = GameObject.FindGameObjectWithTag("QuantityDropdown").GetComponent<Dropdown>();
        //refillsInput = GameObject.FindGameObjectWithTag("RefillsInput").GetComponent<InputField>();
        //brandToggle = GameObject.FindGameObjectWithTag("BrandToggle").GetComponent<Toggle>();
        //genericToggle = GameObject.FindGameObjectWithTag("GenericToggle").GetComponent<Toggle>();
        //writtenDateInput = GameObject.FindGameObjectWithTag("WrittenInput").GetComponent<InputField>();
        //expirationDateInput = GameObject.FindGameObjectWithTag("ExpInput").GetComponent<InputField>();
        //waiterToggle = GameObject.FindGameObjectWithTag("WaiterToggle").GetComponent<Toggle>();

        assemblyPanel.SetActive(false);
    }

    // Update is called once per frame
    public void OnAssemble()
    {
        assemblyPanel.SetActive(true);
    }

    public void OnDone()
    {
        DestroyRxEntry();
        //ResetAssemblyPanelInputs();
        assemblyPanel.SetActive(false);
    }

    public void OnCancel()
    {
        //ResetAssemblyPanelInputs();
        assemblyPanel.SetActive(false);
    }

    private void DestroyRxEntry()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Toggle toggle = content.transform.GetChild(i).transform.GetChild(0).transform.GetChild(4).GetComponent<Toggle>();
            if (toggle.isOn)
            {
                Destroy(content.transform.GetChild(i).transform.gameObject);
            }
        }
    }

    private void ResetAssemblyPanelInputs()
    {
        //patientInput.text = "";
        //doctorInput.text = "";
        //drugDropdown.value = 0;
        //quantityDropdown.value = 0;
        //refillsInput.text = "";
        //brandToggle.isOn = false;
        //genericToggle.isOn = false;
        //writtenDateInput.text = "";
        //expirationDateInput.text = "";
        //waiterToggle.isOn = false;
    }

    private bool waiter;
    private GameObject currentRx;
    private GameObject cloneRxEntryObj;
    private GameObject cloneRxEntryPanel;
    private Toggle cloneWaiterToggle;
    private GameObject cloneWaiterToggleBackground;
    private GameObject cloneNameBtn;
    private GameObject cloneDrugBtn;
    private GameObject cloneAssemblyBtn;
    private GameObject cloneIdTxtEntry;
}

