using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHolder : MonoBehaviour {

	BoxCollider coll;
	public GameObject bottle;
	public delegate void BottleHolderEvent(BottleHolder sender, GameObject bottle);
	public static event BottleHolderEvent BottlePlaced;
	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider>();
	}
	void OnEnable()
	{
		DialogueController.DialogCompleted += OnDialogCompleted;
	}
	void OnDisable()
	{
		DialogueController.DialogCompleted += OnDialogCompleted;
	}

    private void OnDialogCompleted(GameObject d)
    {
		if(d.tag == "PrescriptionReadyDialog")
		{
			RemoveBottle();
		}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Prescription"))
        {
            other.transform.position = transform.position + coll.center;
            var rbody = other.GetComponent<Rigidbody>();
            rbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            other.GetComponent<PickupObject>().putDown();
            bottle = other.gameObject;

            if (BottlePlaced != null)
                BottlePlaced(this, other.gameObject); 
        }
    }
	
	public void RemoveBottle()
	{
		Destroy(bottle, 3);
	}

}
