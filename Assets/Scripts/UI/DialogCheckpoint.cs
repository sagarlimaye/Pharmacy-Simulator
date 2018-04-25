using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogCheckpoint : MonoBehaviour {
    public DialogueController controller;
	public int checkpointType;

    //public Dialog[] sentences;
    BoxCollider boxCollider;

    private GameObject dialog;
    public bool isPlayer, isCustomer;

    public GameObject Dialog
    {
        get
        {
            return dialog;
        }

        set
        {
            dialog = value;
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {    
        if(other.tag == "Player")
            isPlayer = true;
        
		if (other.tag == "Customer") {
			isCustomer = true;

			if(checkpointType == 0){
				other.gameObject.GetComponent<CustomerAgent> ().playGiveItemAnimation ();
				checkpointType++;
			}
		}
		
        if(isPlayer && isCustomer)
            if(!controller.busy && Dialog != null)
                activate();
                
        // if(player not busy)
    }
    public void clearDialog()
    {
        dialog = null;
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            isPlayer = false;

		if (other.tag == "Customer") {
			isCustomer = false;
			checkpointType = 0;
		}
    }

    public void activate()
    {
        controller.startDialog(Dialog);        
    }

}
