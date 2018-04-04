using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBoxTextController : MonoBehaviour {

	public string drug;

	TextMesh textMesh;

	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
		textMesh.text = drug;
	}
}
