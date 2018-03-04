using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretCapsuleModule : MonoBehaviour {

	public Quaternion rotationQuat;

	void Awake () {
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.player)
		{
			transform.rotation = Quaternion.LookRotation(GameManager.Instance.player.transform.position - transform.position);
		}
	}

	void FixedUpdate(){

	}


}
