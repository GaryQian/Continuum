using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour {

	public GameObject portalScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
			portalScreen.SetActive(true);
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
			portalScreen.SetActive(false);
	}
}
