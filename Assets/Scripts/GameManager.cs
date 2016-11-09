using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	string nextScene;

	public bool IsGameOver {
		get;
		private set;
	}

	public static GameManager Instance { get; private set; }

	private GameManager() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
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
