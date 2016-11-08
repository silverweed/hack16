using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Buttons : MonoBehaviour {

	public void NewGame() {
		SceneManager.LoadScene("Cutscene_Start");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
