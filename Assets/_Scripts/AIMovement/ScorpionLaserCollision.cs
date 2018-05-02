using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionLaserCollision : MonoBehaviour {

    public float damage = 50f;
    public float dotInterval = 0.1f;
    private float nextDamageableTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player")){
            GameManager.Instance.player.GetComponent<Health>().Damage(damage, this.transform.parent.gameObject);
        }
	}

	private void OnTriggerStay(Collider other)
	{
        if (Time.time > nextDamageableTime + dotInterval && other.CompareTag("Player"))
        {
            nextDamageableTime = Time.time + dotInterval;
            GameManager.Instance.player.GetComponent<Health>().Damage(damage, this.transform.parent.gameObject);
        }
    }
}
