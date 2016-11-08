using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Cutscene : MonoBehaviour {

	public string nextScene;
	[Tooltip("Text to write. Pipe symbol (`|`) means newline.")]
	public string str;
	public bool drawImage;
	public float textWait = 0.08f;
	public float fadeSpeed = 0.005f;

	Text text;
	bool done;
	Image image;

	// Use this for initialization
	void Awake() {
		text = GetComponentInChildren<Text>();
		image = transform.FindChild("Image").GetComponent<Image>();
	}
	
	void Start() {
		if (drawImage) {
			image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
			StartCoroutine(UpdateSprite());
		} else
			StartCoroutine(UpdateText());	
	}

	void Update() {
		if (done)
			SceneManager.LoadScene(nextScene);
	}

	IEnumerator UpdateText() {
		int idx = 0;
		while (idx < str.Length) {
			char c = str[idx];
			// Handle special characters
			switch (c) {
			case '^':
				// wait character
				break;
			case '|':
				text.text += "\r\n";
				break;
			default:
				text.text += c;
				break;
			}

			// Adjust wait
			float wait = textWait;
			switch (c) {
			case ';': 
			case ':':
			case ',':
				wait *= 2;
				break;
			case '.':
				if (idx < str.Length - 1 && str[idx+1] == '.')
					break;
				wait *= 4;
				break;
			case '?':
			case '!':
			case '^':
				wait *= 4;
				break;
			case '|':
				wait *= 6;
				break;
			}
			++idx;
			yield return new WaitForSeconds(wait);
		}
		StartCoroutine(FadeAway());
	}

	IEnumerator UpdateSprite() {
		float alpha = 0f;
		while (alpha < 1f) {
			alpha += fadeSpeed;
			var c = image.color;
			image.color = new Color(c.r, c.g, c.b, alpha);
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(3f);
		StartCoroutine(FadeAway());
	}

	IEnumerator FadeAway() {
		float alpha = 1f;
		while (alpha > 0f) {
			alpha -= fadeSpeed;
			if (drawImage) {
				var c = image.color;
				image.color = new Color(c.r, c.g, c.b, alpha);
			} else {
				var c = text.color;
				text.color = new Color(c.r, c.g, c.b, alpha);
			}
			yield return new WaitForSeconds(0.01f);
		}
		done = true;
	}
}
