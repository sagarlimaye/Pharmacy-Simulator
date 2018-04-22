using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrescriptionDatabase : MonoBehaviour
{
    public static string GenerateRefill()
    {
        return refills[rnd.Next(refills.Count)];
    }

    public static bool GenerateGenOrBr()
    {
        return genOrBrOptions[rnd.Next(genOrBrOptions.Count)];
    }

    public static string GenerateSig()
    {
        return "Take " + numCapsules[rnd.Next(numCapsules.Count)] + " capsules po " + timesPerDay[rnd.Next(timesPerDay.Count)]
                                + " for " + duration[rnd.Next(duration.Count)] + " days until all taken";
    }

    public static string GenerateSigFonts()
    {
        return signatureFonts[rnd.Next(signatureFonts.Count)];
    }

    public static DateTime RandomDay()
    {
        DateTime start = new DateTime(2018, 4, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(rnd.Next(range)).Date;
    }

    private static List<string> refills = new List<string>() { "0", "1", "2" };
    private static List<bool> genOrBrOptions = new List<bool>() { true, false };
    private static List<string> signatureFonts = new List<string>() { "Arty Signature.ttf", "mayqueen.ttf", "Geovanna.ttf", "Beastfom.ttf" };

    private static List<string> numCapsules = new List<string>() { "ONE", "TWO", "THREE" };
    private static List<string> timesPerDay = new List<string>() { "qd", "bid", "tid", "qid", "q12h", "q6h", "q4h" };
    private static List<string> duration = new List<string>() { "3", "5", "10", "15", "30" };

    private static System.Random rnd = new System.Random();


}