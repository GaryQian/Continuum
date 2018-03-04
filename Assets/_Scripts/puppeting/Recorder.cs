using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

// Expand this as unit types increase.
public enum UnitType { player, enemy, enemy2, etc }

public class Record {
    public Vector3 position;
    public Quaternion rotation;
    public float hp;
    public float timestamp;

    public Record(Vector3 position, Quaternion rotation, float hp = -1f) {
        this.position = position;
        this.rotation = rotation;
        timestamp = Time.time;

        this.hp = (hp != -1f) ? hp : -1f;
    }
}

public class EventRecord {
    public Hashtable data;
    public int type;
    public float timestamp;

    public EventRecord(float startTime, int type, Hashtable data) {
        this.data = data;
        this.type = type;
        timestamp = Time.time - startTime;
    }
}

public class Recording {
    public Queue<Record> records;
    public Queue<EventRecord> events;
    public UnitType type;
    public float recordDelay;
    public int maxRecords;

    public Recording(UnitType type, float recordDelay = 0.1f) {
        records = new Queue<Record>();
        events = new Queue<EventRecord>();
        this.type = type;
        this.recordDelay = recordDelay;
        this.maxRecords = (int)(15f / recordDelay);
    }

    public void AddRecord(Record r) {
        records.Enqueue(r);
        if (records.Count > maxRecords) {
            records.Dequeue();
        }
    }

    public Record NextRecord() {
        return records.Dequeue();
    }

    public bool HasRecord() {
        return records.Count >= 1;
    }

    public float GetDuration() {
        return records.Count * recordDelay;
    }
}

public class Recorder : MonoBehaviour {
    public UnitType type;
    public float recordDelay = 0.1f;
    public Recording recording;

    public bool isRecording;
    float startRecordTime;
    public bool recordOnStart = true;
    public Health health;

    /// <summary>
    /// If this entity uses events, please ensure that you add an event handler method using:
    ///     void Start() {
    ///         GetComponent<Recorder>().OnEvent += methodNameThatTakesAnEventRecord;
    ///     }
    ///     void methodNameThatTakesAnEventRecord(EventRecord e) {
    ///         switch (e.type) {
    ///             ...
    ///         }
    ///     }
    /// To record an event, use the following:
    ///     Hashtable hash = new Hashtable();
    ///     hash.Add("somekey", data);
    ///     GetComponent<Recorder>().AddEvent(EventType.typenamehere, hash);
    ///     
    /// To access hashed parameters:
    ///     ... (CastToCorrectType) e.data["somekey"] ...
    /// </summary>
    public Action<EventRecord> OnEvent;

    public bool isPlayer = false;

    public void Setup(bool isPlayer, Action<EventRecord> OnEvent) {
        this.isPlayer = isPlayer;
        this.OnEvent = OnEvent;
    }

    private void OnEnable() {
        recording = new Recording(type, recordDelay);
    }

    // Use this for initialization
    void Start () {
        recording = new Recording(type, recordDelay);
        if (recordOnStart) StartRecording();

        PuppetRegister.recorders.Add(this);
        if (isPlayer) PuppetRegister.playerRecorder = this;
	}

    public void StartRecording() {
        isRecording = true;
        startRecordTime = Time.time;
        StartCoroutine(Record());
    }

    public void StopRecording() {
        isRecording = false;
        StopCoroutine("Record");
    }

    public void SwitchToPuppet() {
        StopRecording();
        //Spawn Puppet Script Here
        if (isPlayer) {
            Debug.Log("Creating clone");
            GameObject clone = Instantiate(gameObject);
            Puppet p = clone.AddComponent<Puppet>();
            p.Setup(recording, isPlayer, OnEvent);
            Destroy(clone.GetComponentInChildren<Camera>().gameObject);
            Destroy(clone.GetComponentInChildren<FirstPersonController>());
            Destroy(clone.GetComponent<Recorder>());
            Invoke("StartRecording", 15f);
            transform.position = recording.records.Peek().position;

            StopRecording();
            OnEnable();

        }
        else {
            Puppet p = gameObject.AddComponent<Puppet>();
            p.Setup(recording, isPlayer, OnEvent);
            Destroy(this);
        }
    }

    /// <summary>
    /// To record an event, use the following:
    ///     Hashtable hash = new Hashtable();
    ///     hash.Add("somekey", data);
    ///     GetComponent<Recorder>().AddEvent(EventType.typenamehere, hash);
    /// </summary>
    public void AddEvent(int type, Hashtable data) {
        recording.events.Enqueue(new EventRecord(startRecordTime, type, data));
    }

    IEnumerator Record() {
        while (isRecording) {
            recording.AddRecord(new Record(transform.position, transform.rotation, (health != null ? health.health : -1f)));
            yield return new WaitForSeconds(recordDelay);
        }
    }

}
