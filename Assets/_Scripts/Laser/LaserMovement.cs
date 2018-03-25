using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour {

    float x = 1.0f;
    float y = 1.0f;
    float z = 1.0f;
    float speed = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
	}
}
