using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	
	string nextScene;

	public bool IsGameOver {
		get;
		private set;
	}

	void Start() {
		GameObject.FindObjectOfType<Stressbar>().OnStressFull += GameOver;
		nextScene = "Menu";
		UIManager.Instance.OnScreenBlack += GoToScene;
	}

	void GameOver() {
		IsGameOver = true;
		UIManager.Instance.FadeToBlack();
	}

	void GoToScene() {
		SceneManager.LoadScene(nextScene);
	}

	public void WinLevel() {
		string scenename = SceneManager.GetActiveScene().name;
		int lvnum = int.Parse(scenename.Substring(scenename.Length - 1));
		nextScene = "Cutscene_PreLv" + (lvnum + 1);
		UIManager.Instance.FadeToBlack();
	}
}
