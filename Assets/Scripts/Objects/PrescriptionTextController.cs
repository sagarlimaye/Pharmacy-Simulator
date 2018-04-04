using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrescriptionTextController : MonoBehaviour {

	public string customerFirstName;
	public string customerLastName;
	public string doctorFirstName;
	public string doctorLastName;
	public string drug;
	public string instructions;
	public string quantity;
	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
		textMesh.text = customerLastName + ", " + customerFirstName + "\n" + "Dr. " + doctorLastName + ", " + doctorFirstName + "\n" + drug + "\n" + instructions + "\n" + quantity;
	}
}
