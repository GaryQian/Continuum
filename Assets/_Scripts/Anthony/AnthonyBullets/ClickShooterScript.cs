using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickShooterScript : MonoBehaviour {

	public GameObject Hand;
	private Animator handAnimator;
	public GameObject muzzlePositionHolder;
	public GameObject bullet;
	public GameObject mainCamera;
	private Transform cameraTransform;
	private RaycastHit hit;
	private Vector3 bulletTargetPoint;
	public LayerMask ShotLayerMask;

    public Recorder recorder;
    public Puppet puppet;


    public float shotDelay = 0.15f;
	private bool aimHasTarget = false;
	private float lastShotTime = 0f;

    public bool isClone = false;

	void Awake(){
		ShotLayerMask = LayerMask.GetMask(new string[]{"Default"});
	}

	// Use this for initialization
	void Start () {
        if (puppet != null) puppet.OnEvent += OnEvent;
        Debug.Log("OnEvent registered: " + recorder.OnEvent);
        FindCamera();
		if (Hand != null) handAnimator = Hand.GetComponent<Animator>();

    }

    void FindCamera() {
        mainCamera = Camera.main.gameObject;
        cameraTransform = mainCamera.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (isClone) return;
        if (mainCamera == null) FindCamera();
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
        if (puppet.eventData == null) { Debug.Log("EventData null"); return; }
        switch ((string)puppet.eventData["type"]) {
            case "shoot": {
                    Shoot(puppet.eventData);
                    break;
                }
        }
    }
}
