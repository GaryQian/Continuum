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
	private int count, index;
	public float OKDistance = 3f;
	public GameObject TurretFireLight, TurretHeadLight, TurretTrackBeam;
	private Light LaserChargeLight;
	private LineRenderer LaserRenderer, LaserIndicatorRenderer;
	private LayerMask laserEndpointLayerMask;
	Vector3 laserTargetPoint;

	public bool hasTargetedPlayer = false;
	public bool isAttacking = false;
	public bool isDead = false;

	private Vector3 playerCoordinates;
	private RaycastHit hit;
	public LayerMask ShotLayerMask;

	// Use this for initialization
	void Start () {		
		ShotLayerMask = LayerMask.GetMask(new string[]{"Walls", "Player"});
		laserEndpointLayerMask = LayerMask.GetMask (new string[]{ "Walls" });
		scorpionRenderer = MeshHolderObject.GetComponent<SkinnedMeshRenderer> ();
		LaserChargeLight = TurretFireLight.GetComponent<Light> ();
		LaserRenderer = TurretFireLight.GetComponent<LineRenderer> ();
		LaserIndicatorRenderer = TurretTrackBeam.GetComponent<LineRenderer> ();
	
		laserTargetPoint = Vector3.zero;
		LaserRenderer.useWorldSpace = true;
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
			LaserRenderer.SetPositions (new Vector3[]{ TurretFireLight.transform.position, laserTargetPoint });
		}


		if (Physics.Raycast (this.gameObject.transform.position, playerCoordinates - this.gameObject.transform.position + new Vector3(0, 1f, 0), out hit, 1000, ShotLayerMask)) {
			Debug.Log (hit.collider.gameObject.name);
			Debug.DrawRay (this.gameObject.transform.position, hit.point - this.gameObject.transform.position, Color.magenta);


			if (hit.collider.gameObject.CompareTag ("Player")) {
				if (!isAttacking) {
					isAttacking = true;
					this.gameObject.transform.LookAt (playerCoordinates);
					StartCoroutine ("attackFormRoutine");
				}
			} else {
				if (!isAttacking) {
					agent.isStopped = false;
					patrolRoutine ();
				}
			}
		}
	}


	private void patrolRoutine(){
		if ((gameObject.transform.position - DestinationList [index].transform.position).magnitude < OKDistance) {			
			agent.SetDestination (DestinationList [calculateIndex()].transform.position);
		}
	}

	private IEnumerator attackFormRoutine(){
		//Stop Movement of Scorpion
		agent.isStopped = true;
		Vector3 delayedPlayerCoordinates = playerCoordinates;




		RaycastHit laserHit;
		Physics.Raycast (TurretFireLight.transform.position, delayedPlayerCoordinates - this.gameObject.transform.position + new Vector3 (0, 0, 0), out laserHit, 10000, laserEndpointLayerMask);
		laserTargetPoint = laserHit.point;
			
		//Charge laser sequence
		scorpionRenderer.material = AlarmedMaterial;
		LaserChargeLight.enabled = true;
		LaserIndicatorRenderer.SetPositions (new Vector3[]{TurretFireLight.transform.position, laserTargetPoint});
		LaserIndicatorRenderer.enabled = true;

		yield return new WaitForSeconds(3f);

		//Fire laser sequence
		scorpionRenderer.material = AfterShotMaterial;
		LaserIndicatorRenderer.enabled = false;
		LaserRenderer.SetPositions (new Vector3[]{TurretFireLight.transform.position, laserTargetPoint});
		LaserRenderer.enabled = true;
		yield return new WaitForSeconds(3f);

		//Laser turnoff sequence
		LaserChargeLight.enabled = false;
		LaserRenderer.enabled = false;

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

	private int calculateIndex(){
		if (index++ >= DestinationList.Count - 1)
			index = 0;
		return index;
	}


}
