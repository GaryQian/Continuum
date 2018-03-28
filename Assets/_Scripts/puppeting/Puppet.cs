using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppetable {
    bool isPuppeted = false;
}

public class Puppet : MonoBehaviour {

    public Recording recording;
    public Vector3 targetPos;
    public Quaternion targetRot;

    public Rigidbody body;
    public float lerpSpeed = 10f;
    public float startPlaybackTime;

    public Action<EventRecord> OnEvent;

    public bool isPlayer;

    public void Setup(Recording r, bool isPlayer, Action<EventRecord> OnEvent) {
        recording = r;
        this.isPlayer = isPlayer;
        this.OnEvent = OnEvent;
    }

	// Use this for initialization
	void Start () {
        // We may not want to always playback on start
        StartPlayback();


        if (!isPlayer) PuppetRegister.puppets.Add(this);
        else PuppetRegister.playerPuppet = this;
    }

    public void StartPlayback() {
        startPlaybackTime = Time.time;
        GrabNextRecord();
        SnapToTarg();
        StartCoroutine(PullRecords());
    }
    public void SnapToTarg() {
        transform.position = targetPos;
        transform.rotation = targetRot;
    }

    IEnumerator PullRecords() {
        yield return new WaitForSeconds(recording.recordDelay);
        while (recording.HasRecord()) {
            GrabNextRecord();
            yield return new WaitForSeconds(recording.recordDelay);
        }
        SwitchToRecord();

    }
    void GrabNextRecord() {
        Record r = recording.NextRecord();
        targetPos = r.position;
        targetRot = r.rotation;
    }

    public void SwitchToRecord() {
        //Spawn Puppet Script Here
        if (isPlayer) {
            Destroy(gameObject);
        }
        else {
            Recorder p = gameObject.AddComponent<Recorder>();
            p.Setup(isPlayer, OnEvent);

            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed);
        Quaternion rot = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * lerpSpeed);
		if (body != null) {
            body.MovePosition(pos);
            body.MoveRotation(rot);
        }
        else {
            transform.position = pos;
            transform.rotation = rot;
        }

        if (recording.events.Count > 0 && Time.time - startPlaybackTime >= recording.events.Peek().timestamp) {
            EventRecord e = recording.events.Dequeue();
            if (OnEvent != null) OnEvent(e);
            else Debug.LogWarning("Warning: Event registered but no OnEvent method!");
        }
	}
}
