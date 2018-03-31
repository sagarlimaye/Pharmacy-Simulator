using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    public Transform target;
    NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }

	// Update is called once per frame
	void Update () {
       
	}
}
