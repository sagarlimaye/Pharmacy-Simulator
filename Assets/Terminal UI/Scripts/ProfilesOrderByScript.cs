using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProfilesOrderByScript : MonoBehaviour
{
    public bool ascending = true;
    public ScrollRect scroll;
    List<string> firstNames = new List<string>();
    List<string> lastNames = new List<string>();
    List<string> dobs = new List<string>();
    List<string> phones = new List<string>();
    List<string> addresses = new List<string>();

    public void OrderBy()
    {
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

            firstNames.Add(firstNameFieldTxt);
            lastNames.Add(lastNameFieldTxt);
            dobs.Add(dobFieldTxt);
            phones.Add(phoneFieldTxt);
            addresses.Add(addressFieldTxt);
        }

        

    }
}
