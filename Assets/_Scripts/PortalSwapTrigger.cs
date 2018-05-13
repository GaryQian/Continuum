using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSwapTrigger : MonoBehaviour {

	public GameObject originalModel, portalModel;
	private bool destroyComplete = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(PuppetRegister.inRewind){
            originalModel.SetActive(false);
            portalModel.SetActive(true);
        } else {
            originalModel.SetActive(true);
            portalModel.SetActive(false);
        }
	}

	//void OnTriggerEnter(Collider other) {
	//	if (!destroyComplete && PuppetRegister.inRewind && other.gameObject.tag.Equals ("Player") && other.gameObject.GetComponent<Puppet>() == null) {
	//		Destroy (originalModel);
	//		portalModel.SetActive (true);

	//	}
	//}

    //private void OnTriggerExit(Collider other) {
    //    OnTriggerEnter(other);
    //}
}
