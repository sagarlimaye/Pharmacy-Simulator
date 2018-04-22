using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SearchFunctionScript : MonoBehaviour {

    public string firstNameSearchEntry = "";
    public string lastNameSearchEntry= "";
    public string dobSearchEntry ="";
    public string phoneSearchEntry = "";
    public string addressSearchEntry = "";
    public Dropdown searchByDropdown;
    public InputField searchInput;
    public ScrollRect scroll;

    public AudioClip wrongSound;

    // Update is called once per frame
    public void Update () {
		
        switch(searchByDropdown.value)
        {
            case 1:
                searchInput.contentType = InputField.ContentType.Name;
                firstNameSearchEntry = searchInput.text;
                break;
            case 2:
                searchInput.contentType = InputField.ContentType.Name;
                lastNameSearchEntry = searchInput.text;
                break;
            case 3:
                searchInput.contentType = InputField.ContentType.Standard;
                dobSearchEntry = searchInput.text;
                break;
            case 4:
                searchInput.contentType = InputField.ContentType.Standard;
                phoneSearchEntry = searchInput.text;
                break;
            case 5:
                searchInput.contentType = InputField.ContentType.Standard;
                addressSearchEntry = searchInput.text;
                break;
        }
    }

    public void Search()
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
            Update();

            for (int i = 0; i < scroll.content.childCount; i++)
            {
                GameObject patientEntry = scroll.content.GetChild(i).gameObject;
                GameObject patientEntryPanel = patientEntry.transform.GetChild(0).gameObject;
                Button[] entryFields = patientEntryPanel.GetComponentsInChildren<Button>();
                string firstNameFieldTxt = entryFields[0].GetComponentInChildren<Text>().text;
                string lastNameFieldTxt = entryFields[1].GetComponentInChildren<Text>().text;
                string dobFieldTxt = entryFields[2].GetComponentInChildren<Text>().text;
                string phoneFieldTxt = entryFields[3].GetComponentInChildren<Text>().text;
                string addressFieldTxt = entryFields[4].GetComponentInChildren<Text>().text;

                if (searchInput.text == "")
                {
                    patientEntry.SetActive(true);
                }
                else if (searchByDropdown.value == 1)
                {
                    if (firstNameSearchEntry != firstNameFieldTxt)
                        patientEntry.SetActive(false);
                    else
                        patientEntry.SetActive(true);
                }
                else if (searchByDropdown.value == 2)
                {
                    if (lastNameSearchEntry != lastNameFieldTxt)
                        patientEntry.SetActive(false);
                    else
                        patientEntry.SetActive(true);
                }
                else if (searchByDropdown.value == 3)
                {
                    if (dobSearchEntry != dobFieldTxt)
                        patientEntry.SetActive(false);
                    else
                        patientEntry.SetActive(true);
                }
                else if (searchByDropdown.value == 4)
                {
                    if (phoneSearchEntry != phoneFieldTxt)
                        patientEntry.SetActive(false);
                    else
                        patientEntry.SetActive(true);
                }
                else if (searchByDropdown.value == 5)
                {
                    if (addressSearchEntry != addressFieldTxt)
                        patientEntry.SetActive(false);
                    else
                        patientEntry.SetActive(true);
                }
                else if (searchInput.text == "")
                    patientEntry.SetActive(true);
                else
                    patientEntry.SetActive(true);
            }
        }
    }
}
