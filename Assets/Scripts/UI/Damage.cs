using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Damage : MonoBehaviour {

	public float fadeInSpeed = 0.05f;
	public float fadeOutSpeed = 0.05f;
	public float pulseFrequency = 5f;

	Image image;
	float level;

	// true: the damage builds up over time
	// false: the damage decays over time
	public bool Active { get; set; }

	// Use this for initialization
	void Start() {
		image = GetComponent<Image>();	
	}
	
	void Update() {
		if (GameManager.Instance.IsGameOver) return;

		if (Active) {
			level += fadeInSpeed;
			level += (2 + 7 * level) * fadeInSpeed / 2f * Mathf.Sin(pulseFrequency * Time.time);
		} else {
			if (level == 0f) return;
			level -= fadeOutSpeed;
		}


		level = Mathf.Clamp(level, 0f, 1f);
		var c = image.color;
		image.color = new Color(c.r, c.g, c.b, level);

		Active = false;
	}
}
