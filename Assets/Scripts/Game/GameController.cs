using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int pillCountTarget;
	public Text pillCountText;

	private int pillCount;

	// Use this for initialization
	void Start () {
		pillCount = 0;

		pillCountText.text = "";

		updatePillCount();
	}

	//We display the new pill count for the current prescription
	void updatePillCount(){
		pillCountText.text = "Pills: " + pillCount;
	}
		
	public void incrementPillCount(){
		pillCount++;
		updatePillCount();
	}

	//We reset the pill count to 0 whenever the player creates a new prescription
	public void resetPillCount(){
		pillCount = 0;
		updatePillCount();
	}

	//When the player has counted enough pills, we can replace the pill bottle with a filled prescription
	public bool prescriptionIsReady(){
		return pillCount == pillCountTarget;
	}
}
