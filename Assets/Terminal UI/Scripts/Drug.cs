using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : MonoBehaviour {

    public static List<string> drugNames = new List<string>() { "Humira", "Adulimumab",
                                                                 "Nexium", "Esomeprazole Magnesium",
                                                                 "Percocet", "Oxycodone/Acetaminophen"};
    public static Dictionary<string, List<List<string>>> drugInfo = new Dictionary<string, List<List<string>>>();

    public void Start()
    {
        for (int i = 0; i < drugNames.Count; i++)
        {
            drugInfo.Add(drugNames[i], new List<List<string>> { quantities[i], prices[i] });
        }
    }

    private static List<List<string>> quantities = new List<List<string>>() {new List<string> { "2 pens 80mg/0.8mL",
                                                                                                "2 pens 40mg/0.4mL",
                                                                                                "2 pens 20mg/0.2mL",
                                                                                                "2 pens 10mg/0.1mL" },
                                                                             new List<string> { "2 pens 80mg/0.8mL",
                                                                                                "2 pens 40mg/0.4mL",
                                                                                                "2 pens 20mg/0.2mL",
                                                                                                "2 pens 10mg/0.1mL" },
                                                                             new List<string> { "90 capsules 40 mg",
                                                                                                "90 capsules 20 mg",
                                                                                                "30 capsules 40 mg",
                                                                                                "30 capsules 20 mg",
                                                                                                "15 capsules 40 mg",
                                                                                                "15 capsules 20 mg" },
                                                                             new List<string> { "90 capsules 40 mg",
                                                                                                "90 capsules 20 mg",
                                                                                                "30 capsules 40 mg",
                                                                                                "30 capsules 20 mg",
                                                                                                "15 capsules 40 mg",
                                                                                                "15 capsules 20 mg" },
                                                                             new List<string> { "20 tablets 2.5/325",
                                                                                                "20 tablets 5/325",
                                                                                                "20 tablets 7.5/325",
                                                                                                "30 tablets 2.5/325",
                                                                                                "30 tablets 5/325",
                                                                                                "30 tablets 7.5/325",
                                                                                                "60 tablets 2.5/325",
                                                                                                "60 tablets 5/325",
                                                                                                "60 tablets 7.5/325",
                                                                                                "90 tablets 2.5/325",
                                                                                                "90 tablets 5/325",
                                                                                                "90 tablets 7.5/325"  },
                                                                             new List<string> { "20 tablets 2.5/325",
                                                                                                "20 tablets 5/325",
                                                                                                "20 tablets 7.5/325",
                                                                                                "30 tablets 2.5/325",
                                                                                                "30 tablets 5/325",
                                                                                                "30 tablets 7.5/325",
                                                                                                "60 tablets 2.5/325",
                                                                                                "60 tablets 5/325",
                                                                                                "60 tablets 7.5/325",
                                                                                                "90 tablets 2.5/325",
                                                                                                "90 tablets 5/325",
                                                                                                "90 tablets 7.5/325" }
    };
    private static List<List<string>> prices = new List<List<string>>() {new List<string> { "$4987.46",
                                                                                            "$4987.46",
                                                                                            "$4987.46",
                                                                                            "$4987.46" },
                                                                         new List<string> { "$4987.46",
                                                                                            "$4987.46",
                                                                                            "$4987.46",
                                                                                            "$4987.46" },
                                                                         new List<string> { "$794.47",
                                                                                            "$794.47",
                                                                                            "$301.14",
                                                                                            "$301.14",
                                                                                            "$241.73",
                                                                                            "$241.73" },
                                                                         new List<string> { "$794.47",
                                                                                            "$794.47",
                                                                                            "$301.14",
                                                                                            "$301.14",
                                                                                            "$241.73",
                                                                                            "$241.73" },
                                                                         new List<string> { "$6.67",
                                                                                            "$6.67",
                                                                                            "$6.67",
                                                                                            "$9.50",
                                                                                            "$9.50",
                                                                                            "$9.50",
                                                                                            "$15.49",
                                                                                            "$15.49",
                                                                                            "$15.49",
                                                                                            "$20.44",
                                                                                            "$20.44",
                                                                                            "$20.44" },
                                                                         new List<string> { "$6.67",
                                                                                            "$6.67",
                                                                                            "$6.67",
                                                                                            "$9.50",
                                                                                            "$9.50",
                                                                                            "$9.50",
                                                                                            "$15.49",
                                                                                            "$15.49",
                                                                                            "$15.49",
                                                                                            "$20.44",
                                                                                            "$20.44",
                                                                                            "$20.44" }
    };
}
