using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

	public Transform playerBody;
	public float mouseSensitivity;

	float xAxisClamp = 0.0f;

	void Update() {
		Cursor.lockState = CursorLockMode.Locked;

		if (Cursor.lockState == CursorLockMode.Locked) {
			RotateCamera();
		}
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
}
