using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombDetonation : MonoBehaviour
{

    public float radius = 5.0f;
    public float power = 10.0f;
    public float upforce = 1.0f;
    public GameObject explosionEffect;

    void Start()
    {
        GetComponent<Health>().OnDie += Detonate;
    }

	private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Detonate();
            Debug.Log("I am being triggered");
        }
    }

    void Detonate()
    {

        Vector3 explosionPosition = gameObject.transform.position;
        GameObject particles = Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        particles.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            Health health = null;
            if ((health = col.gameObject.GetComponent<Health>()) != null)
            {
                health.Damage(100);
            }
            if (rb != null)
            {
                Debug.Log(col.gameObject.tag);
                rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);

            }
        }
        Destroy(gameObject);
    }
}
