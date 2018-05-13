using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneDirectionShooter : MonoBehaviour {

    public GameObject BossBullet;
    public float shotInterval = 1f;


	// Use this for initialization
	void Start () {
        InvokeRepeating("Shoot", 0f, shotInterval);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Shoot(){
        Vector3 shotDirection = this.transform.position - this.transform.parent.position;
        GameObject instanceBullet = Instantiate(BossBullet, this.transform.position, Quaternion.identity);
        instanceBullet.transform.LookAt(GameManager.Instance.player.transform);
        BulletMovement bulletScript = instanceBullet.GetComponent<BulletMovement>();
        bulletScript.ShotSource = this.gameObject;
    }
}
