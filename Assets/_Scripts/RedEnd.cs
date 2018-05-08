using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnd : MonoBehaviour {

    public Renderer[] pipes;
    public GameObject glass;
    public Collider ball;
    public Material normal;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void changePipeMaterial() {
        foreach (Renderer pipe in pipes) {
            pipe.material = normal;
            //pipe.material.color = Color.black;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered red end switch");
        if (other.tag == "Player")
        {
            changePipeMaterial();            
            ball.attachedRigidbody.useGravity = true;
            ball.attachedRigidbody.isKinematic = false;
            glass.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
