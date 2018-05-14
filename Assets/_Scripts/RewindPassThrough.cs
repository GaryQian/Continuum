using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class RewindPassThrough : MonoBehaviour {

	private BoxCollider boxCollider;
    private Renderer glassRenderer;
    public GameObject rewindText, portalText;
	// Use this for initialization
	void Start () {
		boxCollider = gameObject.GetComponent<BoxCollider> ();
        glassRenderer = gameObject.GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PuppetRegister.inRewind) {
            rewindText.SetActive(false);
            portalText.SetActive(true);
        } else {
            rewindText.SetActive(true);
            portalText.SetActive(false);
        }
	}
}
