using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alex_person : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Debug.Log("alex_person script");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Cylinder")) {
            Destroy(other.gameObject);
        }
    }
}
