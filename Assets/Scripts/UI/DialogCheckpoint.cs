using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogCheckpoint : MonoBehaviour {

    public DialogueController controller;
    //public Dialog[] sentences;
    BoxCollider boxCollider;

    public GameObject dialog;
    public bool isPlayer, isCustomer;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {    
        if(other.tag == "Player")
            isPlayer = true;
        if(other.tag == "Customer")
            isCustomer = true;
        if(isPlayer && isCustomer)
            if(!controller.busy && dialog != null)
                activate();
                
        // if(player not busy)
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            isPlayer = false;
        if(other.tag == "Customer")
            isCustomer = false;
    }
    public void activate()
    {
        controller.startDialog(dialog);        
    }
}
