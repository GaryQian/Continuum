using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShooterScript : MonoBehaviour {

	public GameObject MuzzlePositionHolder;
	public GameObject Bullet;
	public GameObject Camera;
	private Transform cameraTransform;
	private RaycastHit hit;
	private Vector3 bulletTargetPoint;
	public LayerMask ShotLayerMask;
	private bool aimHasTarget = false;

	void Awake(){
		ShotLayerMask = LayerMask.GetMask(new string[]{"Default"});
	}

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (cameraTransform.position, cameraTransform.forward * 100, Color.magenta);
		if (Physics.Raycast (cameraTransform.position, cameraTransform.forward, out hit, 500, ShotLayerMask)) {
			aimHasTarget = true;
			bulletTargetPoint = hit.point;
//			Debug.Log ("Hit: " + hit.transform.gameObject.name + " in " + hit.point);


		} else {
			aimHasTarget = false;
			bulletTargetPoint = cameraTransform.forward;
		}

		if (Input.GetMouseButtonDown (0)) {
			GameObject instanceBullet = Instantiate (Bullet, MuzzlePositionHolder.transform.position, Quaternion.identity);
			instanceBullet.SetActive (true);
			if (aimHasTarget) {
				instanceBullet.transform.rotation = Quaternion.LookRotation (bulletTargetPoint - MuzzlePositionHolder.transform.position);
			} else {
				instanceBullet.transform.rotation = Quaternion.LookRotation (bulletTargetPoint);
			}
			BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement> ();
			bulletScript.ShotSource = this.gameObject;
		}
	}
}
