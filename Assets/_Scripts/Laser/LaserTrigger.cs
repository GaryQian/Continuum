using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour {

    public GameObject[] laserBeams;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            foreach(GameObject laserBeam in laserBeams) {
                laserBeam.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            foreach (GameObject laserBeam in laserBeams)
            {
                laserBeam.SetActive(true);
            }

        }
    }
}
