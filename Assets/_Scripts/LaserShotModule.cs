using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShotModule : MonoBehaviour {

    private LineRenderer LaserIndicatorRenderer;
    private LayerMask laserEndpointLayerMask;
    public Vector3 laserTargetPoint;
    public GameObject debug_ViewTargetting;
    public float chargeTime = 1f;
    public float beamTime = 0.25f;
    public float rechargeTime = 0.25f;
    public bool hasTargetedPlayer = false;
    public bool isAttacking = false;
    public bool isDead = false;
    bool hasStarted = false;

    public GameObject laserColliderObject;

    private Vector3 playerCoordinates;
    private RaycastHit hit;
    public LayerMask ShotLayerMask;

	// Use this for initialization
	void Start () {
        //Debug.Log("lasershot activated");
        LaserIndicatorRenderer = this.GetComponent<LineRenderer>();
        laserTargetPoint = Vector3.zero;
        ShotLayerMask = LayerMask.GetMask(new string[] { "Walls", "Player" });
        laserEndpointLayerMask = LayerMask.GetMask(new string[] { "Walls" });

        LaserIndicatorRenderer.useWorldSpace = true;

        //Invoke("DelayedStart", 10f);
        hasStarted = true;

	}

    private void DelayedStart(){
        hasStarted = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasStarted)
        {
            //Debug.Log("lasershot updating");

            playerCoordinates = GameManager.Instance.player.transform.position;
            //      Debug.DrawRay (this.gameObject.transform.position, playerCoordinates - this.gameObject.transform.position);
            if (laserTargetPoint != Vector3.zero)
            {
                LaserIndicatorRenderer.SetPositions(new Vector3[] { this.transform.position, laserTargetPoint });
            }


            if (Physics.Raycast(this.gameObject.transform.position, playerCoordinates - this.gameObject.transform.position + new Vector3(0, 1f, 0), out hit, ShotLayerMask))
            {
                //Debug.Log("Raycasting laser");
                Debug.Log (hit.collider.gameObject.name);
                Debug.DrawRay(this.gameObject.transform.position, hit.point - this.gameObject.transform.position, Color.magenta);


                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    //Debug.Log("Raycast hit player");

                    float speed = 3f;
                    var targetRotation = Quaternion.LookRotation(hit.transform.position - transform.position);

                    // Smoothly rotate towards the target point.
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

                    if (!isAttacking)
                    {
                        isAttacking = true;
                        StartCoroutine("attackFormRoutine");
                    }
                    else
                    {

                    }
                }
            }
        }
	}

    private IEnumerator attackFormRoutine()
    {
        //Debug.Log("Attack form routine");
        Vector3 delayedPlayerCoordinates = playerCoordinates;

        RaycastHit laserHit;
        Physics.Raycast(this.transform.position, delayedPlayerCoordinates - this.transform.position, out laserHit, 10000, laserEndpointLayerMask);
        laserTargetPoint = laserHit.point;
        //debug_ViewTargetting = laserHit.transform.gameObject;

        //Charge laser sequence
        LaserIndicatorRenderer.SetPositions(new Vector3[] { this.transform.position, laserTargetPoint });
        LaserIndicatorRenderer.enabled = true;
        SoundManager.PlaySfx(SoundManager.instance.chargeClip);
        yield return new WaitForSeconds(chargeTime);

        //Fire laser sequence
        LaserIndicatorRenderer.enabled = false;
        //LaserRenderer.SetPositions (new Vector3[]{TurretFireLight.transform.position, laserTargetPoint});
        //LaserRenderer.enabled = true;
        createLaserCollider(this.transform.position, laserTargetPoint);
        SoundManager.PlaySfx(SoundManager.instance.laserClip);
        yield return new WaitForSeconds(beamTime);

        //Laser turnoff sequence
        laserColliderObject.SetActive(false);
        yield return new WaitForSeconds(rechargeTime);

        //Return to patrol state
        isAttacking = false;
    }

    private void createLaserCollider(Vector3 origin, Vector3 target)
    {
        Vector3 spawnPosition = Vector3.Lerp(origin, target, 0.5f);
        laserColliderObject.transform.position = spawnPosition;
        laserColliderObject.transform.LookAt(target);
        laserColliderObject.transform.Rotate(0f, 90f, 90f);
        Vector3 scaleVector = laserColliderObject.transform.localScale;
        scaleVector.y = Vector3.Distance(spawnPosition, target) / this.transform.localScale.y / this.transform.parent.parent.localScale.y;
        laserColliderObject.transform.localScale = scaleVector;
        laserColliderObject.SetActive(true);
    }
}
