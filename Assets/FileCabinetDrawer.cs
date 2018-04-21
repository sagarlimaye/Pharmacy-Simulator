using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCabinetDrawer : MonoBehaviour {
    public float distance;
    private bool closed = true;
    public BoxCollider collider;
    public BottleHolder bottleHolder;
    // Use this for initialization
    void Start () {
        collider = GetComponent<BoxCollider>();
        bottleHolder = GetComponent<BottleHolder>();
        collider.enabled = false;
        bottleHolder.enabled = false;
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
                collider.enabled = true;
                bottleHolder.enabled = true;
            }
            else
            {
                closed = true;
                transform.position += new Vector3(0, 0, -1);
                collider.enabled = false;
                bottleHolder.enabled = false;
            }
        }
    }
}
