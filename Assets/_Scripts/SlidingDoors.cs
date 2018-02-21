using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour {

	Animator animator;
	bool doorOpen;

	void Start() {
		doorOpen = false;
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			doorOpen = true;
			ControlDoors("Open");
		}
	}

	void OnTriggerExit(Collider col) {
		if (doorOpen) {
			doorOpen = false;
			ControlDoors("Close");
		}
	}

	void ControlDoors(string direction) {
		animator.SetTrigger(direction);
	}
}
