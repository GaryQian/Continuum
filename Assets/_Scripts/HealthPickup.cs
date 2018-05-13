using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float amountToHeal = 30.0f;
	public float speed = 10f;

	private void Update()
	{
		transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag.Equals("Player"))
        {
            Health health = (Health)other.gameObject.GetComponent<Health>();
            health.Heal(amountToHeal);
            Destroy(gameObject);
        }
	}
}
