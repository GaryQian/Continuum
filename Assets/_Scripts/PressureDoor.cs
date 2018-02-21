using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureDoor : MonoBehaviour {

	Animator animator;
	bool doorOpen;
	double timeLeft = 0;

	void Start() {
		doorOpen = false;
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			doorOpen = true;
			ControlDoors ("Open");
			timeLeft = 5.0;
		}
	}

	void Update() {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0 && doorOpen) {
			doorOpen = false;
			ControlDoors("Close");
		}
	}

	void ControlDoors(string direction) {
		animator.SetTrigger(direction);
	}
}
