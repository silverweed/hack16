using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stressbar : MonoBehaviour {

	Slider slider;
	Image fillArea, unfillArea;

	void Start() {
		slider = GetComponent<Slider>();
		fillArea = transform.FindChild("Fill Area").gameObject.GetComponentInChildren<Image>();
		unfillArea = transform.FindChild("Unfill Area").gameObject.GetComponentInChildren<Image>();
	}

	public void Refill(int amt) {
		slider.value += amt;
		unfillArea.rectTransform.anchorMax = fillArea.rectTransform.anchorMax;
	}

	public void Damage(int amt) {
		slider.value -= amt;
		StartCoroutine(Unfill());
	}

	IEnumerator Unfill() {
		while (unfillArea.rectTransform.anchorMax.x > fillArea.rectTransform.anchorMax.x) {
			unfillArea.rectTransform.anchorMax = unfillArea.rectTransform.anchorMax - new Vector2(0.01f, 0f);
			yield return new WaitForSeconds(0.1f);
		}

		yield return null;
	}
}
