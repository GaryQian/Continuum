using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class RewindPassThrough : MonoBehaviour {

	private BoxCollider boxCollider;
    private Renderer glassRenderer;
	// Use this for initialization
	void Start () {
		boxCollider = gameObject.GetComponent<BoxCollider> ();
        glassRenderer = gameObject.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if (PuppetRegister.inRewind) {
		//	boxCollider.enabled = false;
  //          glassRenderer.enabled = false;
		//} else {
		//	//boxCollider.enabled = true;
		//}
	}
}
