using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public PlayerController player;
    public float maxRange=10f;

    private float speed = 2;
	
	// Update is called once per frame
	void Update () {
        var step = speed * Time.deltaTime;
        var distance = Vector3.Distance(transform.position, player.transform.position); 
        if(distance > 2 && distance<=maxRange)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
	}
}
