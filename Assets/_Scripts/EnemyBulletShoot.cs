using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShoot : MonoBehaviour {

	public GameObject Bullet;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ShootProjectile", 1f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ShootProjectile() {
		Instantiate (Bullet, this.transform.position, Quaternion.identity);
	}
}
