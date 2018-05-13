using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {

    Vector3 lockdownPos;
    bool locked = false;
    public float targScale;

    public AudioClip bassDrop;

    public GameObject hammer;
	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator Lockdown() {
        SoundManager.PlaySfx(bassDrop);
        while (true) {
            GameManager.Instance.player.transform.position = lockdownPos;
            transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, targScale, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, lockdownPos, Time.deltaTime);
            hammer.transform.position = Vector3.Lerp(hammer.transform.position, lockdownPos + new Vector3(2f, 5.5f, 2f), Time.deltaTime * 0.14f);
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
