using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	
	public bool IsGameOver {
		get;
		private set;
	}

	void Start() {
		GameObject.FindObjectOfType<Stressbar>().OnStressFull += GameOver;
	}

	void GameOver() {
		IsGameOver = true;
		UIManager.Instance.FadeToBlack();
	}
}
