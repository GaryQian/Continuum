using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour {

    //GameManager gm;
    NavMeshAgent enemy;
    public Transform destination;
    public float enemySightRange = 30.0f;
    public bool playerInSight = false;
    LayerMask mask;

    // TEMP! fix GM
    public GameObject player;
        
	// Use this for initialization
	void Start () {
        //gm = GameManager.Instance;
        mask = LayerMask.GetMask("Ignore Raycast");
        enemy = GetComponent<NavMeshAgent>();
        enemy.destination = destination.position;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(enemy.transform.localPosition);
        Debug.Log(player.transform.localPosition);
        enemy.transform.LookAt(player.transform);
        if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, enemySightRange, mask.value)) {
            Debug.Log("Player on sight!!!");
        }
	}
}
