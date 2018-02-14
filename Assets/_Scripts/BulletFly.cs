using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour {

	private Transform targetTransform;
	private Rigidbody rb;
	private Vector3 targetPosition2D;
	private float createdAt;

	// Use this for initialization
	void Start () {
		targetTransform = GameManager.Instance.getPlayerLocation ();
		rb = this.GetComponent<Rigidbody> ();
		targetPosition2D = new Vector3 (targetTransform.position.x, 0f, targetTransform.position.z);
		rb.AddForce (targetPosition2D * 50);
//		Debug.Log ("Shooting to: " + targetPosition2D);
		createdAt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - createdAt > 3f) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Collided with: " + other.gameObject.name);
		if(other.tag.Equals("Player")){
			UIManager.instance.FlashRedOnDamage ();
			Destroy (this.gameObject);
		}
	}
}
