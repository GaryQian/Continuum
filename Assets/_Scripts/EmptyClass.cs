using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretRotationModule : MonoBehaviour {

	public GameManager gm;

	void Awake () {
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (gm.player)
		{
			transform.rotation = Quaternion.LookRotation(gm.player.transform.position - transform.position);
		}
	}

	void FixedUpdate(){

	}


}
