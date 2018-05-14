using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour {

    public GameObject Boss;
    public GameObject BossActivationPoint;
    bool hasSpawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
        //Boss.SetActive(true);
        if (other.gameObject.layer != 10) return;
        if (!hasSpawned)
        {
            Instantiate(Boss, BossActivationPoint.transform);
            hasSpawned = true;
        }
	}
}
