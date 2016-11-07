using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stressbar : MonoBehaviour {

	Slider slider;
	Image fillArea, unfillArea;
	float lastDamageTime;
	bool damaged;

	void Start() {
		slider = GetComponent<Slider>();
		fillArea = transform.FindChild("Fill Area").gameObject.GetComponentInChildren<Image>();
		unfillArea = transform.FindChild("Unfill Area").gameObject.GetComponentInChildren<Image>();
	}

	void Update() {
		if (damaged && Time.time - lastDamageTime > 2f) {
			damaged = false;
			StartCoroutine(Unfill());
		}
	}

	public void Refill() {
		slider.value = slider.maxValue;
		unfillArea.rectTransform.anchorMax = fillArea.rectTransform.anchorMax;
	}

	public void Recover(int amt) {
		slider.value += amt;
		unfillArea.rectTransform.anchorMax = fillArea.rectTransform.anchorMax;
	}

	public void Damage(float amt) {
		slider.value -= amt;
		damaged = true;
		lastDamageTime = Time.time;
	}

	IEnumerator Unfill() {
		while (unfillArea.rectTransform.anchorMax.x > fillArea.rectTransform.anchorMax.x) {
			if (damaged) yield break;
			unfillArea.rectTransform.anchorMax = unfillArea.rectTransform.anchorMax - new Vector2(0.02f, 0f);
			yield return new WaitForSeconds(0.1f);
		}

		yield return null;
	}
}
