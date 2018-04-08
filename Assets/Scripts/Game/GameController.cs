using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int pillCountTarget;
	public Text pillCountText;
	public GameObject prescriptionReadyDialog, pickupRequestDialog;
	public GameObject spawnPoint, Customer, requestSpot, waitPos1, destroySpot, pickupSpot;
	public DialogCheckpoint requestCheckpoint, pickupCheckpoint;
	public ScenarioInfoScript scenarioInfo;

	private int pillCount;

	
	void OnEnable()
	{
        ScenarioInfoScript.ScenarioInfoReady += OnScenarioInfoReady;
		CustomerAgent.CustomerSpawned += OnCustomerSpawned;
		DialogueController.DialogCompleted += OnDialogCompleted;
		BottleHolder.BottlePlaced += OnBottlePlaced;
	}
	void OnDisable()
	{
        ScenarioInfoScript.ScenarioInfoReady -= OnScenarioInfoReady;
        DialogueController.DialogCompleted -= OnDialogCompleted;
		CustomerAgent.CustomerSpawned -= OnCustomerSpawned;
		BottleHolder.BottlePlaced -= OnBottlePlaced;
	}

	void OnScenarioInfoReady(ScenarioInfoScript scenarioInfo)
	{	
		Instantiate(Customer, spawnPoint.transform.position, Quaternion.identity);
	}

	void OnCustomerSpawned(CustomerAgent customer)
	{
		requestCheckpoint.dialog = pickupRequestDialog;
		pickupCheckpoint.dialog = null;
	}
	void OnDialogCompleted(GameObject dialog)
	{
		if(dialog.tag == "PickupPrescriptionDialog")
		{
			pickupCheckpoint.dialog = prescriptionReadyDialog;
			requestCheckpoint.dialog = null;
		}
		if(dialog.tag == "PrescriptionReadyDialog")
		{
			requestCheckpoint.dialog = pickupRequestDialog;
			pickupCheckpoint.dialog = null;
		}
	}
	void OnBottlePlaced(GameObject bottle)
	{
		pickupCheckpoint.dialog = prescriptionReadyDialog;
		requestCheckpoint.dialog = null;
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
