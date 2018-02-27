using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {
	public Transform isHeld;
	private float distance;

	//Called once per frame
	void Update(){
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
	}

	//When player clicks and holds mouse, they pick up the object
	void OnMouseDown(){
		//Player must be within a minimum range of the object in order to successfully grab it
		if(distance < 3){
			GetComponent<Rigidbody>().useGravity  = false;
			GetComponent<Rigidbody>().isKinematic = true;
			transform.position = isHeld.position;
			transform.parent   = GameObject.Find("Player").transform;
			transform.parent   = GameObject.Find("FirstPersonPlayer").transform;
		}
	}

	//When player lets go of mouse, they're no longer holding the object
	void OnMouseUp(){
		transform.parent = null;
		GetComponent<Rigidbody>().useGravity  = true;
		GetComponent<Rigidbody>().isKinematic = false;
	}
}