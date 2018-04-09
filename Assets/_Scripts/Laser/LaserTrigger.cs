using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour {

    public GameObject[] laserBeams;


    static List<GameObject> players;

	// Use this for initialization
	void Start () {
        players = new List<GameObject>();
        InvokeRepeating("Check", 1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void Check() {
        int count = 0;
        foreach (GameObject obj in players) {
            if (obj == null) count++;
        }
        Debug.Log("" + players.Count + " " + count);
        if (players.Count - count <= 0) {
            Debug.Log("Reactivating Lasers");
            foreach (GameObject laserBeam in laserBeams) {
                laserBeam.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            Debug.Log("Triggered Laser!");
            players.Add(other.gameObject);
            foreach(GameObject laserBeam in laserBeams) {
                laserBeam.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            players.Remove(other.gameObject);
            Check();

        }
    }
}
