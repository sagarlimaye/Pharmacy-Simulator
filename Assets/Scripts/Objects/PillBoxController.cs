using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBoxController : MonoBehaviour {
	public GameController gameController;

	private float distance;
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		//If the player is close enough to the pill bottle, hits E, and the required pill threshold has been met, the bottle will turn into a prescription
		if(distance < 3 && Input.GetKeyDown(KeyCode.E)){
			//We get the position and rotation of the pill tray, assign it to the filled tray, delete the empty tray, then instantiate the filled tray
			var emptyTray = GameObject.FindGameObjectWithTag("PillTray");
			var trayPosition = emptyTray.transform.position;
			var trayRotation = emptyTray.transform.rotation;

			Destroy (emptyTray);

			var filledTray = Instantiate(Resources.Load<GameObject>("Prefabs/FilledTray"), trayPosition, trayRotation).GetComponent<FilledTrayController>();

			filledTray.gameController = gameController;
			filledTray.setPillCount (50);
		}
	}
}