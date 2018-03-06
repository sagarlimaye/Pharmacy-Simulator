using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour {

    public DialogueController controller;
    public string question;
    public string[] choices;
    public int correctChoice;

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<DialogueController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            controller.showQuestion(question, choices, correctChoice);
            Destroy(this);
        }
    }
}
