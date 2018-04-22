using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCabinetDrawer : MonoBehaviour {
    private float distance;
    private bool closed = true;
    private BoxCollider collider;
    private BottleHolder bottleHolder;
    private Vector3 closedPosition;

    public GameObject player;
    // Use this for initialization
    void Start () {
        collider = GetComponent<BoxCollider>();
        bottleHolder = GetComponent<BottleHolder>();
        collider.enabled = false;
        bottleHolder.enabled = false;
        closedPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        distance = Vector3.Distance(transform.position, player.transform.position);

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
                transform.position = closedPosition;
                collider.enabled = false;
                bottleHolder.enabled = false;
            }
        }
    }
}
