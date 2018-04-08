using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DashableWall : MonoBehaviour {

	public GameObject player;
	private BoxCollider wallCollider;
	private FirstPersonController fpsScript;

	// Use this for initialization
	void Start () {
		wallCollider = gameObject.GetComponent<BoxCollider> ();
		fpsScript = player.GetComponent<FirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fpsScript.isDashing_delayed) {
			wallCollider.enabled = false;;
		} else {
			wallCollider.enabled = true;
		}
	}
}
