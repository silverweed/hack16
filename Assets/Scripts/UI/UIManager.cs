using UnityEngine;
using System.Collections;

public class UIManager : Singleton<UIManager> {

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
	}
}
