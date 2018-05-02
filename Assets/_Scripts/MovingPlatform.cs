using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovingPlatform : MonoBehaviour {
    public Vector3 endPos;
    public Vector3 startPos;
    public bool autoSetStart = false;
    public float duration = 5f;
    float t;
    bool dir = true;
    public float initOffset = 0;
    public bool freeze = false;
    public Rigidbody body;
	// Use this for initialization
	void Start () {
        if (autoSetStart) startPos = transform.position;
        t = 1.0f * initOffset * duration;
        //Debug.Log("" + t + " " + initOffset + " " + duration);
        body = GetComponent<Rigidbody>();
        FixedUpdate();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (freeze) return;
        t += Time.fixedDeltaTime;
        if (t > duration) {
            t = 0;
            dir = !dir;
        }
        float offset = t / duration;
        if (dir) body.MovePosition(new Vector3(Mathf.SmoothStep(startPos.x, endPos.x, offset),
                                               Mathf.SmoothStep(startPos.y, endPos.y, offset),
                                               Mathf.SmoothStep(startPos.z, endPos.z, offset)));
        else body.MovePosition(new Vector3(Mathf.SmoothStep(endPos.x, startPos.x, offset),
                                               Mathf.SmoothStep(endPos.y, startPos.y, offset),
                                               Mathf.SmoothStep(endPos.z, startPos.z, offset)));
    }

    public void SetStart() {
        startPos = transform.position;
    }

    public void SetEnd() {
        endPos = transform.position;
    }

    public void GoToStart() {
        transform.position = startPos;
    }

    public void GoToEnd() {
        transform.position = endPos;
    }

    public void RandomInitOffset() {
        initOffset = (float)Random.Range(0f, 1f);
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collided");
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<PlayerFollower>().player.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.layer == 10) {
            collision.gameObject.GetComponent<PlayerFollower>().player.transform.SetParent(transform.root);
        }
    }
}

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        MovingPlatform myScript = (MovingPlatform)target;
        if (GUILayout.Button("Set Start")) {
            myScript.SetStart();
        }

        if (GUILayout.Button("Set End")) {
            myScript.SetEnd();
        }

        if (GUILayout.Button("Go To Start")) {
            myScript.GoToStart();
        }
        if (GUILayout.Button("Go To End")) {
            myScript.GoToEnd();
        }

        if (GUILayout.Button("Randomize Init")) {
            myScript.RandomInitOffset();
        }
    }
}
