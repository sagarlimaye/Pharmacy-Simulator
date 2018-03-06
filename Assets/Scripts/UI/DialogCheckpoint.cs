using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogCheckpoint : MonoBehaviour {

    public DialogueController controller;
    public Dialog[] sentences;
    private bool active = true;
    public static event DialogueController.DialogCheckpointEvent onCheckpointActivated;

    private void OnTriggerEnter(Collider other)
    {
        // if(player not busy)
        // emit signal to start dialog
        if (other.tag == "Player")
            if(!controller.busy && active)
            {
                if (onCheckpointActivated != null)
                    onCheckpointActivated(sentences);
                active = false;
            }
    }
}
