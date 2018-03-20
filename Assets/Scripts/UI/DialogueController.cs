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
    private int correctKey;
    public GameObject panelLeft, panelRight;
    public bool busy = false;
    public int playerSelection = -1;

    public void setPlayerSelection(int s)
    {
        playerSelection = s;
    }
    

    public delegate void DialogCheckpointEvent(DialogCheckpoint C);
    public delegate void DialogEvent(Dialog d);

    // Use this for initialization
    void Start () {
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
        playerSelection = -1;
	}
    // Update is called once per frame

    IEnumerator playDialog(Transform d)
    {
        var current = d;
        do
        {
            current = current.GetChild(0);
            var line = current.GetComponent<Dialog>();
            if(line is CustomerDialog)
            {
                Question.text = (line as CustomerDialog).text;
                yield return new WaitForSeconds(3.0f);
                Question.text = "";
            }
            else
            {
                playerSelection = -1;
                var pd = line as PlayerDialog;
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
                Player.WalkSpeed = 0;
                yield return new WaitUntil(() => playerSelection > 0);
                Cursor.lockState = CursorLockMode.Locked;
                var selection = current.transform.parent.GetChild(playerSelection - 1);
                current = selection;
            }
        }
        while (current.transform.childCount != 0);
        busy = false;
        
    }
    public void startDialog(GameObject d)
    {
        busy = true;
        StartCoroutine(playDialog(d.transform));
    }
    public void showQuestion(string question, string[] choices, int correct)
    {
        /* shuffle answer choices
        List<string> choiceList = new List<string>(choices);
        System.Random rnd = new System.Random();
        int i = rnd.Next(0, 3);
        answers[i].text = choiceList[correct];
        correctKey = i+1;
        choiceList.RemoveAt(correct);
        var shuffled = choiceList.OrderBy(item => rnd.Next()).ToList<string>();
        for(i=0;i<4; i++)
        {
            if(i!=correctKey - 1)
            {
                answers[i].text = shuffled[i];
                shuffled.RemoveAt(i);
            }
        }
        */
        
        correctKey = correct + 1;
        for(int i = 0; i < 4; i++)
            answers[i].text = choices[i];
        Question.text = question;
        panelLeft.SetActive(true);
        if(choices.Length > 2)
            panelRight.SetActive(true);
        Player.WalkSpeed = 0;
    }
}
