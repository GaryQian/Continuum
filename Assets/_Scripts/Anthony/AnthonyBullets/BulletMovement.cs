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
    public GameObject bulletParticles;
    private bool particlesSet = false;

	// Use this for initialization
	void Start () {
		createdAt = Time.time;
		setupTrailRenderer ();
		// Shoot the bullet
		bulletRb = this.gameObject.GetComponent<Rigidbody> ();
		bulletRb.AddForce (transform.forward * bulletSpeed);
        Invoke("Die", timeTillDestroy);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
//		Debug.Log ("Collided with: " + other.gameObject.name);

		if(other.gameObject.layer == 12){
			Destroy (this.gameObject);
            return;
		} else if (other.tag.Equals ("Player") && ShotSource.tag.Equals("Enemy")) {
			UIManager.instance.FlashRedOnDamage ();
			//Debug.Log ("Player hit by " + ShotSource.name + "'s bullet");
			Health health;
			if ((health = other.GetComponent<Health> ()) != null) health.Damage (bulletDamage, ShotSource);
            Destroy (this.gameObject);
            return;
		} else if (other.tag.Equals ("Enemy") && ShotSource.tag.Equals("Player")) {
			//damage handling for shots fired by player
			Health health;
            if (!this.particlesSet) {
                GameObject particles = Instantiate(bulletParticles, this.transform.position, Quaternion.identity);
                particles.SetActive(true);
                this.particlesSet = true;
            }
			if ((health = other.GetComponent<Health> ()) != null) health.Damage (bulletDamage, ShotSource);
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

    void Die() {
        Destroy(gameObject);
    }


}
