using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	
	public bool IsGameOver {
		get;
		private set;
	}

	void Start() {
		GameObject.FindObjectOfType<Stressbar>().OnStressFull += GameOver;
		UIManager.Instance.OnScreenBlack += () => {
			SceneManager.LoadScene("Menu");
		};
	}

	void GameOver() {
		IsGameOver = true;
		UIManager.Instance.FadeToBlack();
	}
}
