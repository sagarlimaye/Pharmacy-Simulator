using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	public bool isOpen;
	public Animator animator;
	private float distance;

	void Update() {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		if (Input.GetButtonDown("Interact")) {
			Vector3 cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
			RaycastHit hit;
			if (Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, distance)) {
				if (hit.transform.gameObject == gameObject) {
					if (!isOpen) {
						// Player must be within a minimum range of the door to open it
						animator.Play("DoorOpen");
						isOpen = true;
						return;
					} else {
						// Player must be within a minimum range of the door to close it
						animator.Play("DoorClose");
						isOpen = false;
						return;
					}
				}
			}
		}
	}
}
