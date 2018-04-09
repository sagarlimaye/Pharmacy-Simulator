using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosePrintingLabelScript : MonoBehaviour
{
    public static GameObject assemblyScreen;

    private IEnumerator Start()
    {
        yield return StartCoroutine(WaitAndPrint(3.0F));
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

}
