using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public int pillCountTarget;
    public Text pillCountText;
    public GameObject prescriptionReadyDialog, pickupRequestDialog, wrongPrescriptionPlacedDialog;
    public GameObject spawnPoint, requestSpot, waitPos1, destroySpot, pickupSpot;
    public GameObject Customer;
    public GameObject[] CustomerPrefabs;
    public DialogCheckpoint requestCheckpoint, pickupCheckpoint;
    public GameObject filledBottleTarget, emptyBottleTarget;
    private int pillCount;
    public int maxCustomers = 1;
    private int spawnedCustomers = 0;
    public UnityEvent onIncorrectPrescription;
    public UnityEvent onCorrectPrescription;
    public ScenarioInfoScript.Scenario currentScenario;


    void OnEnable()
    {
        CustomerAgent.CustomerSpawned += OnCustomerSpawned;
        DialogueController.DialogCompleted += OnDialogCompleted;
    }

    void OnDisable()
    {
        DialogueController.DialogCompleted -= OnDialogCompleted;
        CustomerAgent.CustomerSpawned -= OnCustomerSpawned;
    }
    
    void OnCustomerSpawned(CustomerAgent customer)
	{
        spawnedCustomers++;
		requestCheckpoint.Dialog = pickupRequestDialog;
		pickupCheckpoint.Dialog = null;
	}
	void OnDialogCompleted(GameObject dialog)
	{
		if(dialog.tag == "PickupPrescriptionDialog")
		{
			pickupCheckpoint.Dialog = prescriptionReadyDialog;
			requestCheckpoint.Dialog = null;
            filledBottleTarget.SetActive(true);
            emptyBottleTarget.SetActive(false);
		}
		if(dialog.tag == "PrescriptionReadyDialog")
		{
			requestCheckpoint.Dialog = pickupRequestDialog;
			pickupCheckpoint.Dialog = null;
		}
	}
    public void CheckPrescription(GameObject bottle)
    {
        var agent = Customer.GetComponent<CustomerAgent>();
        if (bottle.tag.Contains(agent.drug))
            onCorrectPrescription.Invoke();
        else onIncorrectPrescription.Invoke();
        
        requestCheckpoint.Dialog = null;
    }
    void OnBottlePlaced(BottleHolder sender, GameObject bottle)
    {
        if (sender.tag == "FilledPrescriptionAnchor")
        {
            if (bottle.tag.Contains(ScenarioInfoScript.scenarioPatientDrug))
                pickupCheckpoint.Dialog = prescriptionReadyDialog;
            else pickupCheckpoint.Dialog = wrongPrescriptionPlacedDialog; // wrong prescription placed, raise event
            requestCheckpoint.Dialog = null;
        }
    }
    public void CreateOrActivateCustomer()
    {
        var agent = Customer.GetComponent<CustomerAgent>();
        agent.GrabInfoFromScenario();
        Customer.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		pillCount = 0;

		pillCountText.text = "";

		updatePillCount();
        ScenarioInfoScript.currentScenario = currentScenario;

        Customer = CustomerPrefabs[UnityEngine.Random.Range(0, CustomerPrefabs.Length)];
        if (currentScenario == ScenarioInfoScript.Scenario.Two)
            Customer.SetActive(true);
    }

    //We display the new pill count for the current prescription
    void updatePillCount(){
		pillCountText.text = "Pills: " + pillCount;
	}
		
	public void setPillCount(int pills){
		pillCount = pills;
		updatePillCount();
	}

	//We reset the pill count to 0 whenever the player creates a new prescription
	public void resetPillCount(){
		pillCount = 0;
		updatePillCount();
	}

	//When the player has counted enough pills, we can replace the pill bottle with a filled prescription
	public bool prescriptionIsReady(){
		return pillCount == pillCountTarget;
	}
}
