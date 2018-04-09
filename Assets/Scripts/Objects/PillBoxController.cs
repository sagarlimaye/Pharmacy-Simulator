using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillBoxController : MonoBehaviour {
	public GameController gameController;
    //public Input userInputField;
    public string pillType;
	private float distance;
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		//If the player is close enough to the pill bottle, hits E, and the required pill threshold has been met, the bottle will turn into a prescription
		if(distance < 3 && Input.GetKeyDown(KeyCode.E)){
			/*int userInput = int.Parse(userInputField.text.ToString());
			if(userInput > 0){
				//We get the position and rotation of the pill tray, assign it to the filled tray, delete the empty tray, then instantiate the filled tray
				var emptyTray = GameObject.FindGameObjectWithTag("PillTray");
				var trayPosition = emptyTray.transform.position;
				var trayRotation = emptyTray.transform.rotation;

				Destroy (emptyTray);

				var filledTray = Instantiate(Resources.Load<GameObject>("Prefabs/FilledTray"), trayPosition, trayRotation).GetComponent<FilledTrayController>();

				filledTray.gameController = gameController;
				filledTray.setPillCount (adjustPillInput(50));
			}*/

			//We get the position and rotation of the pill tray, assign it to the filled tray, delete the empty tray, then instantiate the filled tray
			var emptyTray = GameObject.FindGameObjectWithTag("PillTray");
			var trayPosition = emptyTray.transform.position;
			var trayRotation = emptyTray.transform.rotation;


			var filledTray = Instantiate(Resources.Load<GameObject>("Prefabs/FilledTray"), trayPosition, trayRotation).GetComponent<FilledTrayController>();

			filledTray.gameController = gameController;
			filledTray.setPillCount (adjustPillInput(50));
			emptyTray.transform.GetChild(0).parent = filledTray.transform;
            filledTray.pillType = pillType;
			Destroy (emptyTray);
		}
	}

	//Take the user input and add an element of randomness to it; there's a 25% chance the number of pills specified will be incorrect
	int adjustPillInput(int userInput){
		int adjustment = 0;

		int outcome = Random.Range (0, 100);

		//If outcome is within 25%, we change the adjustment
		if(outcome < 25){
			//We'll either add or subtract from the user's input; it's a subtle change of only 1 pill
			adjustment = outcome < 13 ? -1 : 1;
		}

		return userInput + adjustment;
	}
}