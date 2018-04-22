using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugDatabase : MonoBehaviour {

    public static List<string> drugNames = new List<string>() { "Humira",
                                                                "Adulimumab",
                                                                "Nexium",
                                                                "Esomeprazole Magnesium",
                                                                "Percocet",
                                                                "Oxycodone/Acetaminophen" };

    public static Dictionary<string, List<List<string>>> drugInfo = new Dictionary<string, List<List<string>>>();

    public void Start()
    {
        //Quantity and prices for 8 variations of Humira (Adulimumab)
        for (int i = 0; i < 2; i++)
        {
            drugInfo.Add(drugNames[i], new List<List<string>> { quantities[0], prices[0] });
        }
        //Quantity and prices for 4 variations of Nexium (Esomeprazole Magnesium)
        for (int i = 2; i < 4; i++)
        {
            drugInfo.Add(drugNames[i], new List<List<string>> { quantities[1], prices[1] });
        }
        //Quantity and prices for 6 variations of Percocet (Oxycodone/Acetaminophen)
        for (int i = 4; i < 6; i++)
        {
            drugInfo.Add(drugNames[i], new List<List<string>> { quantities[2], prices[2] });
        }

    }

    private static List<List<string>> quantities = new List<List<string>>() {new List<string> { "2 pens" },

                                                                             new List<string> { "90 capsules",
                                                                                                "30 capsules",
                                                                                                "15 capsules" },

                                                                             new List<string> { "20 tablets",
                                                                                                "30 tablets",
                                                                                                "60 tablets",
                                                                                                "90 tablets" },
    };
    private static List<List<string>> prices = new List<List<string>>() {new List<string> { "$4987.46"},

                                                                         new List<string> { "$794.47",
                                                                                            "$301.14",
                                                                                            "$241.73" },

                                                                         new List<string> { "$6.67",
                                                                                            "$9.50",
                                                                                            "$15.49",
                                                                                            "$20.44" },

    };
}
