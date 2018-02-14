using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayer : MonoBehaviour {
    public Rigidbody body;

    bool wPressed;
    public GameObject bulletPrefab;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = Vector3.zero;
        
        if (Input.GetKey("w")) {
            delta += new Vector3(10f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("a")) {
            delta += new Vector3(0,0, 10f * Time.deltaTime);
        }
        if (Input.GetKey("s")) {
            delta += new Vector3(-10f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d")) {
            delta += new Vector3(0,0, -10f * Time.deltaTime);
        }
        body.MovePosition(transform.position + delta);
        
	}



    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Test");
    }
}
