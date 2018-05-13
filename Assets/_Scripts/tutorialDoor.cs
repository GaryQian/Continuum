using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialDoor : MonoBehaviour {

	public GameObject door;
	private BoxCollider boxCollider;
    private Renderer glassRenderer;
	// Use this for initialization
	void Start () {
		boxCollider = door.gameObject.GetComponent<BoxCollider>();
        glassRenderer = door.gameObject.GetComponent<MeshRenderer>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			boxCollider.enabled = false;
            glassRenderer.enabled = false;
            UIManager.instance.SendMessage("PRESS R TO ACTIVATE REWIND");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		boxCollider.enabled = true;
		glassRenderer.enabled = true;
	}
}
