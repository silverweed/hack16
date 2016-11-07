using UnityEngine;

public class Character : MonoBehaviour {

	public float speed = 10f;

	void Update() {
		var shift = Vector3.zero;

		if (Input.GetKey(KeyCode.UpArrow))
			shift.y += 1;
		if (Input.GetKey(KeyCode.DownArrow))
			shift.y -= 1;
		if (Input.GetKey(KeyCode.LeftArrow))
			shift.x -= 1;
		if (Input.GetKey(KeyCode.RightArrow))
			shift.x += 1;

		shift = shift.normalized * Time.deltaTime * speed;
		transform.Translate(shift);
	}
}
