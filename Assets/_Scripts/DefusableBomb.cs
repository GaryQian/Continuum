using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefusableBomb : MonoBehaviour
{

    public float radius = 5.0f;
    public float power = 10.0f;
    public float upforce = 1.0f;
    public GameObject explosionEffect;
    public float defusalTime = 5.0f;
    public float timer = 15.0f;
    public Slider defusalSlider;

    float defusalProgress = 0.0f;
    bool defusing = false;
    bool defused = false;

    void Start()
    {
        GetComponent<Health>().OnDie += Detonate;
    }

    private void FixedUpdate() {
        Debug.Log(defusing);
        timer -= Time.deltaTime;
        if (timer <= 0 && !defused) {
            Detonate();
        }
        if (defusing) {
            defusalProgress += Time.deltaTime;
            defusalSlider.value = defusalProgress / defusalTime;
        }
        if (defusalProgress >= defusalTime)
        {
            defused = true;
            defusalSlider.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Player" && Input.GetKey("f")){
            defusing = true;
            defusalSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            defusing = false;
            defusalSlider.gameObject.SetActive(false);
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
