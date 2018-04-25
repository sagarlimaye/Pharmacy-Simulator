using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottleHolder : MonoBehaviour {

	BoxCollider coll;
    private MeshRenderer meshRenderer;
    [HideInInspector]
    public GameObject bottle;
    public enum Placement
    {
        faceDown, vertical
    };
    public Placement placeAs;
	public delegate void BottleHolderEvent(BottleHolder sender, GameObject bottle);
	public static event BottleHolderEvent BottlePlaced;

    [System.Serializable]
    public class OnBottlePlaced : UnityEvent<GameObject> { };
    public OnBottlePlaced onBottlePlaced;
	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
	}
	void OnEnable()
	{
		DialogueController.DialogCompleted += OnDialogCompleted;
        AssemblyScript.LabelPrinted += OnLabelPrinted;
	}
	void OnDisable()
	{
		DialogueController.DialogCompleted -= OnDialogCompleted;
        AssemblyScript.LabelPrinted -= OnLabelPrinted;
    }

    private void OnLabelPrinted()
    {
        if(tag == "Untagged")
        {
            GetComponent<MeshCollider>().enabled = true;
            meshRenderer.enabled = true;
        }

    }

    private void OnDialogCompleted(GameObject d)
    {
		if(d.tag == "PrescriptionReadyDialog")
		{
			RemoveBottle();
		}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Prescription") && enabled)
        {
            other.GetComponent<PickupObject>().putDown();
            other.gameObject.transform.parent = transform;
            if(placeAs == Placement.faceDown)
            {
                other.transform.rotation = Quaternion.Euler(0, 0, 90);
                other.transform.position = transform.position + new Vector3(-0.1f, 0f);
            }
            else
            {
                other.transform.rotation = Quaternion.identity;
                other.transform.position = transform.position;
            }
            bottle = other.gameObject;

            if (BottlePlaced != null)
                BottlePlaced(this, other.gameObject);
            onBottlePlaced.Invoke(other.gameObject);
        }
    }
	
	public void RemoveBottle()
	{
		Destroy(bottle, 3);
	}

}
