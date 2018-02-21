using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

	public GameObject ShotSource;
	//public GameObject Target;
	public float bulletSpeed = 500f;
	private Rigidbody bulletRb;
	public float bulletDamage = 20f;
	public float timeTillDestroy = 4f;
	public float trailTime = 0.5f;
	public float trailWidth = 0.05f;
	public Material trailMaterial;
	public Gradient trailGradient;
	private float createdAt;
	private TrailRenderer trailRenderer;

	// Use this for initialization
	void Start () {
		createdAt = Time.time;
		setupTrailRenderer ();
		// Shoot the bullet
		bulletRb = this.gameObject.GetComponent<Rigidbody> ();
		bulletRb.AddForce (transform.forward * bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (createdAt + timeTillDestroy < Time.time) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log ("Collided with: " + other.gameObject.name);

		if(other.tag.Equals("Environment")){
			Destroy (this.gameObject);
		} else if (other.tag.Equals ("Player") && ShotSource.tag.Equals("Enemy")) {
			UIManager.instance.FlashRedOnDamage ();
			Debug.Log ("Player hit by " + ShotSource.name + "'s bullet");
			Health health;
			if ((health = other.GetComponent<Health> ()) != null) health.Damage (bulletDamage, ShotSource);
			Destroy (this.gameObject);
		} else if (other.tag.Equals ("Enemy") && ShotSource.tag.Equals("Player")) {
			//logistics handling for shots fired by player
		}
	}

	void setupTrailRenderer(){
		trailRenderer = this.gameObject.AddComponent<TrailRenderer> ();
		trailRenderer.startWidth = trailWidth * 3f;
		trailRenderer.endWidth = trailWidth;
		trailRenderer.material = trailMaterial;
		trailRenderer.colorGradient = trailGradient;
		trailRenderer.time = trailTime;
	}


}
