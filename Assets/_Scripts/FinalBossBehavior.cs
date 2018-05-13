using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour {

    public GameObject LaserSphereWrapper;
    public float laserSphereRotateSpeed = 50f;
    public GameObject laserSphere1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LaserSphereWrapper.transform.Rotate(Vector3.up * Time.deltaTime * laserSphereRotateSpeed);
	}
}
