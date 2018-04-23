using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CustomerAgent : MonoBehaviour {
    public Transform target;
	public int scenario;
	public string customerName = "Jamie",  dob ="12/23/1993", drug = "Antibiotic";

	NavMeshAgent agent;
    GameController controller;
    DialogueController dialogcontroller;

	static Animator anim;

    public delegate void CustomerAgentEvent(CustomerAgent customer);
    public static event CustomerAgentEvent CustomerSpawned;

    void OnEnable(){
        DialogueController.DialogCompleted += OnDialogCompleted;
        BottleHolder.BottlePlaced += OnBottlePlaced;
		DialogueController.IncorrectResponseChosen += OnIncorrectResponse;
    }

    void OnDisable(){
        DialogueController.DialogCompleted -= OnDialogCompleted;
        BottleHolder.BottlePlaced -= OnBottlePlaced;
		DialogueController.IncorrectResponseChosen -= OnIncorrectResponse;
    }

    void OnDialogCompleted(GameObject d){
        if(d.tag == "PickupPrescriptionDialog")
            agent.destination = controller.waitPos1.transform.position;
        if(d.tag == "PrescriptionReadyDialog")
        	agent.destination = controller.destroySpot.transform.position;
    }

    void OnBottlePlaced(BottleHolder sender, GameObject bottle){
        if(sender.tag == "FilledPrescriptionAnchor")
            agent.destination = controller.pickupSpot.transform.position;
    }
    
	void Start(){
		anim = GetComponent<Animator>();
        controller = FindObjectOfType<GameController>();
        dialogcontroller = FindObjectOfType<DialogueController>(); 
        agent = GetComponent<NavMeshAgent>();
        agent.destination = controller.requestSpot.transform.position;
        
        // pick random scenario
        //scenario = Random.Range(0, 4);
        scenario = 1;
        // switch on scenario and select the dialogue type and update the game controller variables for score

        target = controller.requestSpot.transform;
        
        customerName = ScenarioInfoScript.scenarioPatientFullName;
        dob = ScenarioInfoScript.scenarioPatientDob;
        drug = ScenarioInfoScript.scenarioPatientDrug;

        if(CustomerSpawned != null)
            CustomerSpawned(this);
    }

	// Update is called once per frame
	void Update () {
		//We need to check if customer is moving in order to activate walk animation
		checkIfMoving();
	}

	//Determine if customer is currently moving by comparing nav mesh agent's current velocity to zero vector
	void checkIfMoving(){
		if (agent.velocity != Vector3.zero) {
            if(anim != null)
			    anim.SetBool ("IsWalking", true);
		}
		else {
            if (anim != null)
			    anim.SetBool ("IsWalking", false);
		}
	}

	void OnIncorrectResponse(Dialog dialog){
		playAngryAnimation ();
	}

	//Play the "anger" animation
	void playAngryAnimation(){
        if(anim != null)
		    anim.SetTrigger ("IsAngry");
	}

	//Play the "give item" animation
	public void playGiveItemAnimation(){
        if(anim != null)
    		anim.SetTrigger ("IsGivingItem");
	}
}
