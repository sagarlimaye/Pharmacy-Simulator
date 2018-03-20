using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This allows objects to be picked up and carried
public class PickupObject : MonoBehaviour {
	public Transform holder;

	private float distance;
	private bool isHeld;

	//Called whenever the object is created
	void Start(){
		isHeld = false;

		//Some objects are instantiated, meaning we need to search for the player's holder
		holder = GameObject.Find("HoldAnchor").GetComponent<Transform>();
	}

	//Called once per frame
	void Update(){
		distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
	}

	//When player clicks and is close enough to object, they pick it up; if they're already holding it, they'll drop it
	void OnMouseDown(){
		//Player must be within a minimum range of the object in order to successfully grab it
		if(distance < 3 && !isHeld){
			GetComponent<Rigidbody>().useGravity  = false;
			GetComponent<Rigidbody>().isKinematic = true;
			transform.position = holder.position;
			transform.parent   = GameObject.Find("Player").transform;
			transform.parent   = GameObject.Find("FirstPersonPlayer").transform;
			isHeld = true;
		}
		//If they're already holding the item and they click again, they'll drop it
		else if(isHeld){
			transform.parent = null;
			GetComponent<Rigidbody>().useGravity  = true;
			GetComponent<Rigidbody>().isKinematic = false;
			isHeld = false;
		}
	}
}