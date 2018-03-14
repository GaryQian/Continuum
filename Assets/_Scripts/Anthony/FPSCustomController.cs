using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCustomController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Translate(x, 0, 0);
		transform.Translate(0, 0, z);
//		transform.Translate(transform.forward * 0.1f);
	}
}
