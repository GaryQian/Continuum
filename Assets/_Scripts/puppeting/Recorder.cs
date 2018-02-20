using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        this.hp = (hp != -1f) ? hp : -1f ;
    }
}

public class Recording {
    public Queue<Record> records;
    public UnitType type;
    public float recordDelay;

    public Recording(UnitType type, float recordDelay = 0.1f) {
        records = new Queue<Record>();
        this.type = type;
        this.recordDelay = recordDelay;
    }

    public void AddRecord(Record r) {
        records.Enqueue(r);
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
    public bool recordOnStart = true;
    public Health health;

	// Use this for initialization
	void Start () {
        recording = new Recording(type, recordDelay);
        if (recordOnStart) StartRecording();
	}

    public void StartRecording() {
        isRecording = true;
        StartCoroutine(Record());
    }

    public void StopRecording() {
        isRecording = false;
        StopCoroutine("Record");
    }

    public void SwitchToPuppet() {
        StopRecording();
        //Spawn Puppet Script Here
        Puppet p = gameObject.AddComponent<Puppet>();
        p.Setup(recording);

        Destroy(this);
    }

    IEnumerator Record() {
        while (isRecording) {
            recording.AddRecord(new Record(transform.position, transform.rotation, (health != null ? health.health : -1f)));
            yield return new WaitForSeconds(recordDelay);
        }
    }

}
