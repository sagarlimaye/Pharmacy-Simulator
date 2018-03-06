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
    public bool busy = false, playerDone = false;


    public delegate void DialogCheckpointEvent(Dialog[] d);
    public delegate void DialogEvent(Dialog d);

    private void OnEnable()
    {
        DialogCheckpoint.onCheckpointActivated += startDialog;
    }
    private void OnDisable()
    {
        DialogCheckpoint.onCheckpointActivated -= startDialog;
    }

    // Use this for initialization
    void Start () {
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
	}
    // Update is called once per frame
    void Update () {
        
        if ((Input.GetKeyDown(KeyCode.Alpha1) && correctKey == 1) 
            || (Input.GetKeyDown(KeyCode.Alpha2) && correctKey == 2) 
            || (Input.GetKeyDown(KeyCode.Alpha3) && correctKey == 3) 
            || (Input.GetKeyDown(KeyCode.Alpha4) && correctKey == 4))
        {
            Player.walkSpeed = PlayerController.DEFAULT_WALK_SPEED;
            Question.text = "";
            panelLeft.SetActive(false);
            panelRight.SetActive(false);
            playerDone = true;
        }

    }
    IEnumerator showCustomerDialog(Dialog d)
    {
        Question.text = (d as CustomerDialog).text;
        yield return new WaitForSeconds(3.0f);
        Question.text = "";
    }
    IEnumerator playDialog(Dialog[] d)
    {
        foreach(Dialog current in d)
        {
            if(current is CustomerDialog)
            {
                Question.text = (current as CustomerDialog).text;
                yield return new WaitForSeconds(3.0f);
            }
            else
            {
                var pd = current as PlayerDialog;
                playerDone = false;
                correctKey = pd.correctChoice + 1;
                int i = 0;
                for (i = 0; i < pd.choices.Length; i++)
                    answers[i].text = pd.choices[i];
                panelLeft.SetActive(true);
                if (pd.choices.Length > 2)
                    panelRight.SetActive(true);
                Player.walkSpeed = 0;
                yield return new WaitUntil(() => playerDone == true);
            }
        }
        
        busy = false;
    }
    void startDialog(Dialog[] d)
    {
        busy = true;
        StartCoroutine("playDialog", d);        
    }
    public void showQuestion(string question, string[] choices, int correct)
    {
        /* huffle answer choices
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
        Player.walkSpeed = 0;
    }
}
