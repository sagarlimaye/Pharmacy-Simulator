using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour {

	public delegate void CustomerDestroyerEvent();

	public static event CustomerDestroyerEvent CustomerDestroyed;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Customer")
			Destroy(other.gameObject);
		if(CustomerDestroyed != null)
			CustomerDestroyed();
	}
}
