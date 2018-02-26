using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{

    public GameManager gm;
    public Transform[] destinations;
    NavMeshAgent enemy;
    public int currentDestination = 0;
    public int totalDestinations = 0;
    public float destinationChangeDist = 15.0f;
    public bool playerDetected;

    // Use this for initialization
    void Start()
    {
        gm = GameManager.Instance;
        enemy = GetComponent<NavMeshAgent>();
        enemy.destination = destinations[0].position;
        currentDestination = 0;
        totalDestinations = destinations.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.player)
        {
            DetectPlayer();
            ChangeDestination();
        }
    }

    void DetectPlayer() {
        if (Vector3.Distance(this.gameObject.transform.position, gm.player.transform.position) < destinationChangeDist) {
            playerDetected = true;
        }
    }

    void ChangeDestination() {
        if (playerDetected) {
            enemy.destination = gm.player.transform.position;
            Debug.Log("Chasing Player.");
        }
        else if (Vector3.Distance(this.gameObject.transform.position, destinations[currentDestination].position) < destinationChangeDist) {
            currentDestination = (currentDestination + 1) % totalDestinations;
            enemy.destination = destinations[currentDestination].transform.position;
            Debug.Log("Destination Changed.");
        }
    }
}
