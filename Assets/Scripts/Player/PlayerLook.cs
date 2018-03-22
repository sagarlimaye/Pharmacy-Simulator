using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour {
	public Text notification;
	public Transform playerBody;
	public float mouseSensitivity;

	float xAxisClamp = 0.0f;

	void Update() {
		Cursor.lockState = CursorLockMode.Locked;

		if (Cursor.lockState == CursorLockMode.Locked) {
			RotateCamera();
		}

		//If the player is looking at an interactable object, show a message indicating they can use it
		showObjectIsInteractable ();
	}

	void RotateCamera() {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		float rotateAmountX = mouseX * mouseSensitivity;
		float rotateAmountY = mouseY * mouseSensitivity;

		xAxisClamp -= rotateAmountY;

		Vector3 targetRotationCamera = transform.rotation.eulerAngles;
		Vector3 targetRotationBody = playerBody.rotation.eulerAngles;

		targetRotationCamera.x -= rotateAmountY;
		targetRotationCamera.z = 0;
		targetRotationBody.y += rotateAmountX;

		if (xAxisClamp > 90) {
			xAxisClamp = 90;
			targetRotationCamera.x = 90;
		} else if (xAxisClamp < -90) {
			xAxisClamp = -90;
			targetRotationCamera.x = 270;
		}

		transform.rotation = Quaternion.Euler(targetRotationCamera);
		playerBody.rotation = Quaternion.Euler(targetRotationBody);
	}

	void showObjectIsInteractable(){
		Vector3 cameraCenter = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
		RaycastHit hit;

		if (Physics.Raycast (cameraCenter, Camera.main.transform.forward, out hit, 3)) {
			if (hit.transform.gameObject) {
				if (hit.transform.gameObject.tag == "Interactable") {
					notification.text = "Press E to use.";
				}
				else if(hit.transform.gameObject.tag == "FilledTray"){
					notification.text = "Press E to fill prescription or R to empty.";
				}
				else if(hit.transform.gameObject.tag == "PillBox"){
					notification.text = "Press E to fill pill tray.";
				}
				else {
					notification.text = "";
				}
			}
		}
		else {
			notification.text = "";
		}
	}
}
