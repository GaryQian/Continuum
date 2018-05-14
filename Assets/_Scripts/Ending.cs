using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {
    public static Ending Instance; ///Singleton Instance

    public AudioClip spaceOddClip;
    public float spaceVol;
    public AudioClip line1;
    public AudioClip line2;
    public AudioClip o;
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

    public int state = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        controller = GameManager.Instance.player.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    public void Begin() {
        NextState();
    }

    public void NextState() {
        state++;
        switch (state) {
            case 1: {
                    Invoke("RaisePot", 4f);
                    controller.StoryAudioSource.PlayOneShot(line1, 1);
                    break;
                }
            case 2: {
                    SoundManager.instance.musicSource2.clip = spaceOddClip;
                    SoundManager.instance.musicSource2.loop = false;
                    SoundManager.instance.musicSource2.volume = spaceVol;
                    SoundManager.instance.musicSource2.Play();
                    Invoke("ToWhite", 60 + 22);
                    Invoke("PlayO", 60 + 34);
                    Invoke("Finish", 60 + 34 + 1);
                    break;
                }
            default: {

                    break;
                }
        }
    }
	
	// Update is called once per frame
	void Update () {

		switch(state) {
            case 1: {
                    SoundManager.instance.musicSource2.volume = Mathf.Lerp(SoundManager.instance.musicSource2.volume, 0, Time.deltaTime);
                    if (SoundManager.instance.musicSource2.volume < 0.01f) {
                        NextState();
                    }
                    break;
                }
            case 2: {

                    break;
                }
            default: {

                    break;
                }
        }

	}

    void RaisePot() {
        GameObject.Find("Pot").GetComponent<RisePlatform>().Begin();
        GameObject.Find("PotRamp1").GetComponent<RisePlatform>().Begin();
        GameObject.Find("PotRamp2").GetComponent<RisePlatform>().Begin();
    }

    void ToWhite() {
        UIManager.instance.ToWhite();
    }

    public void PlayO() {
        SoundManager.PlaySfx(o);
    }

    void Finish() {
        SceneManager.LoadScene("Title");
    }


}
