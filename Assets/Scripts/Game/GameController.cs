using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int pillCountTarget;
	public Text pillCountText;
	public GameObject prescriptionReadyDialog, pickupRequestDialog, wrongPrescriptionPlacedDialog;
	public GameObject spawnPoint, requestSpot, waitPos1, destroySpot, pickupSpot;
    public GameObject Customer;
	public DialogCheckpoint requestCheckpoint, pickupCheckpoint;
    public GameObject filledBottleTarget, emptyBottleTarget;
	private int pillCount;
    public int maxCustomers = 1;
    private int spawnedCustomers = 0; 
	
	void OnEnable()
	{
		CustomerAgent.CustomerSpawned += OnCustomerSpawned;
		DialogueController.DialogCompleted += OnDialogCompleted;
		BottleHolder.BottlePlaced += OnBottlePlaced;
	}


    void OnDisable()
	{
        DialogueController.DialogCompleted -= OnDialogCompleted;
		CustomerAgent.CustomerSpawned -= OnCustomerSpawned;
		BottleHolder.BottlePlaced -= OnBottlePlaced;
    }



    void OnCustomerSpawned(CustomerAgent customer)
	{
        spawnedCustomers++;
		requestCheckpoint.dialog = pickupRequestDialog;
		pickupCheckpoint.dialog = null;
	}
	void OnDialogCompleted(GameObject dialog)
	{
		if(dialog.tag == "PickupPrescriptionDialog")
		{
			pickupCheckpoint.dialog = prescriptionReadyDialog;
			requestCheckpoint.dialog = null;
            filledBottleTarget.SetActive(true);
            emptyBottleTarget.SetActive(false);
		}
		if(dialog.tag == "PrescriptionReadyDialog")
		{
			requestCheckpoint.dialog = pickupRequestDialog;
			pickupCheckpoint.dialog = null;
		}
	}
	void OnBottlePlaced(BottleHolder sender, GameObject bottle)
	{
        if(sender.tag == "FilledPrescriptionAnchor")
        {
            if(bottle.tag.Contains(ScenarioInfoScript.scenarioPatientDrug))
                pickupCheckpoint.dialog = prescriptionReadyDialog;
            else pickupCheckpoint.dialog = wrongPrescriptionPlacedDialog; // wrong prescription placed, raise event
            requestCheckpoint.dialog = null;
        }
        else if(sender.tag == "FileCabinetAnchor")
        {
            if(spawnedCustomers < maxCustomers)
                Instantiate(Customer, spawnPoint.transform.position, Quaternion.identity);
        }
    }
	
	// Use this for initialization
	void Start () {
		pillCount = 0;

		pillCountText.text = "";

		updatePillCount();
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
