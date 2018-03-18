using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledTrayController : MonoBehaviour {
	public GameController gameController;

	private float distance;
	private int pillCount;

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		//If the player is close enough to the pill bottle, hits E, and the pill count is not 0, the empty bottle will turn into a prescription
		if(distance < 3 && Input.GetKeyDown(KeyCode.E) && pillCount != 0){
			//Reset pill count
			gameController.resetPillCount();

			GameObject pillBottle = GameObject.Find ("Bottle");
			var bottlePosition = pillBottle.transform.position;
			var bottleRotation = pillBottle.transform.rotation;

			Destroy (pillBottle);

			//We now instantiate a filled prescription, then delete the empty pill bottle
			var filledPrescription = Instantiate(Resources.Load<GameObject>("Prefabs/Prescription"), bottlePosition, bottleRotation).GetComponent<PrescriptionController>();
			filledPrescription.isFilledCorrectly = gameController.prescriptionIsReady ();

			//We're not yet finished.  We must now replace this filled tray with an empty one
			Instantiate(Resources.Load<GameObject>("Prefabs/PillTray"), transform.position, transform.rotation);

			Destroy (gameObject);
		}
	}

	public void setPillCount(int pills){
		pillCount = pills;
		gameController.setPillCount (pillCount);
	}
}