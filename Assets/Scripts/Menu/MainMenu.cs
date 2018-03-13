using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartTraining(){
		SceneManager.LoadScene ("CheckoutCounter");
	}

	public void StartChallenge(){
		SceneManager.LoadScene ("Pharmacy");
	}

	public void QuitGame(){
		Debug.Log ("Exiting game.");
		Application.Quit ();
	}
}
