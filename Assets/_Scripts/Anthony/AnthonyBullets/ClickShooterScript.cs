﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShooterScript : MonoBehaviour {

	public GameObject Hand;
	private Animator handAnimator;
	public GameObject muzzlePositionHolder;
	public GameObject bullet;
	public GameObject camera;
	private Transform cameraTransform;
	private RaycastHit hit;
	private Vector3 bulletTargetPoint;
	public LayerMask ShotLayerMask;

    public Recorder recorder;


    public float shotDelay = 0.5f;
	private bool aimHasTarget = false;
	private float lastShotTime = 0f;

	void Awake(){
		ShotLayerMask = LayerMask.GetMask(new string[]{"Default"});
	}

	// Use this for initialization
	void Start () {
        recorder.OnEvent += OnEvent;
        Debug.Log("OnEvent registered: " + recorder.OnEvent);
        FindCamera();
		if (Hand != null) handAnimator = Hand.GetComponent<Animator>();

    }

    void FindCamera() {
        camera = Camera.main.gameObject;
        cameraTransform = camera.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (camera == null) FindCamera();
		Debug.DrawRay (cameraTransform.position, cameraTransform.forward * 100, Color.magenta);
		if (Physics.Raycast (cameraTransform.position, cameraTransform.forward.normalized, out hit, 500, ShotLayerMask)) {
			aimHasTarget = true;
			bulletTargetPoint = hit.point;
//			Debug.Log ("Hit: " + hit.transform.gameObject.name + " in " + hit.point);


		} else {
			aimHasTarget = false;
			bulletTargetPoint = cameraTransform.forward;
		}

		if (Input.GetMouseButtonDown (0)) {
			if (Time.time - lastShotTime > shotDelay) {
                Hashtable data = new Hashtable();
                data.Add("type", "shoot");
                data.Add("aimHasTarget", aimHasTarget);
                data.Add("bulletTargetPoint", bulletTargetPoint);
                data.Add("muzzlePosition", muzzlePositionHolder.transform.position);

                Shoot(data);
                recorder.recording.AddEvent(data);

				//lastShotTime = Time.time;
				//GameObject instanceBullet = Instantiate (bullet, muzzlePositionHolder.transform.position, Quaternion.identity);
				//instanceBullet.SetActive (true);
				//if (aimHasTarget) {
				//	instanceBullet.transform.rotation = Quaternion.LookRotation (bulletTargetPoint - muzzlePositionHolder.transform.position);
				//} else {
				//	instanceBullet.transform.rotation = Quaternion.LookRotation (bulletTargetPoint);
				//}
				//BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement> ();
				//bulletScript.ShotSource = this.gameObject;
                //
				//handAnimator.Play ("GunFire");

			}
		}
	}

    public void Shoot(Hashtable data) {
        lastShotTime = Time.time;
        GameObject instanceBullet = Instantiate(bullet, (Vector3)data["muzzlePosition"], Quaternion.identity);
        instanceBullet.SetActive(true);
        if ((bool)data["aimHasTarget"]) {
            instanceBullet.transform.rotation = Quaternion.LookRotation((Vector3)data["bulletTargetPoint"] - (Vector3)data["muzzlePosition"]);
        }
        else {
            instanceBullet.transform.rotation = Quaternion.LookRotation((Vector3)data["bulletTargetPoint"]);
        }
        BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement>();
        bulletScript.ShotSource = this.gameObject;

        if (handAnimator != null) handAnimator.Play("GunFire");
    }

    void OnEvent() {
        Debug.Log("Recorded Shoot playing");
        Puppet p = GetComponentInParent<Puppet>();
        if (p == null) { Debug.Log("No Puppet!"); return; }
        switch ((string)p.eventData["type"]) {
            case "shoot": {
                    Shoot(p.eventData);
                    break;
                }
        }
    }
}
