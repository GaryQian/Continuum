﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetRegister : MonoBehaviour{
    public static PuppetRegister Instance;

    public static List<Recorder> recorders;
    public static List<Puppet> puppets;

    public static Recorder playerRecorder;
    public static Puppet playerPuppet;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        InitLists();
	}

    public void InitLists() {
        recorders = new List<Recorder>();
        puppets = new List<Puppet>();
    }
	
	public void Rewind() {
        foreach (Recorder r in recorders) {
            if (r != null) r.SwitchToPuppet();
        }
        recorders = new List<Recorder>();

        //playerRecorder.SwitchToPuppet();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Rewind();
        }
    }
}
