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
    public float lerpSpeed = 5f;

    public bool isPlayer;

    public void Setup(Recording r) {
        recording = r;
    }

	// Use this for initialization
	void Start () {
        // We may not want to always playback on start
        StartPlayback();


        if (!isPlayer) PuppetRegister.puppets.Add(this);
        else PuppetRegister.playerPuppet = this;
    }

    public void StartPlayback() {
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
    }
    void GrabNextRecord() {
        Record r = recording.NextRecord();
        targetPos = r.position;
        targetRot = r.rotation;
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
	}
}
