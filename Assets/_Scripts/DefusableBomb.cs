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
    public GameObject sliderPrefab;

    float defusalProgress = 0.0f;
    bool defusing = false;
    bool defused = false;

    void Start() {
        //GetComponent<Health>().OnDie += Detonate;
        PuppetRegister.bombs.Add(gameObject);
    }

    private void Update() {
        //Debug.Log(defusing);
        timer -= Time.deltaTime;
        if (timer <= 0 && !defused) {
            Detonate();
        }
        if (defusing) {
            defusalProgress += Time.deltaTime;
            defusalSlider.value = defusalProgress / defusalTime;
        }
        if (defusalProgress >= defusalTime) {
            defused = true;
            defusalSlider.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        string s = "00:";
        if (timer < 10f) s += "0";
        s += (int)timer + ":";
        float remain = 10f * (timer - (int)timer);
        //if (remain < 10f) s += 0;
        s += (int)remain;
        GetComponentInChildren<FollowText>().SetText(s);
    }

    private void OnTriggerStay(Collider col) {
        if (col.gameObject.tag == "Player"){
            defusing = true;
            if (defusalSlider == null) {
                defusalSlider = Instantiate(sliderPrefab, UIManager.instance.gameObject.transform).GetComponent<Slider>();
            }
            defusalSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            defusing = false;
            defusalSlider.gameObject.SetActive(false);
        }
    }

    public void Rewind() {
        timer += PuppetRegister.duration;
    }


    void Detonate()
    {
        Debug.Log("boom");
        GameObject canvas = UIManager.instance.gameObject;
        Subtitle txt = (Subtitle) canvas.GetComponent<Subtitle>();
        if (txt != null) txt.setInput("Boom", 20);
           
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
        if (defusalProgress >= defusalTime) GameManager.Instance.player.GetComponent<Health>().Damage(100000000f);
    }
}
