using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
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

    public UnityAction OnEvent;
    public Action<Hashtable> OnEventAction;

    public bool isPlayer;



    //PARAMS FOR ONEVENT
    public Hashtable eventData;
    //PARAMS FOR ONEVENT


    public void Setup(Recording r, bool isPlayer, UnityAction OnEvent) {
        recording = r;
        this.isPlayer = isPlayer;
        this.OnEvent = OnEvent;
        Health hp = GetComponent<Health>();
        if (hp != null) {
            hp.OnDie += OnDie;
        }
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
        StartCoroutine(ReadEvents());
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
    IEnumerator ReadEvents() {
        float elapsed = 0;
        while (recording.HasEvent()) {
            if (recording.events.Peek().timestamp < elapsed) {
                EventRecord e = recording.events.Dequeue();
                eventData = e.data;
                if (OnEvent != null) { OnEvent(); Debug.Log("Calling OnEvent"); }
                else Debug.LogWarning("Warning: Event registered but no OnEvent method!");
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
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

        //if (recording.events.Count > 0 && Time.time - startPlaybackTime >= recording.events.Peek().timestamp) {
        //    EventRecord e = recording.events.Dequeue();
        //    if (OnEvent != null) OnEvent();
        //    else Debug.LogWarning("Warning: Event registered but no OnEvent method!");
        //}
	}
		
	public void SwapPlayerModel(){
		Debug.Log ("swapping enabled");
		Debug.Log (gameObject.name);

		gameObject.GetComponent<PlayerPuppetModelHolder>().ActivatePuppetModel ();
//		Debug.Log (gameObject.GetComponentInChildren<PlayerPuppetModelFlag> ().gameObject.name + "enabled");
//		gameObject.GetComponentInChildren<PlayerPuppetModelFlag> ().gameObject.SetActive (true);
	}

    private void OnDie() {
        Destroy(gameObject);
    }
}
