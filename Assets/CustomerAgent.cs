using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CustomerAgent : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    public int scenario;
    GameController controller;
    DialogueController dialogcontroller;

    public string customerName = "Jamie",  dob ="12/23/1993", drug = "Antibiotic";

    public delegate void CustomerAgentEvent(CustomerAgent customer);
    public static event CustomerAgentEvent CustomerSpawned;

    void OnEnable()
    {
        DialogueController.DialogCompleted += OnDialogCompleted;
        BottleHolder.BottlePlaced += OnBottlePlaced;
        
    }
    void OnDisable()
    {
        DialogueController.DialogCompleted -= OnDialogCompleted;
        BottleHolder.BottlePlaced -= OnBottlePlaced;
        
    }
    void OnDialogCompleted(GameObject d)
    {
        if(d.tag == "PickupPrescriptionDialog")
            agent.destination = controller.waitPos1.transform.position;
        if(d.tag == "PrescriptionReadyDialog")
        agent.destination = controller.destroySpot.transform.position;
    }
    void OnBottlePlaced(BottleHolder sender, GameObject bottle)
    {
        if(sender.tag == "FilledPrescriptionAnchor")
            agent.destination = controller.pickupSpot.transform.position;
    }
    void Start()
    {
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
        
       
	}
}
