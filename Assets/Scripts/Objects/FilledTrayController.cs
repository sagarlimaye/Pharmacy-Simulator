using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledTrayController : MonoBehaviour {
	public GameController gameController;

	private float distance;
	private int pillCount;
    public string pillType;
	public GameObject bottle;

	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		//If the player hits E, replace the empty pill bottle with a filled prescription; if they hit R, we only replace the tray
		if(distance < 3 && pillCount != 0){
			if(Input.GetKeyDown(KeyCode.E)){
				var holder = transform.GetChild(0).GetComponent<BottleHolder>();
				GameObject pillBottle = holder.bottle;
				var bottlePosition = pillBottle.transform.position;
				var bottleRotation = pillBottle.transform.rotation;

                //We now instantiate a filled prescription, then delete the empty pill bottle
                
                var filledPrescription = Instantiate(Resources.Load<GameObject>("Prefabs/"+pillType), bottlePosition, bottleRotation).GetComponent<PrescriptionController>();
				filledPrescription.isFilledCorrectly = gameController.prescriptionIsReady ();

				Destroy (pillBottle);

				replaceTray ();
			}
			else if(Input.GetKeyDown(KeyCode.R)){
				replaceTray ();
			}
		}
	}

	//If the user empties the current filled tray by either throwing out the pills or converting it to a prescription, this method is called
	void replaceTray(){
		//Reset pill count
		gameController.resetPillCount();

		Instantiate(Resources.Load<GameObject>("Prefabs/PillTray"), transform.position, transform.rotation);

		Destroy (gameObject);
	}

	public void setPillCount(int pills){
		pillCount = pills;
		gameController.setPillCount (pillCount);
	}
}