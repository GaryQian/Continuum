using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{

    NavMeshAgent enemy;
    public int currentDestination;
    public float destinationChangeDist = 15.0f;
    public bool playerDetected;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemy.destination = WorldManager.Instance.destinations[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player)
        {
            DetectPlayer();
            ChangeDestination();
        }
    }

    void DetectPlayer() {
        if (Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.player.transform.position) < destinationChangeDist) {
            playerDetected = true;
        }
    }

    void ChangeDestination() {
        Vector3 pos = WorldManager.Instance.destinations[currentDestination].position;
        if (playerDetected) {
            if (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) > 5f)
                pos = GameManager.Instance.player.transform.position;
            else
                pos = transform.position;
            enemy.destination = pos;
            //Debug.Log("Chasing Player.");
        }
        else if (Vector3.Distance(this.gameObject.transform.position, pos) < destinationChangeDist) {
            currentDestination = (currentDestination + 1) % WorldManager.Instance.destinations.Length;
            enemy.destination = WorldManager.Instance.destinations[currentDestination].transform.position;
            //Debug.Log("Destination Changed.");
        }
    }
}
