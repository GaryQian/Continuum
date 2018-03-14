using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraHeadTracking : MonoBehaviour {

	public GameObject HeadModel;

	// Use this for initialization
	void Start () {
		Camera.main.cullingMask = 1 << 0;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = HeadModel.transform.position;
	}
}
