using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortProfilesScript : MonoBehaviour {

    public static GameObject profilesContent;
    public static GameObject rxContent; //need to implement sorting for waiters on Rx panel

    public void Awake()
    {
        profilesContent = GameObject.FindGameObjectWithTag("ProfilesContent");
    }

    public void SortByLast()
    {
        GenerateAndSortLastNameList();

        foreach (var entry in lastSorted)
        {
            for (int i = 0; i < profilesContent.transform.childCount; i++)
            {
                if(entry.Id == Int32.Parse(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                            .GetComponent<Text>().text))
                {
                    if(lastAscending)
                        profilesContent.transform.GetChild(i).transform.SetAsLastSibling();
                    else
                        profilesContent.transform.GetChild(i).transform.SetAsFirstSibling();
                }
            }
        }

        if (lastAscending)
            lastAscending = false;
        else
            lastAscending = true;
    }

    public void SortByFirst()
    {
        GenerateAndSortFirstNameList();

        foreach (var entry in firstSorted)
        {
            for (int i = 0; i < profilesContent.transform.childCount; i++)
            {
                if (entry.Id == Int32.Parse(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                            .GetComponent<Text>().text))
                {
                    if (firstAscending)
                        profilesContent.transform.GetChild(i).transform.SetAsLastSibling();
                    else
                        profilesContent.transform.GetChild(i).transform.SetAsFirstSibling();
                }
            }
        }

        if (firstAscending)
            firstAscending = false;
        else
            firstAscending = true;
    }

    public void SortByDob()
    {
        GenerateAndSortDobList();

        foreach (var entry in dobSorted)
        {
            for (int i = 0; i < profilesContent.transform.childCount; i++)
            {
                if (entry.Id == Int32.Parse(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                            .GetComponent<Text>().text))
                {
                    if (dobAscending)
                        profilesContent.transform.GetChild(i).transform.SetAsLastSibling();
                    else
                        profilesContent.transform.GetChild(i).transform.SetAsFirstSibling();
                }
            }
        }

        if (dobAscending)
            dobAscending = false;
        else
            dobAscending = true;
    }

    private void GenerateAndSortLastNameList()
    {
        List<LastName> lastList = new List<LastName>();

        for (int i = 0; i < profilesContent.transform.childCount; i++)
        {
            LastName ln = new LastName(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(1)
                                        .GetComponentInChildren<Text>().text,
                                       profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                        .GetComponent<Text>().text);
            lastList.Add(ln);
        }

        lastSorted = lastList.OrderBy(x => x.Last);
    }

    private void GenerateAndSortFirstNameList()
    {
        List<FirstName> firstList = new List<FirstName>();

        for (int i = 0; i < profilesContent.transform.childCount; i++)
        {
            FirstName ln = new FirstName(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0)
                                        .GetComponentInChildren<Text>().text,
                                       profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                        .GetComponent<Text>().text);
            firstList.Add(ln);
        }

        firstSorted = firstList.OrderBy(x => x.First);
    }

    private void GenerateAndSortDobList()
    {
        List<DateOfBirth> dobList = new List<DateOfBirth>();

        for (int i = 0; i < profilesContent.transform.childCount; i++)
        {
            DateOfBirth ln = new DateOfBirth(profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(2)
                                        .GetComponentInChildren<Text>().text,
                                       profilesContent.transform.GetChild(i).transform.GetChild(0).transform.GetChild(7)
                                        .GetComponent<Text>().text);
            dobList.Add(ln);
        }

        dobSorted = dobList.OrderBy(x => (DateTime.Parse(x.Dob)));
    }

    #region Data Type Classes
    private class LastName
    {
        public LastName(string last, string id)
        {
            this.Last = last;
            this.Id = Int32.Parse(id);
        }

        private string last;
        public string Last { get; set; }

        private int id;
        public int Id { get; set; }
    }

    private class FirstName
    {
        public FirstName(string first, string id)
        {
            this.First = first;
            this.Id = Int32.Parse(id);
        }

        private string first;
        public string First { get; set; }

        private int id;
        public int Id { get; set; }
    }

    private class DateOfBirth
    {
        public DateOfBirth(string dob, string id)
        {
            this.Dob = dob;
            this.Id = Int32.Parse(id);
        }

        private string dob;
        public string Dob { get; set; }

        private int id;
        public int Id { get; set; }
    }
    #endregion

    private IEnumerable<LastName> lastSorted;
    private IEnumerable<FirstName> firstSorted;
    private IEnumerable<DateOfBirth> dobSorted;
    private bool lastAscending = true;
    private bool firstAscending = true;
    private bool dobAscending = true;
 
}
