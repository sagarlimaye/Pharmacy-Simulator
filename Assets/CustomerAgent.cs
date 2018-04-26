using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class CustomerAgent : MonoBehaviour {
    public Transform target;
	public int scenario;
	public string customerName = "Jamie",  dob ="12/23/1993", drug = "Antibiotic";

	private bool hasGivenItem = false;	//We need this flag to prevent the "give item" animation from playing multiple times

	NavMeshAgent agent;
    public GameController controller;
    public DialogueController dialogcontroller;

	static Animator anim;

    public delegate void CustomerAgentEvent(CustomerAgent customer);
    public static event CustomerAgentEvent CustomerSpawned;

    void OnEnable(){
        DialogueController.DialogCompleted += OnDialogCompleted;
        BottleHolder.BottlePlaced += OnBottlePlaced;
		DialogueController.IncorrectResponseChosen += OnIncorrectResponse;
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

        if (CustomerSpawned != null)
            CustomerSpawned(this);
    }

    void OnDisable(){
        DialogueController.DialogCompleted -= OnDialogCompleted;
        BottleHolder.BottlePlaced -= OnBottlePlaced;
		DialogueController.IncorrectResponseChosen -= OnIncorrectResponse;
    }

    void OnDialogCompleted(GameObject d){
        //if (d.tag == "PickupPrescriptionDialog")
        //    GoToWaitPos();
        if (d.tag == "PrescriptionReadyDialog")
            GoToDestroySpot();
    }

    public void GoToDestroySpot(){
        try
        {
            agent.destination = controller.destroySpot.transform.position;
        }
        catch (System.NullReferenceException e) { }
    }

    public void startWait(float seconds){
        if(enabled)
            StartCoroutine(WaitAtPos(seconds));
    }

    public IEnumerator WaitAtPos(float seconds){
        agent.destination = controller.waitPos1.transform.position;
        yield return new WaitForSeconds(seconds);
        agent.destination = controller.requestSpot.transform.position;
    }

    public void GoToWaitPos() {
        try
        {
            agent.destination = controller.waitPos1.transform.position;
        }
        catch(System.NullReferenceException e) { }
    }

    void OnBottlePlaced(BottleHolder sender, GameObject bottle){
        if (sender.tag == "FilledPrescriptionAnchor")
            GoToPickupSpot();
    }

    public void GoToPickupSpot(){
        try
        {
            agent.destination = controller.pickupSpot.transform.position;
        }
        catch(System.NullReferenceException e) {  }
    }

    void Start(){
		anim = GetComponent<Animator>();
        controller = FindObjectOfType<GameController>();
        dialogcontroller = FindObjectOfType<DialogueController>(); 
    }

	// Update is called once per frame
	void Update () {
		//We need to check if customer is moving in order to activate walk animation
		checkIfMoving();

		Vector3 targetPosition = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
		transform.LookAt(targetPosition);
	}

	//Determine if customer is currently moving by comparing nav mesh agent's current velocity to zero vector
	void checkIfMoving(){
		if (agent.velocity != Vector3.zero) {
			//Vector3 targetPostition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
			//Vector3 targetPosition = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);

			//transform.LookAt(targetPosition);

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
		if (anim != null && !hasGivenItem) {
			anim.SetTrigger ("IsGivingItem");

			//We have the customer face forward here because he won't do it properly anywhere else
			transform.LookAt(Vector3.forward);
			hasGivenItem = true;
		}
	}
}