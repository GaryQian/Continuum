using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterModule : MonoBehaviour {

	public GameObject Gun;
	public GameObject Body;
	public GameObject Muzzle;
	public GameObject Bullet;
    public NavMeshAgent enemy;
	public float shootInterval = 0.5f;
    public float sightRange = 15.0f;
	public float detectionRange = 0.01f;
	private bool targetSighted = false;
	public Material NormalStateMaterial;
	public Material AlarmedStateMaterial;
	public bool randomizeShotInterval = true;

    Health hp;

	void Awake () {
	}

	// Use this for initialization
	void Start () {
        hp = GetComponentInParent<Health>();
		if (randomizeShotInterval) {
			InvokeRepeating ("shootBulletFromEnemy", 0f, Random.Range(0.1f, 0.5f));
		} else {
			InvokeRepeating ("shootBulletFromEnemy", 0f, shootInterval);
		}

	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.player)
        {
            transform.rotation = Quaternion.LookRotation(GameManager.Instance.player.transform.position - transform.position);
            Debug.DrawRay(transform.position, GameManager.Instance.player.transform.position - transform.position, Color.green);
        }
	}

	void FixedUpdate(){
        if (Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) < sightRange)
        {
            RaycastHit hit;
            if (GameManager.Instance.player && Physics.Raycast(transform.position, ((GameManager.Instance.player.transform.position + new Vector3(0, 0.5f, 0)) - transform.position).normalized, out hit, detectionRange))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    if (!targetSighted)
                    {
                        targetSighted = true;
                        Body.GetComponent<Renderer>().material = AlarmedStateMaterial;
                    }
                }
                else
                {
                    if (targetSighted)
                    {
                        targetSighted = false;
                        Body.GetComponent<Renderer>().material = NormalStateMaterial;
                    }
                }
            }
            else
            {
                if (targetSighted)
                {
                    targetSighted = false;
                    Body.GetComponent<Renderer>().material = NormalStateMaterial;
                }
            }
        }
	}

	void shootBulletFromEnemy() {
        if (!hp.isAlive) return;
        //enemy.destination = GameManager.Instance.player.transform.position;
		if (targetSighted) {
			GameObject instanceBullet = Instantiate (Bullet, Muzzle.transform.position, Quaternion.identity);
			instanceBullet.transform.rotation = Quaternion.Euler(Quaternion.LookRotation (GameManager.Instance.player.transform.position - transform.position).eulerAngles
				+ new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)));
			BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement> ();
			bulletScript.ShotSource = this.gameObject;
		}
	}

	void shootBullet(){

	}

}
