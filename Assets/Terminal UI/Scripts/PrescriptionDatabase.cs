using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrescriptionDatabase : MonoBehaviour
{
    public static string generateRefill()
    {
        return refills[rnd.Next(refills.Count)];
    }

    public static string generateGenOrBr()
    {
        return genOrBrOptions[rnd.Next(genOrBrOptions.Count)];
    }

    public static string GenerateSig()
    {
        return "Take " + numCapsules[rnd.Next(numCapsules.Count)] + " capsules by mouth " + timesPerDay[rnd.Next(timesPerDay.Count)]
                                + " for " + duration[rnd.Next(duration.Count)] + " until all taken";
    }

    public static DateTime RandomDay()
    {
        DateTime start = new DateTime(2018, 4, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(rnd.Next(range));
    }
    public static DateTime FutureRandomDay()
    {
        DateTime finish = new DateTime(2018, 12, 31);
        int range = (finish - DateTime.Today).Days;
        return DateTime.Today.AddDays(rnd.Next(range));
    }

    private static List<string> refills = new List<string>() { "No refills", "1", "2" };
    private static List<string> genOrBrOptions = new List<string>() { "Generic substitution permitted", "" };
    private static List<string> numCapsules = new List<string>() { "one", "two", "three" };
    private static List<string> timesPerDay = new List<string>() { "once a day", "twice a day", "three times per day", "four times per day" };
    private static List<string> duration = new List<string>() { "3", "5", "10", "15", "30" };
    private static System.Random rnd = new System.Random();


}