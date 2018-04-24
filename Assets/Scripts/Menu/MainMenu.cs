using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public static GameObject mainMenu;
    public static GameObject trainingMenu;

    public void StartTraining()
    {
        mainMenu.SetActive(false);
        trainingMenu.SetActive(true);
    }

    public void PickUpScenario()
    {
        SceneManager.LoadScene("Scenario1");
        ScenarioInfoScript.currentScenario = ScenarioInfoScript.Scenario.One;
    }

    public void DiscrepancyScenario()
    {
        SceneManager.LoadScene("Scenario1");
        ScenarioInfoScript.currentScenario = ScenarioInfoScript.Scenario.Two;
    }

    public void StartChallenge()
    {
		SceneManager.LoadScene ("Scenario1");
        ScenarioInfoScript.currentScenario = ScenarioInfoScript.Scenario.Challenge;
    }

    public void OnBack()
    {
        mainMenu.SetActive(true);
        trainingMenu.SetActive(false);
    }

    public void QuitGame(){
		Debug.Log ("Exiting game.");
		Application.Quit ();
	}

    private void Awake()
    {
        mainMenu = GameObject.FindGameObjectWithTag("Main Menu");
        trainingMenu = GameObject.FindGameObjectWithTag("Training Menu");
        trainingMenu.SetActive(false);
    }
}
