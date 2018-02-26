using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Renderer>().enabled = false;
	}
}
