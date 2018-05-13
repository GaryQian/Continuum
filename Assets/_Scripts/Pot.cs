using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {

    Vector3 lockdownPos;
    bool locked = false;
    public float targScale;

    public AudioClip bassDrop;
	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator Lockdown() {
        SoundManager.PlaySfx(bassDrop);
        while (true) {
            GameManager.Instance.player.transform.position = lockdownPos;
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, targScale, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, lockdownPos, Time.deltaTime);
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (locked) return;
        if (other.gameObject.layer == 10) {
            lockdownPos = GameManager.Instance.player.transform.position;
            locked = true;
            StartCoroutine(Lockdown());
        }
    }
}
