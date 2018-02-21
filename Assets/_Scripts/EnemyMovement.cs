using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour {

    public GameManager gm;
	public Transform destination;
	NavMeshAgent enemy;
    LayerMask mask;
        
	// Use this for initialization
	void Start () {
        gm = GameManager.Instance;
        enemy = GetComponent<NavMeshAgent>();
        enemy.destination = destination.position;
	}
	
	// Update is called once per frame
	void Update () {
		enemy.destination = gm.player.transform.position;
	}

}
