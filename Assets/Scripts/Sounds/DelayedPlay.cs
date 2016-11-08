using UnityEngine;
using System.Collections;

public class DelayedPlay : MonoBehaviour {

	public float seconds;

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().Play((ulong)(44100 * seconds));
	}
}
