using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Damage : MonoBehaviour {

	public float transitionSpeed = 0.05f;
	public float pulseFrequency = 5f;

	Image image;
	float level;

	// true: the damage builds up over time
	// false: the damage decays over time
	bool Active { get; set; }

	// Use this for initialization
	void Start() {
		image = GetComponent<Image>();	
	}
	
	void Update() {
		// DEBUG
		if (Input.GetKeyUp(KeyCode.F)) Active = !Active;

		if (Active) {
			level += transitionSpeed;
			level += (2 + 7 * level) * transitionSpeed * Mathf.Sin(pulseFrequency * Time.time);
		} else {
			if (level == 0f) return;
			level -= 2f * transitionSpeed;
		}


		level = Mathf.Clamp(level, 0f, 1f);
		var c = image.color;
		image.color = new Color(c.r, c.g, c.b, level);
	}
}
