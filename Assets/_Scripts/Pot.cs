using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {

    Vector3 lockdownPos;
    bool locked = false;
    public float targScale;
    public float targWhiteScale;

    public AudioClip bassDrop;

    public GameObject hammer;
    public GameObject white;
    public GameObject floor;
	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator Lockdown() {
        SoundManager.PlaySfx(bassDrop);
        foreach (Collider c in GetComponents<Collider>()) {
            Destroy(c);
        }
        Destroy(floor);
        Ending.Instance.PlayO();
        while (true) {
            GameManager.Instance.player.transform.position = lockdownPos;
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, targScale, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, lockdownPos, Time.deltaTime);
            hammer.transform.position = Vector3.Lerp(hammer.transform.position, lockdownPos + new Vector3(2f, 5.5f, 2f), Time.deltaTime * 0.14f);
            white.transform.localScale = new Vector3(1f, 16f, 1f) * Mathf.Lerp(white.transform.localScale.x, targWhiteScale, Time.deltaTime * 0.1f);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (locked) return;
        if (other.gameObject.layer == 10) {
            lockdownPos = GameManager.Instance.player.transform.position;
            locked = true;
            StartCoroutine(Lockdown());
            Ending.Instance.NextState();
        }
    }
}
