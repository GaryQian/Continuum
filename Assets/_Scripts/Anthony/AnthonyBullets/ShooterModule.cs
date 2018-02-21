using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterModule : MonoBehaviour {

	public GameObject Target;
	public GameObject Gun;
	public GameObject Body;
	public GameObject Muzzle;
	public GameObject Bullet;
	public float shootInterval = 0.5f;
	public float detectionRange = 0.01f;
	private bool targetSighted = false;
	public Material NormalStateMaterial;
	public Material AlarmedStateMaterial;

	void Awake () {
	}

	// Use this for initialization
	void Start () {
		Target = GameObject.FindWithTag ("Player");
		InvokeRepeating ("shootBulletFromEnemy", 0f, shootInterval);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation (Target.transform.position - transform.position);
		Debug.DrawRay(transform.position, Target.transform.position - transform.position, Color.green);
	}

	void FixedUpdate(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Target.transform.position - transform.position, out hit, detectionRange)) {
			if (hit.transform.gameObject.CompareTag("Player")) {
				if (!targetSighted) {
					targetSighted = true;
					Body.GetComponent<Renderer> ().material = AlarmedStateMaterial;
				}
			} else {
				if (targetSighted) {
					targetSighted = false;
					Body.GetComponent<Renderer> ().material = NormalStateMaterial;
				}
			}
		} else {
			if (targetSighted) {
				targetSighted = false;
				Body.GetComponent<Renderer> ().material = NormalStateMaterial;
			}
		}
	}

	void shootBulletFromEnemy() {
		if (targetSighted) {
			GameObject instanceBullet = Instantiate (Bullet, Muzzle.transform.position, Quaternion.identity);
			instanceBullet.transform.rotation = Quaternion.LookRotation (Target.transform.position - transform.position);
			BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement> ();
			bulletScript.ShotSource = this.gameObject;
		}
	}

	void shootBullet(){

	}
}
