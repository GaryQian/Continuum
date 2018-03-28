using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *	This script component is only to be attached to the highest GameObject encapsulating the player's puppet model,
 *	which is disabled by default, but switched on to active state when time is rewinded.
 *
 */

public class PlayerPuppetModelHolder : MonoBehaviour {

	public GameObject playerPuppetModel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivatePuppetModel(){
		playerPuppetModel.SetActive (true);
	}
}
