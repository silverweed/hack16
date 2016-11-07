using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Buttons : MonoBehaviour {

	public void NewGame() {
		SceneManager.LoadScene("Level1");
	}

	public void ExitGame() {
		Application.Quit();
	}
}
