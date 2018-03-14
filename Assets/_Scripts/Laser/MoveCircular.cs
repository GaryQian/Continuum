using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircular : MonoBehaviour {

    public Transform laser;
	public float x = 0;
	public float y = 0;
	public float z = 1;
	public float speed = 3;
	public bool random = true;
	
    // Use this for initialization
	void Start () {
		if (random) {
			randomize ();
		}
	}

	void randomize() {
		speed = Random.Range (5, 10);
		x = Random.Range (-1, 1) * speed;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3(0, 0, 0);
        Vector3 relativePos = (laser.position + offset) - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
		transform.Translate(x * Time.deltaTime, y * Time.deltaTime, z * Time.deltaTime);
	}
}
