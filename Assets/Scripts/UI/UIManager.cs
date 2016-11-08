﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIManager : Singleton<UIManager> {

	public event Action OnScreenBlack;
	public float fadeToBlackSpeed = 0.03f;
	
	Image blackScreen;

	protected UIManager() {}

	public Damage Damage { 
		get;
		private set;
	}

	public Stressbar Stressbar { 
		get;
		private set;
	}

	// Use this for initialization
	void Start() {
		Damage = GameObject.FindObjectOfType<Damage>();
		Stressbar = GameObject.FindObjectOfType<Stressbar>();
		blackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
	}

	public void FadeToBlack() {
		StartCoroutine(FadeToBlackCoroutine());
	}

	IEnumerator FadeToBlackCoroutine() {
		float alpha = 0f;
		while (blackScreen.color.a < 1f) {
			alpha += fadeToBlackSpeed;
			var c = blackScreen.color;
			blackScreen.color = new Color(c.r, c.g, c.b, alpha);
			yield return new WaitForSeconds(0.01f);;
		}
		if (OnScreenBlack != null)
			OnScreenBlack();
	}
}
