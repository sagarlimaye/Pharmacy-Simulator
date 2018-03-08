using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogCheckpoint : MonoBehaviour {

    public DialogueController controller;
    //public Dialog[] sentences;
    public static event DialogueController.DialogCheckpointEvent onCheckpointActivated;
    public static event DialogueController.DialogCheckpointEvent onCheckpointEntered;

    private void OnTriggerEnter(Collider other)
    {
        // if(player not busy)
        // emit signal to start dialog
        if (other.tag == "Player")
        {
            if (onCheckpointEntered != null)
                onCheckpointEntered(this);
            if(!controller.busy)
                activate();
            
        }
    }
    public void activate()
    {
        if (onCheckpointActivated != null)
            onCheckpointActivated(this);
        controller.startDialog(gameObject);
        gameObject.SetActive(false);
        
    }
}
