using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DialogueController : MonoBehaviour {

    public PlayerController Player;
    public Text Question;
    public Text[] answers;
    public GameObject panelLeft, panelRight;
    public bool busy = false;
    public int playerSelection = -1;

    private GameObject dialog;
    public void setPlayerSelection(int s)
    {
        playerSelection = s;
    }
    
    public delegate void DialogEvent(Dialog d);
    public delegate void DialogControllerEvent(GameObject d);
    public static event DialogControllerEvent DialogStarted;
    public static event DialogControllerEvent DialogCompleted;
    public static event DialogControllerEvent DialogDisplayed;
    public static event DialogEvent IncorrectResponseChosen;
    public static event DialogEvent CorrectResponseChosen;
    // Use this for initialization
    void Start () {
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        playerSelection = -1;
	}
    // Update is called once per frame

    IEnumerator playDialog(Transform d)
    {
        dialog = d.gameObject;
        var current = d;
        if(DialogStarted!=null)
            DialogStarted(dialog);
        do
        {
            current = current.GetChild(0);
            var line = current.GetComponent<Dialog>();
            if(line is CustomerDialog)
            {
                Question.text = (line as CustomerDialog).text;
                if (DialogDisplayed != null)
                    DialogDisplayed(current.gameObject);
                yield return new WaitForSeconds(3.0f);
                Question.text = "";
            }
            else
            {
                playerSelection = -1;
                int i = 0;
                foreach(Transform child in current.transform.parent)
                {
                    answers[i].text = child.GetComponent<Dialog>().text;
                    i++;
                }
                Cursor.lockState = CursorLockMode.None;
                panelLeft.SetActive(true);
                if (d.transform.parent.childCount > 2)
                    panelRight.SetActive(true);
                if (DialogDisplayed != null)
                    DialogDisplayed(current.gameObject);
                Player.WalkSpeed = 0;
                yield return new WaitUntil(() => playerSelection > 0);
                Cursor.lockState = CursorLockMode.Locked;
                var response = current.transform.parent.GetChild(playerSelection - 1).GetComponent<PlayerDialog>();
                if(response.isCorrect)
                {
                    if(CorrectResponseChosen != null)
                        CorrectResponseChosen(dialog.GetComponent<Dialog>());
                    current = response.transform;
                }
                else
                {
                    if(IncorrectResponseChosen != null)
                        IncorrectResponseChosen(dialog.GetComponent<Dialog>());
                    current = response.transform.parent;
                }
            }
        }
        while (current.transform.childCount != 0);
        busy = false;
        if(DialogCompleted != null)
            DialogCompleted(dialog);
    }
    public void startDialog(GameObject d)
    {
        busy = true;
        StartCoroutine(playDialog(d.transform));
    }

}
