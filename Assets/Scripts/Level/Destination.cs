using UnityEngine;
using System.Collections;

public class Destination : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D cld) {
		if (cld.tag == "Player") {
			GameManager.Instance.WinLevel();
		}
	}
}
