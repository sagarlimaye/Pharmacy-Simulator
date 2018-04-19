using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCabinetDrawer : MonoBehaviour {
    private float distance;
    private bool closed = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);

        if (distance < 3 && Input.GetKeyDown(KeyCode.E) && PlayerLook.hitObject == gameObject)
        {
            if (closed)
            {
                transform.position += new Vector3(0, 0, 1);
                closed = false;
            }
            else
            {
                closed = true;
                transform.position += new Vector3(0, 0, -1);
            }
        }
    }
}
