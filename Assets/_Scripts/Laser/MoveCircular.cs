using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCircular : MonoBehaviour {

    public Transform laser;
	public float x = 1;
	public float z = 1;
    public float speed = 1;
	public bool random = true;
    public float height = 0.0f;
	
    // Use this for initialization
	void Start () {
		if (random) {
			randomize ();
		}
	}

	void randomize() {
		x = Random.Range(-2, 2);
        speed = Random.Range(1, 3);
        height = Random.Range(-1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = new Vector3(0, height, 0);
        Vector3 relativePos = (laser.position + offset) - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * speed);
        transform.Translate(x * Time.deltaTime, 0, 0);
        transform.Translate(0, 0, z * Time.deltaTime);
	}
}
