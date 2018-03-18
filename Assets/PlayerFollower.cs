using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public PlayerController player;
    private float speed = 2;
	
	// Update is called once per frame
	void Update () {
        var step = speed * Time.deltaTime;
        if(Vector3.Distance(transform.position, player.transform.position) > 2)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
	}
}
