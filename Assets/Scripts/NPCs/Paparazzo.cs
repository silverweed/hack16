﻿using UnityEngine;
using System.Collections;

public class Paparazzo : Npc {

	public float speed = 5f;
	public float patrolRadius = 3f;
	public float targetFwdDistance = 2f;

	FSM ai;
	Vector3? target;

	// Use this for initialization
	void Start() {
		ai = new FSM(this);
		StartCoroutine(TakeDecisions());
	}

	void Update() {
		if (target.HasValue) {
			print("pos: " + transform.position + ", vec: " + (transform.position - target.Value));
			transform.Translate(Time.deltaTime * speed *
				(-transform.position + target.Value).normalized);
			// TODO rotation
		}
	}
	
	IEnumerator TakeDecisions() {
		while (true) {
			ai.Update();
			yield return new WaitForSeconds(.5f);
		}
	}

	class FSM {
		enum State { PATROLLING, MOVING };

		State state = State.PATROLLING;
		Vector3 target;
		Paparazzo owner;

		public FSM(Paparazzo owner) {
			this.owner = owner;
		}

		public void Update() {
			switch (state) {
			case State.PATROLLING:
				if (PlayerNearby()) {
					owner.target = target;
					state = State.MOVING;
				}
				break;
			case State.MOVING:
				if (Vector3.Distance(owner.transform.position, target) < 0.1f) {
					state = State.PATROLLING;
					owner.target = null;
				}
				break;
			}
			print(state + ", target = " + target);
		}

		bool PlayerNearby() {
			var player = GameObject.FindObjectOfType<MovePlayer>();
			if (Vector3.Distance(owner.transform.position,
						player.transform.position) < owner.patrolRadius) 
			{
				target = player.transform.position + (Vector3)MovePlayer.directionPlayer.normalized * owner.targetFwdDistance;
				return true;
			}
			return false;
		}
	}
}
