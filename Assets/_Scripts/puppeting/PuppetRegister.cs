using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetRegister : MonoBehaviour {
    public static PuppetRegister Instance;

    public static List<Recorder> recorders;
    public static List<Puppet> puppets;
    public static List<GameObject> dead;

    public static Recorder playerRecorder;
    public static Puppet playerPuppet;

    public static bool ready;
    public static bool inRewind;

    public static float duration = 15f;
    public static float timer;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Use this for initialization
    void Start() {
        InitLists();
        RemoveDistortion();
        inRewind = false;
        timer = 0;
    }

    public void InitLists() {
        recorders = new List<Recorder>();
        puppets = new List<Puppet>();
        dead = new List<GameObject>();

        ready = false;
        Invoke("SetReady", duration);
    }

    public void SetReady() {
        Debug.Log("ready to rewind");
        ready = true;
    }

    public void RemoveDistortion() {
        TimeDistortion.targ = 0;
        inRewind = false;
    }

    public void Rewind() {
        if (!ready) return;
        Debug.Log("REWINDING");
        TimeDistortion.targ = 1f;
        ready = false;
        inRewind = true;
        Invoke("SetReady", duration * 2f);
        Invoke("RemoveDistortion", duration);
        foreach (Recorder r in recorders) {
            if (r != null) r.SwitchToPuppet();
        }
        recorders = new List<Recorder>();
        foreach (GameObject obj in dead) {

            if (obj != null) obj.SetActive(true);
        }
        dead = new List<GameObject>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Rewind();
        }
        if (inRewind) {
            timer = Mathf.Max(0, timer - Time.deltaTime);
        }
        else {
            timer = Mathf.Min(duration, timer + Time.deltaTime);
        }
    }
}
