using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour {

	public GameObject portalScreen;

	// Use this for initialization
	void Start () {
        portalScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        Debug.Log("trigger");
        if (other.tag == "Player") {
            portalScreen.SetActive(true);
            Debug.Log("player triggered");
        }
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
			portalScreen.SetActive(false);
	}
}
