using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public string targetScene;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene(targetScene);
    }


}
