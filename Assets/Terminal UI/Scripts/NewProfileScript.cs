using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class NewProfileScript : MonoBehaviour
{
    public enum Scenario { One, Two, Three, Off };
    public Scenario currentScenario;

    #region On Scene Objects
    public static InputField searchInput;
    public static GameObject newProfilePanel;
    public static GameObject patientInfoPanel;
    public static GameObject profilesContent;
    public static GameObject profileScreen;
    #endregion

    #region Patient Entry Public Prefabs
    public Button firstBtnPrefab;
    public Button lastBtnPrefab;
    public Button dobBtnPrefab;
    public Button phoneBtnPrefab;
    public Button addressBtnPrefab;
    public Button insBtnPrefab;
    public Button addRxPrefab;
    public Text idTxtPrefab;
    public GameObject patientEntryObjPrefab;
    public GameObject patientEntryPanelPrefab;
    #endregion

    #region Profile Panel Public Prefabs
    public GameObject profilePanelPrefab;
    public GameObject patientInfoPanelPrefab;
    public GameObject firstPanelPrefab;
    public GameObject lastPanelPrefab;
    public GameObject dobPanelPrefab;
    public GameObject phonePanelPrefab;
    public GameObject addressPanelPrefab;
    public GameObject insCompanyPanelPrefab;
    public GameObject memberPanelPrefab;
    public GameObject groupPanelPrefab;
    public GameObject pcpPanelPrefab;
    public GameObject pcpPhonePanelPrefab;
    public InputField firstInputPrefab;
    public InputField lastInputPrefab;
    public InputField dobInputPrefab;
    public InputField phoneInputPrefab;
    public InputField addressInputPrefab;
    public InputField insCompanyInputPrefab;
    public InputField memberInputPrefab;
    public InputField groupInputPrefab;
    public InputField pcpInputPrefab;
    public InputField pcpPhoneInputPrefab;
    public Button okBtnPrefab;
    public Button cancelBtnPrefab;
    #endregion

    public AudioClip wrongSound;

    private void InitializeRandomProfileGeneration()
    {
        for (int i = 0; i < patientEntries; i++)
        {

            InstantiatePatientEntry();

            cloneFirstBtn.GetComponentInChildren<Text>().text = PatientDatabase.firsts[rnd.Next(PatientDatabase.firsts.Count)];
            cloneLastBtn.GetComponentInChildren<Text>().text = PatientDatabase.lasts[rnd.Next(PatientDatabase.lasts.Count)];
            cloneDobBtn.GetComponentInChildren<Text>().text = PatientDatabase.dobs[rnd.Next(PatientDatabase.dobs.Count)];
            clonePhoneBtn.GetComponentInChildren<Text>().text = PatientDatabase.phones[rnd.Next(PatientDatabase.phones.Count)];
            cloneAddressBtn.GetComponentInChildren<Text>().text = PatientDatabase.addresses[rnd.Next(PatientDatabase.addresses.Count)];

            cloneIdTxtEntry.GetComponentInChildren<Text>().text = (i + 1).ToString();

            InstantiateProfilePanel();

            cloneFirstInput.GetComponentInChildren<InputField>().text = cloneFirstBtn.GetComponentInChildren<Text>().text;
            cloneLastInput.GetComponentInChildren<InputField>().text = cloneLastBtn.GetComponentInChildren<Text>().text;
            cloneDobInput.GetComponentInChildren<InputField>().text = cloneDobBtn.GetComponentInChildren<Text>().text;
            clonePhoneInput.GetComponentInChildren<InputField>().text = clonePhoneBtn.GetComponentInChildren<Text>().text;
            cloneAddressInput.GetComponentInChildren<InputField>().text = cloneAddressBtn.GetComponentInChildren<Text>().text;
            cloneInsCompanyInput.GetComponentInChildren<InputField>().text = PatientDatabase.insCompanies[rnd.Next(PatientDatabase.insCompanies.Count)];
            cloneMemberInput.GetComponentInChildren<InputField>().text = PatientDatabase.memberIds[rnd.Next(PatientDatabase.memberIds.Count)];
            cloneGroupInput.GetComponentInChildren<InputField>().text = PatientDatabase.groupIds[rnd.Next(PatientDatabase.groupIds.Count)];
            clonePcpInput.GetComponentInChildren<InputField>().text = PatientDatabase.pcps[rnd.Next(PatientDatabase.pcps.Count)];
            clonePcpPhoneInput.GetComponentInChildren<InputField>().text = PatientDatabase.pcpPhones[rnd.Next(PatientDatabase.pcpPhones.Count)];

            for (int j = 0; j < PatientDatabase.maleFirstNames.Count; j++)
            {
                if (cloneFirstInput.GetComponentInChildren<InputField>().text == PatientDatabase.maleFirstNames[j])
                {
                    cloneNewProfilePanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = maleAvatars[rnd.Next(maleAvatars.Length)];
                    break;
                }
                else
                    cloneNewProfilePanel.transform.GetChild(0).GetComponentInChildren<Image>().sprite = femaleAvatars[rnd.Next(femaleAvatars.Length)];
            }
            cloneIdTxtProfile.GetComponentInChildren<Text>().text = (i + 1).ToString();
        }
    }

    public void OnNewProfile()
    {
        if (ScenarioInfoScript.currentScenario == ScenarioInfoScript.Scenario.Two)
        {
            SoundManager.instance.PlaySingle(wrongSound);

            if (SwitchPanelScript.panelOpen)
                GuideButtonScript.OnWrongClickInPanel();
            else
                GuideButtonScript.OnWrongClick();
        }

        else
        {
            addingNewProfile = true;
            newProfilePanel.SetActive(true);
            patientInfoPanel.SetActive(true);
        }
    }
    public void OnAddRx()
    {
        newProfilePanel.SetActive(true);
        patientInfoPanel.SetActive(true);
    }
    public void OnCancel()
    {
        currentProfile = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        newProfilePanel.SetActive(false);
        if(currentProfile != null)
            currentProfile.SetActive(false);
        addingNewProfile = false;
    }

    public void OnOk()
    {
        if (addingNewProfile)
        {
            InstantiatePatientEntry();
            SaveProfileDataToNewEntry();
            InstantiateProfilePanel();
            SaveProfileDataToNewProfileClone();
            searchInput.text = "";
            newProfilePanel.SetActive(false);
            ResetNewProfileInputFields();
        }
        else //clicking OK after opening newProfile with "Edit"
        {
            ModifyExistingEntryFromProfile(); //uses personal info entered in profile and enters it to a new entry
        }

        addingNewProfile = false;
    }

    public void OnEdit()
    {
        currentPatientEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentEntryIDTxt = currentPatientEntry.transform.GetChild(7).GetComponent<Text>().text;

        for (int i = 3; i < (profilesContent.transform.childCount + 3); i++)
        {
            GameObject currentProfileClone = profileScreen.transform.GetChild(i).gameObject;
            string currentProfileIDTxt = currentProfileClone.transform.GetChild(4).gameObject.GetComponent<Text>().text;

            if (currentProfileIDTxt == currentEntryIDTxt)
            {
                profileScreen.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Awake()
    {
        searchInput = (InputField)GameObject.FindGameObjectWithTag("SearchInputField").GetComponent<InputField>();
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
        profileScreen = GameObject.FindGameObjectWithTag("ProfilesScreen");
        newProfilePanel = GameObject.FindGameObjectWithTag("NewProfilePanel");
        patientInfoPanel = GameObject.FindGameObjectWithTag("PatientInfoPanel");

        maleAvatars = Resources.LoadAll<Sprite>("ProfileAvatars/Male");
        femaleAvatars = Resources.LoadAll<Sprite>("ProfileAvatars/Female");

        InitializeRandomProfileGeneration();
        newProfilePanel.SetActive(false);
    }

    private void InstantiatePatientEntry()
    {
        clonePatientEntryObj = Instantiate(patientEntryObjPrefab, profilesContent.transform);
        clonePatientEntryPanel = Instantiate(patientEntryPanelPrefab, clonePatientEntryObj.transform);
        cloneFirstBtn = Instantiate(firstBtnPrefab, clonePatientEntryPanel.transform);
        cloneLastBtn = Instantiate(lastBtnPrefab, clonePatientEntryPanel.transform);
        cloneDobBtn = Instantiate(dobBtnPrefab, clonePatientEntryPanel.transform);
        clonePhoneBtn = Instantiate(phoneBtnPrefab, clonePatientEntryPanel.transform);
        cloneAddressBtn = Instantiate(addressBtnPrefab, clonePatientEntryPanel.transform);
        cloneInsBtn = Instantiate(insBtnPrefab, clonePatientEntryPanel.transform);
        cloneAddRxBtn = Instantiate(addRxPrefab, clonePatientEntryPanel.transform);
        cloneIdTxtEntry = Instantiate(idTxtPrefab, clonePatientEntryPanel.transform);
    }

    private void InstantiateProfilePanel()
    {
        cloneNewProfilePanel = Instantiate(profilePanelPrefab, profileScreen.transform);
        clonePatientInfoPanel = Instantiate(patientInfoPanelPrefab, cloneNewProfilePanel.transform);
        Instantiate(okBtnPrefab, cloneNewProfilePanel.transform);
        Instantiate(cancelBtnPrefab, cloneNewProfilePanel.transform);

        cloneFirstPanel = Instantiate(firstPanelPrefab, clonePatientInfoPanel.transform);
        cloneLastPanel = Instantiate(lastPanelPrefab, clonePatientInfoPanel.transform);
        cloneDobPanel = Instantiate(dobPanelPrefab, clonePatientInfoPanel.transform);
        clonePhonePanel = Instantiate(phonePanelPrefab, clonePatientInfoPanel.transform);
        cloneAddressPanel = Instantiate(addressPanelPrefab, clonePatientInfoPanel.transform);
        cloneInsCompanyPanel = Instantiate(insCompanyPanelPrefab, clonePatientInfoPanel.transform);
        cloneMemberPanel = Instantiate(memberPanelPrefab, clonePatientInfoPanel.transform);
        cloneGroupPanel = Instantiate(groupPanelPrefab, clonePatientInfoPanel.transform);
        clonePcpPanel = Instantiate(pcpPanelPrefab, clonePatientInfoPanel.transform);
        clonePcpPhonePanel = Instantiate(pcpPhonePanelPrefab, clonePatientInfoPanel.transform);

        cloneFirstInput = Instantiate(firstInputPrefab, cloneFirstPanel.transform);
        cloneLastInput = Instantiate(lastInputPrefab, cloneLastPanel.transform);
        cloneDobInput = Instantiate(dobInputPrefab, cloneDobPanel.transform);
        clonePhoneInput = Instantiate(phoneInputPrefab, clonePhonePanel.transform);
        cloneAddressInput = Instantiate(addressInputPrefab, cloneAddressPanel.transform);
        cloneInsCompanyInput = Instantiate(insCompanyInputPrefab, cloneInsCompanyPanel.transform);
        cloneMemberInput = Instantiate(memberInputPrefab, cloneMemberPanel.transform);
        cloneGroupInput = Instantiate(groupInputPrefab, cloneGroupPanel.transform);
        clonePcpInput = Instantiate(pcpInputPrefab, clonePcpPanel.transform);
        clonePcpPhoneInput = Instantiate(pcpPhoneInputPrefab, clonePcpPhonePanel.transform);
        cloneIdTxtProfile = Instantiate(idTxtPrefab, cloneNewProfilePanel.transform);
    }

    private void SaveProfileDataToNewEntry()
    {
        {
            patientEntryInputFields = new List<Button>() { cloneFirstBtn, cloneLastBtn, cloneDobBtn, clonePhoneBtn, cloneAddressBtn };
            for (int i = 0; i < patientEntryInputFields.Count; i++)
            {
                GameObject entryPanel = patientInfoPanel.transform.GetChild(i).gameObject;
                string inputTxt = entryPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
                patientEntryInputFields[i].GetComponentInChildren<Text>().text = inputTxt;
            }

            cloneIdTxtEntry.GetComponentInChildren<Text>().text = (profilesContent.transform.childCount - 1).ToString();
        }
    }

    private void SaveProfileDataToNewProfileClone()
    {
        {
            profileInputFields = new List<InputField>() { cloneFirstInput, cloneLastInput, cloneDobInput, clonePhoneInput, cloneAddressInput,
            cloneInsCompanyInput, cloneMemberInput, cloneGroupInput, clonePcpInput, clonePcpPhoneInput };

            for (int i = 0; i < profileInputFields.Count; i++)
            {
                GameObject entryPanel = patientInfoPanel.transform.GetChild(i).gameObject;
                string inputTxt = entryPanel.transform.GetChild(1).transform.GetComponentInChildren<InputField>().text;
                profileInputFields[i].GetComponentInChildren<InputField>().text = inputTxt;
            }

            cloneIdTxtProfile.GetComponentInChildren<Text>().text = (profilesContent.transform.childCount - 1).ToString();
        }
    }

    private void ModifyExistingEntryFromProfile()
    {
        currentProfile = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string currentProfileIDTxt = currentProfile.transform.GetChild(4).gameObject.GetComponent<Text>().text;

        for (int i = 0; i < profilesContent.transform.childCount; i++)
        {
            currentPatientEntry = profilesContent.transform.GetChild(i).transform.GetChild(0).gameObject;
            string currentEntryIDTxt = currentPatientEntry.transform.GetChild(7).gameObject.GetComponent<Text>().text;
            if (currentEntryIDTxt == currentProfileIDTxt)
            {
                GameObject currentPatientInfoPanel = currentProfile.transform.GetChild(1).gameObject;

                for (int j = 0; j < 5; j++)
                {
                    GameObject entryPanel = currentPatientInfoPanel.transform.GetChild(j).gameObject;
                    string inputTxt = entryPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
                    currentPatientEntry.transform.GetChild(j).transform.GetChild(0).gameObject.GetComponent<Text>().text = inputTxt;
                }
                break;
            }
        }
        currentProfile.SetActive(false);
    }

    private void ResetNewProfileInputFields()
    {
        patientInfoPanel.transform.GetChild(0).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(1).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(2).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(3).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(4).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(5).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(6).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(7).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(8).GetChild(1).GetComponent<InputField>().text = "";
        patientInfoPanel.transform.GetChild(9).GetChild(1).GetComponent<InputField>().text = "";
    }

    private int patientEntries = 20;
    private bool addingNewProfile;
    private GameObject currentProfile;
    private GameObject currentPatientEntry;
    private Sprite[] maleAvatars;
    private Sprite[] femaleAvatars;
    private static System.Random rnd = new System.Random();

    #region Patient Entry Clones 
    private Text cloneIdTxtEntry;
    private Button cloneFirstBtn;
    private Button cloneLastBtn;
    private Button cloneDobBtn;
    private Button clonePhoneBtn;
    private Button cloneAddressBtn;
    private Button cloneInsBtn;
    private Button cloneAddRxBtn;
    private List<Button> patientEntryInputFields;
    private GameObject clonePatientEntryObj;
    private GameObject clonePatientEntryPanel;
    #endregion

    #region Profile Panel Clones
    private Text cloneIdTxtProfile;
    private GameObject cloneNewProfilePanel;
    private GameObject clonePatientInfoPanel;
    private GameObject cloneFirstPanel;
    private GameObject cloneLastPanel;
    private GameObject cloneDobPanel;
    private GameObject clonePhonePanel;
    private GameObject cloneAddressPanel;
    private GameObject cloneInsCompanyPanel;
    private GameObject cloneMemberPanel;
    private GameObject cloneGroupPanel;
    private GameObject clonePcpPanel;
    private GameObject clonePcpPhonePanel;
    private InputField cloneFirstInput;
    private InputField cloneLastInput;
    private InputField cloneDobInput;
    private InputField clonePhoneInput;
    private InputField cloneAddressInput;
    private InputField cloneInsCompanyInput;
    private InputField cloneMemberInput;
    private InputField cloneGroupInput;
    private InputField clonePcpInput;
    private InputField clonePcpPhoneInput;
    private List<InputField> profileInputFields;
    #endregion


}
