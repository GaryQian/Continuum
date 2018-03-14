using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicider : MonoBehaviour {

    public float time;
	// Use this for initialization
	void Start () {
        Invoke("Die", time);
	}
	
	// Update is called once per frame
    void Die () {
        Destroy(gameObject);
    }
}
