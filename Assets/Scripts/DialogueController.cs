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
        }

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
        panelRight.SetActive(true);
        Player.walkSpeed = 0;
    }
}
