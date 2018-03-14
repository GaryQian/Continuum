using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircular : MonoBehaviour {

    public Transform laser;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3(0, 0, 0);
        Vector3 relativePos = (laser.position + offset) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
        transform.Translate(0, 0, 3 * Time.deltaTime);
	}
}
