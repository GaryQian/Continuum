using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour {

    AudioSource source;
    public AudioClip clip;
    public bool loop;
    public float delay = 0;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !source.isPlaying) {
            Invoke("StartSound", delay);
        }
    }

    void StartSound() {
        source.Play();
        CancelInvoke("StartSound");
    }

    private void OnDestroy() {
        source.Stop();
    }
}
