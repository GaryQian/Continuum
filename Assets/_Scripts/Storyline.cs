using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storyline : MonoBehaviour {

    public AudioClip clip;
    public float delay = 0;
    public bool destroyOnPlay = true;

    public string msg = "";

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Entered" + other.gameObject.layer);
        if (other.gameObject.layer == 10) {
            controller = other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            Invoke("Play", delay);
        }
    }

    void Play() {
        if (clip != null) controller.StoryAudioSource.PlayOneShot(clip);
        if (msg != null && msg != "") UIManager.instance.SendMessage(msg);
        if (destroyOnPlay) Destroy(gameObject);
    }
}
