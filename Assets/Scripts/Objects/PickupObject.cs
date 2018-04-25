using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This allows objects to be picked up and carried
public class PickupObject : MonoBehaviour {
	public Transform holder;

	private float distance;
	private bool isHeld;
    private Rigidbody rigidBody;
    public GameObject player;
    public delegate void PickupObjectEvent(GameObject obj);
    public static event PickupObjectEvent PickedUpObject;
    public bool isPickupEnabled = true;
	//Called whenever the object is created
	void Start(){
		isHeld = false;

		//Some objects are instantiated, meaning we need to search for the player's holder
		holder = GameObject.Find("HoldAnchor").GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
        if (player == null)
            player = GameObject.Find("Player");
        if(tag == "Prescription/Empty")
            isPickupEnabled = false;
	}
    private void OnEnable()
    {
        AssemblyScript.LabelPrinted += OnLabelPrinted;
    }
    private void OnDisable()
    {
        AssemblyScript.LabelPrinted -= OnLabelPrinted;
    }
    private void OnLabelPrinted()
    {
        if (tag == "Prescription/Empty")
            isPickupEnabled = true;
    }


    //Called once per frame
    void Update(){
		distance = Vector3.Distance(transform.position, player.transform.position);
	}

	//When player clicks and is close enough to object, they pick it up; if they're already holding it, they'll drop it
	void OnMouseDown(){
		//Player must be within a minimum range of the object in order to successfully grab it
		if(distance < 3 && !isHeld && isPickupEnabled)
        {
			pickUp ();
		}
		//If they're already holding the item and they click again, they'll drop it
		else if(isHeld){
			putDown ();
		}
	}

	void pickUp(){
		GetComponent<Rigidbody>().useGravity  = false;
		GetComponent<Rigidbody>().isKinematic = true;
		transform.position = holder.position;
		transform.parent   = GameObject.Find("Player").transform;
		transform.parent   = GameObject.Find("FirstPersonPlayer").transform;
		isHeld = true;
        rigidBody.constraints = RigidbodyConstraints.None;
        if (PickedUpObject != null)
            PickedUpObject(gameObject);

	}

	public void putDown(){
		transform.parent = null;
		rigidBody.useGravity  = true;
		rigidBody.isKinematic = false;

		isHeld = false;
	}
}