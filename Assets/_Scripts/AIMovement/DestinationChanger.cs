using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestinationChanger : MonoBehaviour {

	public GameObject MeshHolderObject;
	private SkinnedMeshRenderer scorpionRenderer;
	public Material AlarmedMaterial, AfterShotMaterial;
	public NavMeshAgent agent;
	public List<GameObject> DestinationList;
	public int count, index;
    public float DestinationArrivalOKDistance;
	public GameObject TurretFireLight, TurretHeadLight, TurretTrackBeam;
	private Light LaserChargeLight;
	private LineRenderer LaserIndicatorRenderer;
	private LayerMask laserEndpointLayerMask;
	Vector3 laserTargetPoint;
    public GameObject debug_ViewTargetting;
    public float debug_magnitudeTillDestination;
    public GameObject debug_GoingToDestination;
    public float chargeTime = 1f;
    public float beamTime = 0.25f;
    public float rechargeTime = 0.25f;
    public float detectionRadius = 40f;
	public bool hasTargetedPlayer = false;
	public bool isAttacking = false;
	public bool isDead = false;

    public GameObject laserColliderObject;
      
	private Vector3 playerCoordinates;
	private RaycastHit hit;
	public LayerMask ShotLayerMask;

	// Use this for initialization
	void Start () {
        detectionRadius *= Mathf.Pow(this.gameObject.transform.localScale.y, 0.25f);
        ShotLayerMask = LayerMask.GetMask(new string[]{"Walls", "Player"});
		laserEndpointLayerMask = LayerMask.GetMask (new string[]{ "Walls" });
		scorpionRenderer = MeshHolderObject.GetComponent<SkinnedMeshRenderer> ();
		LaserChargeLight = TurretFireLight.GetComponent<Light> ();
		    //LaserRenderer = TurretFireLight.GetComponent<LineRenderer> ();
		LaserIndicatorRenderer = TurretTrackBeam.GetComponent<LineRenderer> ();
	
		laserTargetPoint = Vector3.zero;
		    //LaserRenderer.useWorldSpace = true;
		LaserIndicatorRenderer.useWorldSpace = true;
		index = 0;
		agent.SetDestination(DestinationList [index].transform.position);
		count = DestinationList.Count;
	}
	
	// Update is called once per frame
	void Update () {
		playerCoordinates = GameManager.Instance.player.transform.position;
//		Debug.DrawRay (this.gameObject.transform.position, playerCoordinates - this.gameObject.transform.position);
		if (laserTargetPoint != Vector3.zero) {
			LaserIndicatorRenderer.SetPositions (new Vector3[]{ TurretFireLight.transform.position, laserTargetPoint });
			    //LaserRenderer.SetPositions (new Vector3[]{ TurretFireLight.transform.position, laserTargetPoint });
		}


        if (Physics.Raycast(this.gameObject.transform.position, playerCoordinates - this.gameObject.transform.position + new Vector3(0, 1f, 0), out hit, detectionRadius, ShotLayerMask))
        {
            //Debug.Log (hit.collider.gameObject.name);
            Debug.DrawRay(this.gameObject.transform.position, hit.point - this.gameObject.transform.position, Color.magenta);

            Debug.Log("Ray SHOT");

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Found Player");

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
            else
            {
                if (!isAttacking)
                {
                    agent.isStopped = false;
                    Debug.Log("Patrolling");
                    patrolRoutine();
                }
            }
        }
        else
        {
            if (!isAttacking)
            {
                agent.isStopped = false;
                Debug.Log("Patrolling");
                patrolRoutine();
            }
        }
	}


	private void patrolRoutine(){
        debug_magnitudeTillDestination = (gameObject.transform.position - DestinationList[index].transform.position).magnitude;
        debug_GoingToDestination = DestinationList[index];
        if ((gameObject.transform.position - DestinationList [index].transform.position).magnitude < DestinationArrivalOKDistance) {			
			agent.SetDestination (DestinationList [calculateIndex()].transform.position);
		}
	}

	private IEnumerator attackFormRoutine(){
		//Stop Movement of Scorpion
		agent.isStopped = true;
		Vector3 delayedPlayerCoordinates = playerCoordinates;




		RaycastHit laserHit;
        Physics.Raycast (TurretFireLight.transform.position, delayedPlayerCoordinates - TurretFireLight.transform.position, out laserHit, 10000, laserEndpointLayerMask);
		laserTargetPoint = laserHit.point;
        debug_ViewTargetting = laserHit.transform.gameObject;
			
		//Charge laser sequence
		scorpionRenderer.material = AlarmedMaterial;
		LaserChargeLight.enabled = true;
		LaserIndicatorRenderer.SetPositions (new Vector3[]{TurretFireLight.transform.position, laserTargetPoint});
		LaserIndicatorRenderer.enabled = true;

        yield return new WaitForSeconds(chargeTime);

		//Fire laser sequence
		scorpionRenderer.material = AfterShotMaterial;
		LaserIndicatorRenderer.enabled = false;
		    //LaserRenderer.SetPositions (new Vector3[]{TurretFireLight.transform.position, laserTargetPoint});
		    //LaserRenderer.enabled = true;
        createLaserCollider(TurretFireLight.transform.position, laserTargetPoint);
        yield return new WaitForSeconds(beamTime);

		//Laser turnoff sequence
		LaserChargeLight.enabled = false;
		    //LaserRenderer.enabled = false;
        laserColliderObject.SetActive(false);
        yield return new WaitForSeconds(rechargeTime);

		//Return to patrol state
		scorpionRenderer.material = AlarmedMaterial;
		isAttacking = false;
	}

	void SmoothLook(Vector3 newDirection){
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime);
	}

	private void fireLaserRoutine(){

	}

	private void deathRoutine(){

	}

    private void createLaserCollider(Vector3 origin, Vector3 target){
        Vector3 spawnPosition = Vector3.Lerp(origin, target, 0.5f);
        laserColliderObject.transform.position = spawnPosition;
        laserColliderObject.transform.LookAt(target);
        laserColliderObject.transform.Rotate(0f, 90f, 90f);
        Vector3 scaleVector = laserColliderObject.transform.localScale;
        scaleVector.y = Vector3.Distance(spawnPosition, target) / this.transform.localScale.y;
        laserColliderObject.transform.localScale = scaleVector;
        laserColliderObject.SetActive(true);
    }

	private int calculateIndex(){
		if (index++ >= DestinationList.Count - 1)
			index = 0;
		return index;
	}


}
