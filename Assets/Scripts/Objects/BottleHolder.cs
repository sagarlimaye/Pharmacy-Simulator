using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHolder : MonoBehaviour {

	BoxCollider coll;
	public GameObject bottle;
	public delegate void BottleHolderEvent(GameObject bottle);
	public static event BottleHolderEvent BottlePlaced;
	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Interactable" && other.gameObject.name.Contains("Pill"))
		{
			other.transform.position = coll.transform.position;
			var rbody = other.GetComponent<Rigidbody>();
			rbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
			other.GetComponent<PickupObject>().putDown();
			bottle = other.gameObject;

			if(BottlePlaced != null)
				BottlePlaced(other.gameObject);
		}
	}
}
