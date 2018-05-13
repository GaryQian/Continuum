using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

    public AudioClip clip;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    private void Start() {
        controller = GameManager.Instance.player.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    private void OnDestroy() {
        Debug.Log("Playing line!");
        controller.StoryAudioSource.PlayOneShot(clip);
    }
}
