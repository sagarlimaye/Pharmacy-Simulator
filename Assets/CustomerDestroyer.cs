using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour {

	public delegate void CustomerDestroyerEvent();
    public Transform spawnPoint;
	public static event CustomerDestroyerEvent CustomerDestroyed;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Customer")
        {
            other.gameObject.SetActive(false);
            other.transform.position = spawnPoint.position;
        }


		if(CustomerDestroyed != null)
			CustomerDestroyed();
	}
}
