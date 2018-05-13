using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {
    public GameObject player;
    public Rigidbody body;
    GameObject par;
    Vector3 parPrevPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 p = player.transform.position;
        if (par) {
            p += par.transform.position - parPrevPos;
            parPrevPos = par.transform.position;
        }
        player.transform.position = p;
        body.MovePosition(p);
        //transform.position = player.transform.position;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 12 && other.tag != "NoStick" && other.tag != "NOBULLET") {
            par = other.gameObject;
            parPrevPos = par.transform.position;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == 12 && other.tag != "NoStick" && other.tag != "NOBULLET") {
            if (par == other) {
                par = null;

            }
        }
    }

}
