using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewProfileScript : MonoBehaviour
{
    public Button personalOrInsuranceBtn;
    public Button firstBtn;
    public Button lastBtn;
    public Button dobBtn;
    public Button phoneBtn;
    public Button addressBtn;
    public Button insBtn;
    public Button addRxBtn;
    public GameObject contentRectT;
    public GameObject patientEntryObj;
    public GameObject patientEntryPanel;
    public GameObject personalInfoPanel;
    public GameObject insuranceInfoPanel;
    public GameObject newProfilePanel;
    public InputField searchInput;

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
        }
    }

    public void Awake ()
    {
        newProfilePanel.SetActive(false);
        personalInfoPanel.SetActive(false);
        insuranceInfoPanel.SetActive(false);
    }
    
    public void OnNewProfile()
    {
        newProfilePanel.SetActive(true);
        personalInfoPanel.SetActive(true);
    }
    public void OnCancel()
    {
        newProfilePanel.SetActive(false);
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
        InstantiatePatientEntry();

        newEntryFields = new List<Button>() { newFirstBtn, newLastBtn, newDobBtn, newPhoneBtn, newAddressBtn };

        for (int i = 0; i < personalInfoPanel.transform.childCount; i++)
        {
            GameObject entryPanel = personalInfoPanel.transform.GetChild(i).gameObject;  
            string inputTxt = entryPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text;
            newEntryFields[i].GetComponentInChildren<Text>().text = inputTxt;
        }

        searchInput.text = "";
        newProfilePanel.SetActive(false);
    }

    public void InstantiatePatientEntry()
    {
        newPatientEntryObj = Instantiate(patientEntryObj, contentRectT.transform);
        newPatientEntryPanel = Instantiate(patientEntryPanel, newPatientEntryObj.transform);
        newFirstBtn = Instantiate(firstBtn, newPatientEntryPanel.transform);
        newLastBtn = Instantiate(lastBtn, newPatientEntryPanel.transform);
        newDobBtn = Instantiate(dobBtn, newPatientEntryPanel.transform);
        newPhoneBtn = Instantiate(phoneBtn, newPatientEntryPanel.transform);
        newAddressBtn = Instantiate(addressBtn, newPatientEntryPanel.transform);
        Instantiate(insBtn, newPatientEntryPanel.transform);
        Instantiate(addRxBtn, newPatientEntryPanel.transform);
    }

    private int patientEntries = 50;
    private bool personalOn = true;
    private static System.Random rnd = new System.Random();
    private Button newFirstBtn;
    private Button newLastBtn;
    private Button newDobBtn;
    private Button newPhoneBtn;
    private Button newAddressBtn;
    private List<Button> newEntryFields;
    private GameObject newPatientEntryObj;
    private GameObject newPatientEntryPanel;

    #region lists
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
    #endregion
}
