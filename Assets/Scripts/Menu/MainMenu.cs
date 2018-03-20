using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartTraining(){
		SceneManager.LoadScene ("Prototype");
	}

	public void StartChallenge(){
		SceneManager.LoadScene ("Prototype");
	}

	public void QuitGame(){
		Debug.Log ("Exiting game.");
		Application.Quit ();
	}
}
