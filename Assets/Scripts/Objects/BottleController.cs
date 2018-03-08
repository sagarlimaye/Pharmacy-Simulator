using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour {
	public GameController gameController;

	private float distance;
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		//If the player is close enough to the pill bottle, hits E, and the required pill threshold has been met, the bottle will turn into a prescription
		if(distance < 3 && Input.GetKeyDown(KeyCode.E) && gameController.prescriptionIsReady()){
			//Reset pill count
			gameController.resetPillCount();

			GameObject[] pills = GameObject.FindGameObjectsWithTag("Pill");

			foreach (GameObject pill in pills) {
				Destroy(pill);
			}

			//We now instantiate a filled prescription, then delete this object
			Instantiate(Resources.Load<GameObject>("Prescription"), transform.position, transform.rotation);
			
			Destroy (gameObject);
		}
	}
}
