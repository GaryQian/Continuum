﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterModule : MonoBehaviour {

    public GameManager gm;
	public GameObject Gun;
	public GameObject Body;
	public GameObject Muzzle;
	public GameObject Bullet;
    public NavMeshAgent enemy;
	public float shootInterval = 0.5f;
    public float sightRange = 15.0f;
	public float detectionRange = 0.01f;
	private bool targetSighted = false;
	public Material NormalStateMaterial;
	public Material AlarmedStateMaterial;

	void Awake () {
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("shootBulletFromEnemy", 0f, shootInterval);
	}
	
	// Update is called once per frame
	void Update () {
        if (gm.player)
        {
            transform.rotation = Quaternion.LookRotation(gm.player.transform.position - transform.position);
            Debug.DrawRay(transform.position, gm.player.transform.position - transform.position, Color.green);
        }
	}

	void FixedUpdate(){
        if (Vector3.Distance(gm.player.transform.position, transform.position) < sightRange)
        {
            RaycastHit hit;
            if (gm.player && Physics.Raycast(transform.position, (gm.player.transform.position + new Vector3(0, 0.5f, 0)) - transform.position, out hit, detectionRange))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    if (!targetSighted)
                    {
                        targetSighted = true;
                        Body.GetComponent<Renderer>().material = AlarmedStateMaterial;
                    }
                }
                else
                {
                    if (targetSighted)
                    {
                        targetSighted = false;
                        Body.GetComponent<Renderer>().material = NormalStateMaterial;
                    }
                }
            }
            else
            {
                if (targetSighted)
                {
                    targetSighted = false;
                    Body.GetComponent<Renderer>().material = NormalStateMaterial;
                }
            }
        }
	}

	void shootBulletFromEnemy() {
        //enemy.destination = gm.player.transform.position;
		if (targetSighted) {
			GameObject instanceBullet = Instantiate (Bullet, Muzzle.transform.position, Quaternion.identity);
			instanceBullet.transform.rotation = Quaternion.LookRotation (gm.player.transform.position - transform.position);
			BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement> ();
			bulletScript.ShotSource = this.gameObject;
		}
	}

	void shootBullet(){

	}
}
