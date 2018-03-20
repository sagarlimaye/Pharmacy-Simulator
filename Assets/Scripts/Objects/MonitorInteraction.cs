using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorInteraction : MonoBehaviour {
	public Text notification;

	private float distance;
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

		/*if (distance < 3) {
			notification.text = "Press E to use.";
		}
		else {
			notification.text = "";
		}*/
	}
}
