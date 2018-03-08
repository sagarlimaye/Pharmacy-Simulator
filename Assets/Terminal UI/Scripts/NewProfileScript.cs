using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class NewProfileScript : MonoBehaviour
{
    public InputField searchInput;
    public GameObject profilePanel;
    public GameObject personalInfoPanel;
    public GameObject insuranceInfoPanel;
    public GameObject contentRectT;

    #region Patient Entry Public Prefabs
    public Button personalOrInsuranceBtn;
    public Button firstBtnPrefab;
    public Button lastBtnPrefab;
    public Button dobBtnPrefab;
    public Button phoneBtnPrefab;
    public Button addressBtnPrefab;
    public Button insBtnPrefab;
    public Button addRxBtnPrefab;
    public GameObject patientEntryObjPrefab;
    public GameObject patientEntryPanelPrefab;
    #endregion

    #region Profile Panel Public Prefabs
    public GameObject profilePanelPrefab;
    public GameObject personalPanelPrefab;
    public GameObject insurancePanelPrefab;
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
    #endregion

    public void Start()
    {
        for (int i = 0; i < patientEntries; i++)
        {
            InstantiatePatientEntry();

            newFirstBtn.GetComponentInChildren<Text>().text = firsts[rnd.Next(firsts.Count)];
            newLastBtn.GetComponentInChildren<Text>().text = lasts[rnd.Next(lasts.Count)];
            newDobBtn.GetComponentInChildren<Text>().text = dobs[rnd.Next(dobs.Count)];
            newPhoneBtn.GetComponentInChildren<Text>().text = phones[rnd.Next(phones.Count)];
            newAddressBtn.GetComponentInChildren<Text>().text = addresses[rnd.Next(addresses.Count)];

            InstantiateProfilePanel();

            newFirstInput.GetComponentInChildren<Text>().text = newFirstBtn.GetComponentInChildren<Text>().text;
            newLastInput.GetComponentInChildren<Text>().text = newLastBtn.GetComponentInChildren<Text>().text;
            newDobInput.GetComponentInChildren<Text>().text = newDobBtn.GetComponentInChildren<Text>().text;
            newPhoneInput.GetComponentInChildren<Text>().text = newPhoneBtn.GetComponentInChildren<Text>().text;
            newAddressInput.GetComponentInChildren<Text>().text = newAddressBtn.GetComponentInChildren<Text>().text;
            newInsCompanyInput.GetComponentInChildren<Text>().text = insCompanies[rnd.Next(insCompanies.Count)];
            newMemberInput.GetComponentInChildren<Text>().text = memberIds[rnd.Next(memberIds.Count)];
            newGroupInput.GetComponentInChildren<Text>().text = groupIds[rnd.Next(groupIds.Count)];
            newPcpInput.GetComponentInChildren<Text>().text = pcps[rnd.Next(pcps.Count)];
            newPcpPhoneInput.GetComponentInChildren<Text>().text = pcpPhones[rnd.Next(pcpPhones.Count)];
        }
    }

    public void Awake ()
    {
        profilePanel.SetActive(false);
        personalInfoPanel.SetActive(false);
        insuranceInfoPanel.SetActive(false);
    }
    
    public void OnNewProfile()
    {
        addingNewProfile = true;
        profilePanel.SetActive(true);
        personalInfoPanel.SetActive(true);
    }
    public void OnCancel()
    {
        profilePanel.SetActive(false);
        addingNewProfile = false;
    }
    public void OnPersonalOrInsurance()
    {
        if (personalOn)
        {
            insuranceInfoPanel.SetActive(true);
            personalInfoPanel.SetActive(false);
            personalOn = false;
            personalOrInsuranceBtn.GetComponentInChildren<Text>().text = "Personal Info";
        }
        else
        {
            insuranceInfoPanel.SetActive(false);
            personalInfoPanel.SetActive(true);
            personalOn = true;
            personalOrInsuranceBtn.GetComponentInChildren<Text>().text = "Insurance Info";
        }
    }

    public void OnOk()
    {
        if (addingNewProfile)
        {
            InstantiatePatientEntry();
            InstantiateProfilePanel();
            searchInput.text = "";
        }
        PopulateNewPatientEntry(); //uses personal info entered in profile and enters it to a new entry
        PopulateNewProfilePanel();

        profilePanel.SetActive(false);
        addingNewProfile = false;
    }

    public void OnEdit()
    {

        GameObject currentPatientEntry = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;

        currentPatientEntry.transform.GetChild(7).gameObject.SetActive(true);
    }

    private void InstantiatePatientEntry()
    {
        newPatientEntryObj = Instantiate(patientEntryObjPrefab, contentRectT.transform);
        newPatientEntryPanel = Instantiate(patientEntryPanelPrefab, newPatientEntryObj.transform);
        newFirstBtn = Instantiate(firstBtnPrefab, newPatientEntryPanel.transform);
        newLastBtn = Instantiate(lastBtnPrefab, newPatientEntryPanel.transform);
        newDobBtn = Instantiate(dobBtnPrefab, newPatientEntryPanel.transform);
        newPhoneBtn = Instantiate(phoneBtnPrefab, newPatientEntryPanel.transform);
        newAddressBtn = Instantiate(addressBtnPrefab, newPatientEntryPanel.transform);
        Instantiate(insBtnPrefab, newPatientEntryPanel.transform);
        Instantiate(addRxBtnPrefab, newPatientEntryPanel.transform);
    }

    private void PopulateNewPatientEntry()
    {
        newEntryFields = new List<Button>() { newFirstBtn, newLastBtn, newDobBtn, newPhoneBtn, newAddressBtn };
        for (int i = 0; i < personalInfoPanel.transform.childCount; i++)
        {
            GameObject entryPanel = personalInfoPanel.transform.GetChild(i).gameObject;
            string inputTxt = entryPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            newEntryFields[i].GetComponentInChildren<Text>().text = inputTxt;
        }
    }

    private void InstantiateProfilePanel()
    {
        //places new profile under patientEntryPanel containing button clicked
        newProfilePanel = Instantiate(profilePanelPrefab, newPatientEntryPanel.transform.parent); //mistake here
        newPersonalPanel = Instantiate(personalPanelPrefab, newProfilePanel.transform);
        newInsurancePanel = Instantiate(insurancePanelPrefab, newProfilePanel.transform);
        newfirstPanel = Instantiate(firstPanelPrefab, newPersonalPanel.transform);
        newLastPanel = Instantiate(lastPanelPrefab, newPersonalPanel.transform);
        newDobPanel = Instantiate(dobPanelPrefab, newPersonalPanel.transform);
        newPhonePanel = Instantiate(phonePanelPrefab, newPersonalPanel.transform);
        newAddressPanel = Instantiate(addressPanelPrefab, newPersonalPanel.transform);
        newInsCompanyPanel = Instantiate(insCompanyPanelPrefab, newInsurancePanel.transform);
        newMemberPanel = Instantiate(memberPanelPrefab, newInsurancePanel.transform);
        newGroupPanel = Instantiate(groupPanelPrefab, newInsurancePanel.transform);
        newPcpPanel = Instantiate(pcpPanelPrefab, newInsurancePanel.transform);
        newPcpPhonePanel = Instantiate(pcpPhonePanelPrefab, newInsurancePanel.transform);
        newFirstInput = Instantiate(firstInputPrefab, newfirstPanel.transform);
        newLastInput = Instantiate(lastInputPrefab, newLastPanel.transform);
        newDobInput = Instantiate(dobInputPrefab, newDobPanel.transform);
        newPhoneInput = Instantiate(phoneInputPrefab, newPhonePanel.transform);
        newAddressInput = Instantiate(addressInputPrefab, newAddressPanel.transform);
        newInsCompanyInput = Instantiate(insCompanyInputPrefab, newInsCompanyPanel.transform);
        newMemberInput = Instantiate(memberInputPrefab, newMemberPanel.transform);
        newGroupInput = Instantiate(groupInputPrefab, newGroupPanel.transform);
        newPcpInput = Instantiate(pcpInputPrefab, newPcpPanel.transform);
        newPcpPhoneInput = Instantiate(pcpPhoneInputPrefab, newPcpPhonePanel.transform);
    }

    public void PopulateNewProfilePanel()
    {
        newPersonalInputFields = new List<InputField>() { newFirstInput, newLastInput, newDobInput, newPhoneInput, newAddressInput };
        newInsInputFields = new List<InputField>() { newInsCompanyInput, newMemberInput, newGroupInput, newPcpInput, newPcpPhoneInput };

        for (int i = 0; i < newPersonalPanel.transform.childCount; i++)
        {
            GameObject profileSubPanel = newPersonalPanel.transform.GetChild(i).gameObject;
            string inputTxt = profileSubPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            newPersonalInputFields[i].GetComponentInChildren<Text>().text = inputTxt;
        }
        for (int i = 0; i < newInsurancePanel.transform.childCount; i++)
        {
            GameObject profileSubPanel = newInsurancePanel.transform.GetChild(i).gameObject;
            string inputTxt = profileSubPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            newInsInputFields[i].GetComponentInChildren<Text>().text = inputTxt;
        }
    }

    private int patientEntries = 50;
    private bool personalOn = true;
    private bool addingNewProfile;
    private static System.Random rnd = new System.Random();
    

    #region Patient Entry Private Properties  
    private Button newFirstBtn;
    private Button newLastBtn;
    private Button newDobBtn;
    private Button newPhoneBtn;
    private Button newAddressBtn;
    private List<Button> newEntryFields;
    private GameObject newPatientEntryObj;
    private GameObject newPatientEntryPanel;
    #endregion

    #region Profile Panel Private Properties
    private GameObject newProfilePanel;
    private GameObject newPersonalPanel;
    private GameObject newInsurancePanel;
    private GameObject newfirstPanel;
    private GameObject newLastPanel;
    private GameObject newDobPanel;
    private GameObject newPhonePanel;
    private GameObject newAddressPanel;
    private GameObject newInsCompanyPanel;
    private GameObject newMemberPanel;
    private GameObject newGroupPanel;
    private GameObject newPcpPanel;
    private GameObject newPcpPhonePanel;
    private InputField newFirstInput;
    private InputField newLastInput;
    private InputField newDobInput;
    private InputField newPhoneInput;
    private InputField newAddressInput;
    private InputField newInsCompanyInput;
    private InputField newMemberInput;
    private InputField newGroupInput;
    private InputField newPcpInput;
    private InputField newPcpPhoneInput;
    private List<InputField> newPersonalInputFields;
    private List<InputField> newInsInputFields;
    #endregion

    #region Random Entry Generation Lists
    private List<string> firsts = new List<string>()
        {"Maria","Rose","Jana","Jennifer","Cassandra","Ashley","Tennille","Janice","Brett","Janie","Michael","Delmar","Daron","Lola",
        "Rachelle","Pandora","Angelica","Miranda","Adena","Dane","Zack","Joshua","Gerardo","Lucy","Barbara","Lincoln","Deedra","Valentin",
        "Vicent","Lana","Margarita","Carmela","Sindy","Thomas","Huy","Carrie","Shemika","Selena","Willie","Misty","Bertha","Maribel",
        "Carlos","Farah","Tory","Eliseo","Victor", "Jacob","Michelle","James","Deirdre","Julian","Joseph","Wendy","Jason","Anna","Edward",
        "Sophie","Cameron","Amanda","Kevin","Mary","Ruth","Isaac","Benjamin","Carol","Natalie","Brandon","Stewart","Phil","Harry","Adrian",
        "Tracey","Andrea","Adrian","Trevor","Frank","Michelle","Edward","Irene","Stephen","Hannah","Jason","Carl","Paul","Kimberly",
        "Lillian","Emily","Anthony","Kevin","Anne","Jake","Madeleine","Wendy","Natalie","Carolyn","Paul","David","Deirdre","Virginia",
        "Karen","Jennifer","Anna","Robert","Blake","Matt","Richard","Theresa","Lauren","Sophie","Melanie","Neil","Lucas","Brandon",
        "Yvonne","Yvonne","Victor","Victoria","Connor","Maria","Liam","Owen","Nathan","Wanda","Julian","Sebastian","Boris","Brian",
        "Nicholas","Maria","Victor","Amelia","Brian","Nathan","Kimberly","Austin","Isaac","Lisa","Madeleine","Joshua","Claire","Sarah",
        "Christopher","Alexandra","Jason","Maria","Maria","Caroline","Elizabeth","Lucas","Piers","Phil","Kimberly","Dorothy",
        "Nicholas","Piers","Jason","Piers","Grace","William","Kevin","Diane","Owen","Megan","Stewart","Victoria","Alison","Julian",
        "Gordon","Virginia","Eric","Audrey","Irene","Anthony","Dorothy","Anthony","Alexandra","Stephanie","Dan","Adam","Ruth",
        "Anthony","Lauren","Jessica","James","Brian","Alexander","Carl","Jennifer","Claire","Ryan","Jane","Trevor","Benjamin",
        "Heather","William","Vanessa","Ferguson","Abraham","Taylor","Roberts","Berry","Hughes","Mathis","Bower","Campbell","Miller",
        "Berry","Fraser","Hudson","Ferguson","Hardacre","Reid","Allan","Fisher","Roberts","Carr","Wallace","Hunter","Alsop",
        "Davies","Langdon","Dickens","Mills","MacLeod","Davies","Bell","Anderson","Cornish","Davidson","McLean","Bower","Roberts",
        "Kelly","Burgess","Kerr","McDonald","King","Sharp","Graham","Baker","Forsyth","McLean","McLean","Oliver","McDonald",
        "Hudson","Carr","Randall","Burgess","Baker","James","Simpson","Knox","Abraham","Black","Bell","Roberts","Hodges","Davidson",
        "Welch","Butler","Howard","Lambert","Jones","Sharp","Walker","Rutherford","Vaughan","Cameron","Mathis","Butler",
        "Avery","Cornish","Hart","Howard","Cameron","Campbell","Lee","Skinner","Hudson","Morrison","Roberts","Peters",
        "Wright","Hodges","MacLeod","Skinner","Parsons","Davidson","Morgan","Skinner","Kelly","Butler","Terry","Hamilton",
        "Knox","Short","Scott","Lewis","Johnston","Graham","Dyer","Turner","Welch","Rampling","Marshall","Metcalfe","Jackson",
        "Skinner","Avery","Dickens","Quinn","Arnold","Martin","Rampling","Stewart","Grant","Dyer","Ellison","Bond","Ball",
        "Rampling","Scott","Scott","Grant","Mills","Anderson","McGrath","Bailey","Peters","Hart","Ferguson","Anderson","Hardacre",
        "Young","Marshall","May","Burgess","Avery","Payne","Randall","Mackay","McGrath","May","Walsh","Greene"};
    private List<string> lasts = new List<string>()
        {"Frank","Isaac","Audrey","Eric","Abigail","Eric","Alison","Jan","Ian","Eric","Ian","Fiona","Faith","Mary","Chloe","Phil",
        "Ryan","Oliver","Nicholas","Christian","Peter","Eric","Oliver","Emma","Sean","Wendy","Dorothy","Karen","Tim","Megan","Isaac",
        "Max","Jacob","Nicola","Harry","Carol","Alison","Amelia","Tim","Eric","Peter","Joan","Adam","Penelope","Charles","John",
        "Jake","David","Sean","Anna","Connor","Victor","Benjamin","Alan","Stephanie","Jane","Virginia","Christopher","Sam","Fiona",
        "James","Eric","Lisa","Connor","Andrea","Richard","Michelle","Lauren","Joan","Alison","Thomas","Zoe","Julia","Neil",
        "Dominic","Yvonne","Isaac","Joanne","Joan","Jonathan","Ella","Alan","Emily","Carolyn","Wanda","Amelia","Robert","Karen",
        "Rachel","Rose","Amy","Jason","Thomas","Max","Stephanie","Diana","Zoe","Diane","William","Evan","Keith","Fiona",
        "James","Sally","Evan","Kimberly","Caroline","Victoria","Caroline","Olivia","Robert","Sophie","Stephanie","Ava","Sophie",
        "Natalie","Kevin","James","Amanda","Una","Adrian","Dylan","Stephen","Oliver","Lillian","Sally","Luke","Adam","Tracey","Matt",
        "Elizabeth","Edward","Katherine","Blake","Zoe","Blake","Victoria","Liam","Nathan","Jasmine","Peter","Ella","Stephen","Maria",
        "Edward","Yvonne","Theresa","Fiona","Carl","Frank","Adam","Trevor","Jan","Faith","Eric","Wanda","Matt","Ryan","Hannah",
        "Theresa","Karen","Lillian","Joshua","Felicity","Lauren","Isaac","Adam","Deirdre","Amy","Julia","Rachel","Gavin","Fiona",
        "Elizabeth","Christian","Chloe","Ella","Carol","Frank","Zoe","Victor","Ruth","Charles","William","Natalie","Joan","Elizabeth",
        "Abigail","Emma","Robert","Liam","Deirdre","Leah","Melanie","Natalie","Ella","Steven","Edward","Wendy","Joseph","Campbell",
        "May","Lawrence","Young","Paterson","MacDonald","Fraser","Gill","Berry","Mathis","Dowd","Graham","Thomson","Reid","Howard","Knox",
        "Bell","Mackay","Jackson","Edmunds","Clarkson","Wilkins","Brown","Hunter","Duncan","Chapman","Coleman","Kerr","Lee","Skinner",
        "Hemmings","Jackson","Piper","Blake","Miller","Ross","Miller","Powell","Peters","Sharp","Hamilton","Greene","Campbell",
        "Vaughan","Murray","Chapman","Wilkins","Lee","Stewart","Poole","Grant","Rees","Mackenzie","Forsyth","Parr","Edmunds","Reid",
        "Mitchell","Young","White","Manning","Harris","Mackay","King","Manning","Buckland","Bower","Lewis","Rees","Grant","Sutherland",
        "Mackenzie","Davies","Scott","Vaughan","Arnold","North","Ellison","Ince","Hart","Walsh","Clarkson","Reid","McDonald",
        "Johnston","Smith","Ince","Simpson","MacDonald","Anderson","Hemmings","Sharp","Parr","Abraham","Hunter","Kelly","Ogden","Bower",
        "Henderson","Scott","Sanderson","Clarkson","Morrison","Metcalfe","Sanderson","MacLeod","Vance","Gray","McDonald","Wright",
        "Lawrence","Fisher","Jackson","Hamilton","Chapman","Hart","MacDonald","Quinn","Buckland","Johnston","MacDonald","Taylor","McGrath",
        "Peters","Mackay","Short","Springer","McGrath","Dowd","Mackenzie","Peake","Lee","Hamilton","Greene","Rees","Ellison","Dyer",
        "Tucker","Lawrence","Lewis","Welch","Hughes","Baker","Kelly","Harris","Nash","Russell","Sharp","Harris","Taylor","Oliver",
        "Davies","Stewart","Ogden","Arnold","Hill","Nash","Robertson","Randall","Bower","Gill","Simpson","Lee","Dowd","Sutherland",
        "Payne","Clarkson","Peake","McDonald","Kelly","Underwood","Glover","King","Hughes","Welch","Brown","Sanderson","McGrath",
        "Bailey","Powell","MacLeod","Fisher","Terry","Ince","Payne","Russell","Bailey","Lee","Murray","Sharp","Turner","Duncan",
        "Walsh","Manning","Avery","Poole","Bond","Graham","Parsons","Davidson"};
    private List<string> dobs = new List<string>()
        {"7/16/1995","3/16/2003","11/2/2010","1/19/2002","10/17/2000","4/28/1950","7/24/1958","7/7/1954","9/21/1942","3/23/2009",
        "7/15/2003","5/16/1963","12/25/1991","12/16/1974","1/3/1942","8/26/1979","8/10/1955","1/14/1953","10/4/2007","8/14/2008",
        "5/17/2006","4/9/1975","3/26/1946","5/21/2009","4/28/2007","7/25/1942","7/14/1991","8/26/1968","10/22/1956","5/2/1955",
        "8/4/1954","5/5/2005","8/20/1982","7/27/1966","12/24/1991","5/20/1962","7/28/2012","6/1/1966","11/23/1964","9/2/1953",
        "2/22/1968","10/22/1994","8/23/1943","10/9/1987","12/2/1959","9/23/2013","8/7/2002","10/18/2003","1/28/1962","12/5/1955",
        "7/28/1959","12/27/1992","1/15/1979","6/5/1977","6/9/2007","5/27/1966","4/9/2001","8/11/2010","12/3/1959","7/23/2015",
        "1/5/1961","12/8/1994","3/9/1949","1/16/1945","3/2/1965","9/14/2016","3/25/1943","5/14/1959","6/17/2003","9/23/1942",
        "2/26/1996","10/8/2002","9/20/1968","5/16/1987","10/28/1957","5/18/2011","8/23/1988","7/15/1980","1/7/2016","4/24/1960",
        "4/14/1948","12/25/1947","11/19/1994","10/27/2002","9/28/1990","8/1/2000","7/9/1996","4/14/1996","9/26/2004","5/8/2006",
        "3/20/1947","5/8/1953","7/15/1956","10/12/1961","11/14/1955","12/11/1971","6/9/1977","1/26/1998","5/13/1969","3/17/1973",
        "3/27/2000","6/4/2013","12/9/1981","1/7/1959","8/12/2001","2/13/1969","8/11/1989","8/14/1972","2/24/1963","5/14/2008",
        "7/10/1945","9/9/2015","10/27/2010","7/9/1942","6/14/2003","5/21/1948","11/7/1961","8/18/1985","3/12/1949","1/22/1998",
        "6/8/1989","1/5/1947","11/11/1945","1/14/1982","9/15/2007","7/15/2014","10/10/1987","6/25/1955","5/25/1989","5/23/1947",
        "6/27/1989","2/10/1977","12/23/1997","10/27/1952","2/22/1961","2/28/1963","6/20/1969","1/17/1974","12/16/1981","2/1/1956",
        "5/2/2009","1/26/2000","7/11/1989","7/5/2015","7/9/1948","7/2/1969","4/14/1970","4/21/1996","5/18/1944","9/12/2013",
        "1/28/1954","7/28/1960","12/13/1973","2/4/2003","4/23/1969","8/18/1987","4/7/1955","8/17/1984","8/28/1958","4/2/1973",
        "1/14/1943","8/20/2011","3/18/1968","10/3/1987","4/7/1940","11/1/1992","1/28/1996","7/28/1945","9/2/1999","5/14/1974",
        "4/9/1957","4/25/1985","12/16/2015","9/1/1957","4/27/2015","8/9/2001","3/25/1993","1/3/1992","4/21/1955","1/23/2001",
        "9/14/1990","8/6/1988","5/24/1956","5/27/1951","5/8/1993","7/26/2000","10/13/1979","2/17/1980","12/6/1997","3/25/1944",
        "3/2/1976","8/22/2008","8/14/1962","6/2/2015","5/28/2015","2/17/2014","1/18/2002","12/12/1979","8/21/1992","5/15/2009"};
    private List<string> phones = new List<string>()
        {"(832) 902-5930","(832) 124-6549","(281) 228-5841","(281) 324-7692","(713) 211-7052","(832) 160-3371","(713) 484-5682",
        "(713) 239-8472","(713) 765-7603","(713) 614-2800","(832) 652-3991","(281) 282-3382","(281) 231-6973","(713) 836-8072",
        "(281) 540-3807","(281) 489-2834","(713) 957-8453","(713) 769-2977","(713) 554-4792","(281) 800-8234","(713) 768-2726",
        "(281) 543-9638","(281) 928-2888","(832) 826-8918","(281) 582-8481","(281) 248-9633","(281) 678-7327","(713) 237-9331",
        "(713) 273-1082","(713) 841-3633","(832) 588-2843","(281) 528-7011","(713) 312-5562","(281) 726-7590","(281) 239-6920",
        "(832) 524-3115","(832) 752-8839","(832) 391-3963","(713) 908-1327","(713) 292-8382","(832) 904-5276","(281) 292-7960",
        "(713) 128-2627","(832) 896-8525","(832) 713-2363","(832) 970-1835","(832) 317-6670","(281) 378-5484","(713) 420-3381",
        "(281) 158-2814","(713) 169-3104","(713) 791-8321","(832) 402-8407","(281) 182-8930","(713) 139-2219","(832) 173-6831",
        "(281) 305-7602","(281) 923-9451","(832) 221-8722","(832) 575-1345","(281) 705-6718","(832) 896-5027","(713) 423-5398",
        "(713) 659-8791","(713) 706-9363","(832) 894-7667","(713) 422-1370","(281) 971-9751","(713) 815-3810","(832) 467-2634",
        "(832) 416-3115","(713) 624-6403","(832) 604-5819","(832) 293-2262","(281) 485-8740","(713) 288-7801","(832) 853-4648",
        "(832) 155-5397","(832) 157-5906","(713) 824-3109","(832) 253-9536","(281) 735-8064","(281) 270-7116","(713) 964-1916",
        "(832) 876-9157","(281) 151-5441","(281) 135-1791","(832) 436-7454","(281) 663-4019","(281) 798-4543","(713) 937-4845",
        "(281) 282-2308","(832) 927-6003","(832) 252-5412","(281) 543-8994","(713) 135-8569","(713) 554-2482","(281) 952-1525",
        "(713) 390-2396","(832) 554-9046","(281) 626-4327","(713) 255-3304","(281) 331-4243","(713) 710-3302","(281) 124-1095",
        "(713) 109-8092","(281) 378-9837","(832) 103-1696","(281) 148-8573","(713) 303-2929","(281) 449-2145","(832) 164-6821",
        "(832) 131-7189","(832) 289-9903","(713) 200-3172","(832) 807-7315","(713) 804-2011","(832) 326-3597","(832) 948-5293",
        "(713) 880-4277","(713) 802-8581","(713) 509-1639","(832) 573-9297","(832) 600-2957","(281) 661-5952","(281) 377-7904",
        "(832) 369-8742","(281) 926-1165","(713) 808-4066","(281) 652-3067","(281) 864-8608","(832) 297-3979","(281) 528-4514",
        "(281) 871-6679","(832) 832-9498","(713) 618-3268","(713) 355-9562","(713) 553-9625","(713) 718-1268","(832) 910-1757",
        "(832) 108-3185","(281) 534-4803","(281) 568-7795","(281) 508-5870","(832) 631-9355","(713) 827-9610","(281) 154-9655",
        "(832) 164-6588","(713) 320-4107","(832) 504-6868","(713) 901-1272","(281) 154-8832","(832) 390-8973","(281) 854-2325",
        "(281) 516-4333","(832) 475-5355","(281) 565-4324","(713) 653-5842","(713) 134-6378","(832) 417-4309","(281) 735-1252",
        "(832) 303-1311","(832) 398-6118","(832) 732-4900","(832) 696-9628","(281) 556-2019","(281) 150-8671","(713) 615-5141",
        "(713) 213-6945","(713) 329-9890","(281) 656-7233","(832) 117-7890","(281) 670-7916","(281) 693-7370","(832) 207-9080",
        "(713) 390-5507","(832) 549-6148","(713) 490-1971","(713) 649-7597","(281) 691-7343","(281) 949-2236","(832) 801-6949",
        "(281) 899-8689","(713) 714-6828","(713) 155-8179","(832) 702-6686","(281) 597-8808","(713) 582-1881","(832) 928-2186",
        "(281) 914-2686","(832) 823-7241","(832) 365-3831","(713) 947-5737","(713) 892-4998","(281) 227-2007","(713) 727-2919",
        "(832) 917-5485","(713) 749-3661","(832) 788-9418","(281) 679-7385" };
    private List<string> addresses = new List<string>()
        {"22 Brickell Dr. Gilmer, TX 75645","8762 Ash St. Robstown, TX 78380","642 Golf St. Austin, TX 78768",
        "785 Bowman Street Sherman, TX 75092","7397 West Vista Ave. San Antonio,","7720 Smith Street Sutherland Springs, TX",
        "9228 Lyme Dr. Cat Spring, TX","31 N. Wright Ave. Houston, TX","89 6th Street Bivins, TX 75555",
        "609 Rock Maple Ave. Floresville, TX","54 E. Blue Spring Dr. Fritch,","63 Nut Swamp St. Midland, TX",
        "16 Selby St. Dallas, TX 75386","24 Lower Street Amarillo, TX 79120","7874 High Point St. Conroe, TX",
        "582 Pheasant St. Honey Grove, TX","307 Sugar Ave. Amarillo, TX 79116","97 Timber Avenue Cookville, TX 75558",
        "8922 Campfire St. Driscoll, TX 78351","607 Walnut Ave. Houston, TX 77047","68 E. Saxton Ave. Schwertner, TX",
        "62 Fortune Ave. Grand Prairie, TX","7104 Sutor Dr. El Paso, TX","904 N. Ridgewood Dr. Newton, TX",
        "13 Fox Ave. Jewett, TX 75846","825 Brickell Rd. Odonnell, TX 79351","7163 S. Angel St. El Paso,",
        "8537 SW. Oak Valley Street Powderly,","7661 Livingston Street Devers, TX 77538","774 8th Lane Cedar Creek, TX",
        "8149 Miller St. Forestburg, TX 76239","7385B Brown Dr. Stockdale, TX 78160","714 Shub Farm Lane Weatherford, TX",
        "9431 3rd St. Waco, TX 76711","128 John Drive Houston, TX 77220","655 High Ridge Dr. Queen City,",
        "8480 Lancaster St. Mabank, TX 75156","9279 Bliss St. Ralls, TX 79357","7418 Baker Dr. Brownwood, TX 76804",
        "7346 6th Lane Iredell, TX 76649","7347 East Olive Ave. Columbus, TX","53 8th St. Amarillo, TX 79178",
        "9530 Hill Court El Paso, TX","9774 Foster Street Canyon, TX 79015","64 Honor Ave. Houston, TX 77280",
        "847 Atlantic Rd. Houston, TX 77090","652 West Glen Eagles St. Newark,","816 Grant St. Houston, TX 77278",
        "75 Lumber Ave. Mountain Home, TX","8 Wayne St. Bedias, TX 77831","38 Primrose Street San Marcos, TX",
        "90 South Winter St. Kosse, TX","179 Catherine Rd. De Berry, TX","2 E. Garnet St. El Paso,",
        "633 Railway St. Woodlawn, TX 75694","462 N. Bohemia Ave. Ackerly, TX","33 Catherine St. Waco, TX 76707",
        "351 S. Leatherwood Lane Valley Mills,","718 Bear Hill Lane Pittsburg, TX","918 Sunshine Rd. Dallas, TX 75265",
        "9910 San Juan Road Pasadena, TX","496 Meadowbrook St. Royse City, TX","8843 Snowflake Ave. New Home, TX",
        "27 North Catherine Lane Sabinal, TX","61 Centre Road Richardson, TX 75080","97 Cross Street Tom Bean, TX",
        "7726 Oval Circle Schwertner, TX 76573","8520 Glenholme St. Sierra Blanca, TX","7225 N. Hazelnut St. San Saba,",
        "53 W. Noble Rd. Dallas, TX","7521 Garfield St. Mineral Wells, TX","7423 Railroad St. Sarita, TX 78385",
        "87 Old Miller Dr. Fort Worth,","8 Lumber Lane Haltom City, TX","49 Bradford Dr. Roosevelt, TX 76874",
        "9237 E. Oakland Ave. Sadler, TX","5 4th Lane San Antonio, TX","6 White St. Desdemona, TX 76445",
        "427 NE. Strawberry St. Energy, TX","434 Glenridge Street Austin, TX 78746","118 Jockey Hollow Circle Oakwood, TX",
        "687 Corona Street Euless, TX 76040","5 Monroe St. Del Rio, TX","7483 San Carlos Ave. Sabine Pass,",
        "9107 Serenity Street Humble, TX 77346","9575 Glen Eagles Street Midland, TX","892 Middle River St. Fort Worth,",
        "208 Honor Rd. Follett, TX 79034","9148 Peninsula St. Plantersville, TX 77363","9453 Wagon Dr. Dallas, TX 75225",
        "499 Chapel St. Tyler, TX 75708","272 North Clark Dr. Houston, TX","634 Angel Dr. Houston, TX 77089",
        "9864 Knight Drive Hico, TX 76457","62 Union Street Richards, TX 77873","8214 Leatherwood Dr. Floresville, TX 78114",
        "52 West Trinity Court Austin, TX","98 W. Thatcher Lane Fort Worth,","43 Duke Dr. Knippa, TX 78870",
        "25 Clearance Dr. Buffalo Gap, TX","7390 South King Street Austin, TX","322 Newbridge Ave. San Antonio, TX",
        "357 North Pineknoll Dr. Dallas, TX","57 E. Hilldale Lane Wheeler, TX","8843 Lumber Ave. Burkburnett, TX 76354",
        "91 Butcher Ave. Rockdale, TX 76567","9925 S. Schoolhouse Ave. Bedias, TX","80 Walt Whitman Street Wichita Falls,",
        "9005 Roosevelt Drive Fort Worth, TX","25 Pebble Ave. Muleshoe, TX 79347","23 Chapel Ave. West Point, TX",
        "20 Morris St. Amarillo, TX 79103","8921 E. Iroquois Street Santa Anna,","98 Upper Street New Deal, TX",
        "554 Kent Lane El Paso, TX","8566 Mammoth Street Oakhurst, TX 77359","399 Lyme St. Dobbin, TX 77333",
        "16 Wrangler Drive Karnack, TX 75661","95 Manor Station St. Cuney, TX","8274 Bank Street Snook, TX 77878",
        "38 Prairie Lane Davilla, TX 76523","391 Mulberry St. Walnut Springs, TX","191 Oxford Ave. Corpus Christi, TX",
        "718 Revolution Ave. Earth, TX 79031","820 William Dr. Longview, TX 75615","74 Essex Ave. Baird, TX 79504",
        "7170 Amber Ave. Laredo, TX 78046","9504 SE. Elm Ave. Houston, TX","560 State Court Dallas, TX 75378",
        "8666 Cliff Ave. Springlake, TX 79082","9457C Honor Lane Hobson, TX 78117","941 Valley View St. Dallas, TX",
        "69 Love Drive Del Valle, TX","8467 Duke Street Brookston, TX 75421","7674 Halifax Drive Merit, TX 75458",
        "7168 Victoria St. San Antonio, TX","449 Nova St. Fort Worth, TX","284 Flax St. De Leon, TX",
        "8579 Lyme Ave. Houston, TX 77294","81 Ridgeview Ave. Celina, TX 75009","31 Snake Hill Drive San Antonio,",
        "544 SW. Bayport St. Lawn, TX","74 Division St. Seymour, TX 76380","7029 Cave Drive Dallas, TX 75218",
        "29 Bishop St. Mount Calm, TX","69 Diamond Street Seguin, TX 78156","97 Fawn Drive Houston, TX 77056",
        "82 San Pablo St. Matador, TX","8398 Royalty Dr. Laird Hill, TX","7086 Glen Creek St. Booker, TX",
        "8114 N. Essex St. Houston, TX","8 Walnut Ave. Duncanville, TX 75138","540 West Penn Street Tennessee Colony",
        "7968 Moon Ave. Conroe, TX 77306","7980 Adams St. Coolidge, TX 76635","15 Liberty Dr. Houston, TX 77294",
        "9889 South Albany Dr.Apt 55 Fort","353 Old Silver Spear Dr. Farwell,","4 Thomas St. League City, TX",
        "963 Birch Hill Drive Wingate, TX","857 Rockville Ave. San Antonio, TX","9360 Tower St. Arlington, TX 76015",
        "7367 S. Archer Road San Antonio,","572 Phoenix Dr. Humble, TX 77346","507 Homewood Road Corpus Christi, TX",
        "61 E. Morris Dr. Arlington, TX","9042 Surrey Ave. Austin, TX 78780","89 East Fletcher Street Lueders, TX",
        "828 Brandywine Court Corpus Christi, TX","8653 Morris Street Ennis, TX 75119","114 Foxrun Street Hitchcock, TX 77563",
        "7306 Prince Dr. Corpus Christi, TX","7717 Buckingham Drive Encinal, TX 78019","777 Bellevue Dr. Selman City, TX",
        "8183 Queen Drive Tolar, TX 76476","81 North Ocean Street Olmito, TX","9317 W. Sunnyslope Drive Amarillo, TX",
        "102 Highland St. San Antonio, TX","8252 Plymouth Street El Paso, TX","9334 Shore Lane San Angelo, TX",
        "166 Broad Lane Wichita Falls, TX","83 NE. Lake Forest St. Kurten,","7989 Marlborough Street Center, TX 75935",
        "4 Lafayette Street Wichita Falls, TX","618 Hill Field Dr. Tyler, TX","4 Brookside Street Houston, TX 77221",
        "105 6th Street Daisetta, TX 77533","8039 Ridge Rd. Mcallen, TX 78505","295 Brandywine Ave. Alleyton, TX 78935",
        "575 Pineknoll St. Dallas, TX 75260","643 Warren Lane Shamrock, TX 79079","7133 Bay Meadows Court Penitas, TX",
        "75 Gonzales Ave. Mauriceville, TX 77626","8229 Champion Street Linden, TX 75563","8745 Henry St. Raymondville, TX 78580",
        "295 Pulaski St. Garland, TX 75042","379 Poplar Lane Beaumont, TX 77702","541 Dew Dr. Mc Neil, TX",
        "9585 Windfall Street Rising Star, TX","16 Sage Ave. Fannin, TX 77960"};
    private List<string> memberIds = new List<string>() { "7105730000","7739069000","2743508000","5535367000","7023485000","2183692000",
        "2502127000","9464863000","4044425000","5725355000","2715092000","1073927000","9775998000","5691204000","9113619000","9018803000",
        "7835774000","8823867000","5512671000","6528624000","6137052000","1163668000","7518566000","1668137000","8265149000","4263005000",
        "7992012000","2932228000","2650629000","3768154000","2240413000","3422232000","4588029000","4835382000","6290196000","9665833000",
        "6997860000","8742040000","8320260000","8023936000","2136822000","3155579000","2586125000","6349916000","2493078000","4810620000",
        "5945994000","5883535000","7717735000","6709210000","8210907000","3510331000","2456764000","5367882000","3646286000","4241996000",
        "1396292000","1517924000","8246441000","6122951000","8939103000","1254784000","1342432000","9727288000","3268758000","2557901000",
        "4019118000","3689114000","7076070000","9278348000","9778535000","3747318000","9277154000","3937152000","1996659000","7908344000",
        "5244221000","5442283000","9705988000","8276768000","4694547000","5579315000","7291069000","7619322000","5536093000","6054764000",
        "5618671000","4383411000","6540892000","7517515000","3523587000","1326047000","1758543000","3622143000","2581917000","9182460000",
        "4202795000","3357873000","5357555000","7247628000","6974950000","1733002000","5354833000","8776991000","4624702000","7531659000",
        "6460992000","9748708000","6995855000","9369638000","5498354000","3945291000","8099693000","6377691000","3775092000","2507371000",
        "8813621000","2630621000","1923511000","5263124000","8565156000","3579630000","8261489000","2658572000","3374275000","5431091000",
        "8317388000","2613634000","2334949000","2534546000","2906290000","6517206000","8624955000","1948197000","9162029000","1163704000",
        "7387696000","6191891000","3074184000","8662165000","2591905000","5460782000","6010048000","8212119000","6406105000","2748787000",
        "4171286000","7243997000","4377145000","1355873000","6953208000","4925492000","6763167000","3275885000","6330689000","2707460000",
        "2597106000","2621919000","2336667000","8191394000","8340365000","9324716000","2010723000","6008610000","8380510000","4361586000",
        "8559307000","3981818000","5396278000","4002038000","4020341000","8392738000","3203694000","2735117000","7129757000","6860200000",
        "3571709000","9024037000","6988964000","4057533000","8132815000","9508966000","3905987000","6093016000","2465981000","6496758000",
        "6040887000","3170182000","2351140000","2308878000","7934401000","1508906000","8849778000","4304500000","9789976000","4465900000",
        "5923864000","1424226000","8103631000","1066134000" };
    private List<string> groupIds = new List<string>() { "5170689000","8420826000","3972018000","1287145000","1262584000","7302484000",
        "1439892000","4348364000","6568809000","1844962000","8306056000","5935067000","9371670000","2698491000","1711894000","8464933000",
        "9039861000","8273537000","6120712000","8311547000","6568936000","7629775000","6766886000","9128447000","7744703000","7879482000",
        "5717879000","2557878000","4163514000","2015681000","3000259000","4895161000","6419899000","9777160000","8783804000","7127196000",
        "7843565000","4247617000","4843971000","6136044000","9077659000","2769760000","1275610000","6154301000","1133100000","1126511000",
        "5840010000","7086861000","2534156000","6911236000","3436280000","6641832000","3999595000","6187031000","3129425000","9713801000",
        "3296244000","8429619000","8794834000","2412906000","1113884000","4897286000","1102285000","5066156000","1065527000","4982524000",
        "3393289000","6682275000","7700601000","4003075000","1746212000","5727946000","5725162000","7519450000","4632449000","8728288000",
        "3651965000","7446099000","8590831000","4289186000","5994858000","7151867000","4262312000","2143335000","2195970000","1691199000",
        "5248823000","4647073000","5937381000","7469799000","8627161000","9567497000","2729959000","5813775000","9284607000","4286582000",
        "9300558000","7584381000","7369077000","4038490000","6654549000","5321116000","1237731000","5902230000","2438051000","7181281000",
        "6996130000","2684140000","7011057000","1600798000","9546319000","5863678000","3551062000","8246677000","2980340000","5547816000",
        "6205269000","9601611000","4463988000","2558811000","7146128000","8429466000","7342682000","5440410000","8706597000","2968669000",
        "4468776000","3297297000","8174680000","3615509000","5173596000","4300205000","6648863000","5346154000","9750772000","3798808000",
        "6464669000","4699715000","9206532000","2896319000","5942676000","4102571000","7470446000","1901057000","9354457000","3294058000",
        "4580768000","7591554000","3216767000","2650463000","8673285000","6107437000","9749003000","4153695000","8863010000","4095450000",
        "4989916000","7820455000","5121704000","7748109000","1255324000","3205742000","9311560000","2683097000","2156352000","2077797000",
        "5198598000","7261671000","9842178000","8306897000","6373008000","5034787000","6381093000","8635485000","8173667000","3865906000",
        "9072306000","8244166000","4071142000","7229120000","1537456000","1792413000","1515249000","3493520000","6179643000","8030391000",
        "2728859000","3030323000","7433980000","2125797000","2890266000","8303373000","5441162000","4066593000","1248643000","6398433000",
        "5624153000","9048090000","3180767000","6256649000" };

    private List<string> pcps = new List<string>() { "Dr. Kevin Y. Nieto","Dr. Francisco L. Cooke","Dr. Gordon R. Peacock",
        "Dr. David Q. Black","Dr. Alan Z. Ferrer","Dr. Barry L. Mattson","Dr. Jeffery U. Schubert","Dr. Lawrence B. Mize",
        "Dr. Ray U. Walsh","Dr. William W. Betts","Dr. Eddie B. Jamison","Dr. Greg G. Metcalf","Dr. Douglas T. Stringer",
        "Dr. Adam P. Beckman","Dr. Samuel L. Goddard","Dr. Joseph S. Baugh","Dr. Chris Y. Havens","Dr. Leroy N. Halverson",
        "Dr. Eric K. Welsh","Dr. Arthur O. Nance","Dr. Jonathan X. Ellington","Dr. Vincent W. Salter","Dr. Joshua C. Turpin",
        "Dr. Leonard U. Duckworth","Dr. Jon E. Willey","Dr. Chad N. Garrett","Dr. Patrick V. Bolden","Dr. Melvin K. Larue",
        "Dr. Jorge K. Hodges","Dr. Floyd G. Schmid","Dr. Martin B. Ferry","Dr. Gary C. Kane","Dr. Robert G. Denson",
        "Dr. Kevin E. Head","Dr. Dan I. Bates","Dr. Jason U. Kong","Dr. Theodore X. Joy","Dr. Barry Q. Bass","Dr. Clifford R. McCracken",
        "Dr. Paul I. Everett","Dr. David B. Horne","Dr. Calvin L. Herr","Dr. Alvin D. Fuller","Dr. Anthony C. Lim",
        "Dr. Floyd Y. Tomlin","Dr. Patrick F. McCloud","Dr. Peter H. Gates","Dr. Ronald T. McManus","Dr. Ernest O. Rosa",
        "Dr. Edwin F. Herr","Dr. Thomas K. Mickelsen","Dr. Jamie R. Bucio","Dr. Arthur D. Bethune","Dr. Luis T. Cheever",
        "Dr. Luis C. Bridgeman","Dr. Wesley I. Harvey","Dr. Russell R. Viau","Dr. Rick U. Miyashiro","Dr. Michael O. Kulik",
        "Dr. Max Z. Berard","Dr. Eddie M. Jerry","Dr. Leroy V. Huffman","Dr. Craig S. Stoneburner","Dr. Byron S. Decarlo",
        "Dr. Ivan D. Hang","Dr. Brett I. Darwish","Dr. Tom S. Ellman","Dr. Michael S. Acheson","Dr. Ronnie C. Comes",
        "Dr. Frederick S. Stokely","Dr. Jared X. Robnett","Dr. Luis H. Hartke","Dr. Erik Z. Stinnett","Dr. Jaime G. Livengood",
        "Dr. Johnnie U. Womack","Dr. Daniel Z. Packer","Dr. Arthur Y. Yoo","Dr. Gerald A. Wilde","Dr. Dan B. Benes",
        "Dr. Milton C. Trudell","Dr. Troy Y. Villines","Dr. Travis D. Spellman","Dr. Christopher N. Cloud","Dr. Wayne R. Kulas",
        "Dr. Leonard T. Gano","Dr. Cody U. Gilfillan","Dr. Alvin C. Dickson","Dr. Elmer G. Kull","Dr. Eddie A. Bettis",
        "Dr. Clinton W. Yinger","Dr. Elmer T. Parmley","Dr. John E. Zhao","Dr. Dan T. Waldrep","Dr. Jared Z. Meiners",
        "Dr. Alvin F. Witherell","Dr. Ivan E. Prewitt","Dr. Ronnie I. Cruz","Dr. Roberto F. Hoffert","Dr. Glenn N. Schumaker",
        "Dr. Miguel E. Aho","Dr. Lyman V. Kirbo","Dr. Renaldo C. Montaldo","Dr. Clinton B. Dawkins","Dr. Augustus V. Buttke",
        "Dr. Glenn N. Adamany","Dr. Burt O. Respicio","Dr. Rusty K. Garduque","Dr. Lawerence P. Bechler","Dr. Randell Y. Kellems",
        "Dr. Scotty V. Perisic","Dr. Victor N. Hedman","Dr. Tyson L. Karschner","Dr. Theo O. Naihe","Dr. Ernie S. Kuskie",
        "Dr. Riley F. Maccini","Dr. Alan Q. Javers","Dr. Shawn Y. Waggenspack","Dr. Refugio F. Malett","Dr. Pierre A. Zerbey",
        "Dr. Aron F. Gisriel","Dr. Alfred U. Schuering","Dr. Milford T. Portillo","Dr. Guy I. Ganzon","Dr. Cruz H. Bethel",
        "Dr. Tristan E. Byre","Dr. Rory F. Langlitz","Dr. Arlen M. Fairclough","Dr. Chas S. Umphres","Dr. Wm Y. Gobbel",
        "Dr. Mario I. Jungwirth","Dr. Benito K. Nance","Dr. Ryan E. Krehbiel","Dr. Thurman J. Mellick","Dr. Virgil C. Jagow",
        "Dr. Graham E. McGlothan","Dr. Alonzo R. Pittsenbarger","Dr. Leland F. Bonvillian","Dr. Dustin Q. Bleich",
        "Dr. Julian N. Tahe","Dr. Leo X. Jumpp","Dr. Chang O. Deisley","Dr. Darin O. Noga","Dr. Demetrius O. Isik",
        "Dr. Enrique J. Gerr","Dr. Quincy F. Viegas","Dr. Luke Q. Sora","Dr. Joe R. Bhamani","Dr. Graham D. Akagi",
        "Dr. Luke Q. Koster","Dr. Carmine E. Melichar","Dr. Tracy K. Garay","Dr. Sally D. Zamora","Dr. Anna L. Ernst",
        "Dr. Linda R. London","Dr. Theresa M. Fuller","Dr. Jeanne U. Santos","Dr. Katie R. Hanlon","Dr. Tracy G. Han",
        "Dr. Melanie K. Sotelo","Dr. Erin E. Nichols","Dr. Ella K. Chandler","Dr. Jessica G. Robb","Dr. Phyllis V. Askew",
        "Dr. Ashley U. Hastings","Dr. Lucille L. Barfield","Dr. Lori G. Alonso","Dr. Kathy Y. Estrada","Dr. Jean Z. Cooney",
        "Dr. Janice P. Dewitt","Dr. Irene P. Bailey","Dr. Thelma D. Patten","Dr. Juanita F. Mejia","Dr. Brenda W. Finley",
        "Dr. Holly K. Holguin","Dr. Lisa E. Roark","Dr. Lauren B. Emmons","Dr. April Q. Dees","Dr. Anita P. Becerra",
        "Dr. Tonya L. Centeno","Dr. Louise X. Sisson","Dr. Loretta K. Medrano","Dr. Rhonda X. Carranza","Dr. Bernice F. Applegate",
        "Dr. Ida Z. Wilkes","Dr. Eileen K. Segura","Dr. April S. Toledo","Dr. Jennifer W. Huber","Dr. Renee O. Washington",
        "Dr. Catherine S. Thomson","Dr. Kristen I. Valdes","Dr. Laura W. Juarez","Dr. Eileen H. Redd","Dr. Emily Q. Braden",
        "Dr. Joanne D. Cornelius","Dr. Lois P. Forte","Dr. Ruby B. Brenner","Dr. Brittany V. Block","Dr. Sally W. Forman",
        "Dr. Beatrice H. Jung","Dr. Judy S. Hanna" };
    private List<string> pcpPhones = new List<string>() {"(281) 156-7183","(281) 372-5449","(832) 694-7418","(832) 341-1036",
        "(281) 181-9369","(281) 567-9419","(832) 909-7803","(713) 276-7482","(281) 110-9425","(281) 594-2918","(832) 377-9526",
        "(713) 987-8602","(281) 919-1092","(713) 232-9507","(281) 162-6141","(832) 961-4516","(832) 949-3398","(281) 140-6561",
        "(281) 878-9948","(832) 161-3718","(713) 339-2176","(832) 407-4337","(713) 731-3737","(281) 289-8722","(281) 196-7807",
        "(281) 393-4298","(713) 983-5684","(832) 372-4360","(281) 566-6019","(832) 835-3653","(713) 742-4985","(713) 435-5241",
        "(832) 753-8561","(832) 503-1391","(281) 824-1454","(832) 932-6826","(281) 742-3578","(281) 893-7353","(713) 348-8103",
        "(281) 646-7635","(281) 203-9364","(713) 857-4497","(832) 824-7096","(281) 307-3273","(713) 349-4449","(281) 887-3606",
        "(281) 544-4973","(832) 255-6857","(713) 566-4357","(281) 269-8424","(281) 578-5977","(832) 498-7305","(281) 899-1236",
        "(713) 267-1228","(281) 725-3098","(832) 502-4539","(713) 632-3437","(713) 389-1575","(713) 244-5849","(281) 365-5409",
        "(713) 886-5022","(713) 969-1760","(281) 793-4589","(713) 161-8108","(281) 368-5088","(832) 712-1791","(281) 206-6337",
        "(832) 242-3266","(281) 435-2976","(281) 165-4275","(832) 660-9750","(713) 418-2547","(281) 195-7822","(281) 352-8775",
        "(713) 942-5609","(713) 680-2669","(281) 403-7051","(713) 221-5831","(713) 797-7325","(832) 683-9887","(832) 134-2642",
        "(281) 812-5305","(832) 780-5526","(281) 789-7352","(281) 921-9723","(281) 322-1694","(713) 441-3112","(713) 894-9100",
        "(713) 314-5173","(832) 514-5927","(713) 335-9691","(281) 516-7927","(281) 957-7362","(713) 565-3204","(713) 925-7681",
        "(832) 457-7585","(281) 113-6731","(713) 761-2133","(832) 358-2702","(832) 936-8057","(832) 266-2677","(281) 516-1401",
        "(713) 451-2501","(281) 792-5202","(832) 896-4243","(713) 182-7612","(832) 470-3035","(281) 978-7667","(281) 346-3178",
        "(832) 107-3714","(713) 518-2497","(713) 335-4823","(832) 525-6979","(713) 721-6307","(713) 736-6758","(832) 165-5966",
        "(713) 329-7980","(832) 335-7198","(832) 920-7301","(713) 657-8616","(832) 291-3118","(281) 914-2798","(713) 126-4724",
        "(281) 166-9625","(713) 608-4946","(281) 191-5402","(832) 904-1903","(281) 682-2117","(832) 259-9040","(281) 619-7236",
        "(281) 947-6184","(281) 508-5818","(832) 890-3987","(713) 207-7676","(281) 497-4111","(281) 651-8254","(281) 270-4352",
        "(713) 801-8683","(832) 300-1955","(713) 786-8870","(713) 974-8146","(281) 101-3727","(832) 123-7827","(713) 449-9709",
        "(713) 445-3876","(281) 472-4537","(713) 232-2817","(713) 428-1197","(832) 178-3991","(832) 163-5582","(832) 961-3961",
        "(832) 495-3723","(713) 436-8075","(713) 361-5325","(713) 765-5494","(713) 976-7053","(832) 778-6999","(281) 294-8461",
        "(832) 639-1470","(281) 456-2395","(832) 742-6684","(281) 465-9680","(713) 317-9961","(281) 558-5779","(832) 587-7040",
        "(713) 259-7994","(832) 427-2769","(713) 671-6521","(713) 193-7866","(713) 547-6206","(832) 752-8167","(713) 900-4264",
        "(281) 319-9850","(832) 287-3177","(713) 490-9444","(713) 791-5563","(832) 520-2101","(713) 268-8323","(832) 775-8778",
        "(281) 336-1924","(281) 133-7425","(281) 822-9390","(713) 118-1645","(713) 728-8615","(281) 225-8607","(832) 482-8230",
        "(281) 547-3490","(281) 899-8156","(281) 716-1729","(281) 656-3878","(832) 730-6373","(281) 605-1831","(713) 225-9374",
        "(832) 976-8000","(713) 310-5066","(832) 520-5474","(281) 924-1650","(832) 218-1460","(281) 255-1830","(713) 822-5315",};
    private List<string> insCompanies = new List<string>() { "Aetna", "UnitedHealth", "Humana", "Blues Cross Blue Shield", "Cigna" };
    #endregion
}
