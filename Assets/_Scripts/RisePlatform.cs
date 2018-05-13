using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class RisePlatform : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;
    Rigidbody body;
    bool moved = false;
    public float moveSpeed = 1f;
    //public Collider triggerCollider;

	// Use this for initialization
	void Start () {
        //transform.position = startPos;
        body = GetComponent<Rigidbody>();
        body.MovePosition(startPos);
        //if (triggerCollider == null) {
        //    Collider[] col = this.GetComponents<Collider>();
        //    if (col.Length == 2) {
        //        triggerCollider = col[1];
        //    }
        //    else {
        //        triggerCollider = null;
        //    }
        //}
	}

    IEnumerator Move() {
        while (Vector3.SqrMagnitude(transform.position - endPos) > 0.08f) {
            body.MovePosition(Vector3.Lerp(transform.position, endPos, Time.fixedDeltaTime * 4f * moveSpeed));
            yield return new WaitForFixedUpdate();
        }

    }


    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.layer == 10) {
            //triggerCollider.enabled = false;
            Begin();
        }
    }

    public void Begin() {
        //triggerCollider.enabled = false;
        StartCoroutine(Move());
        moved = true;
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
}


#if UNITY_EDITOR
[CustomEditor(typeof(RisePlatform))]
public class RisePlatformEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        RisePlatform myScript = (RisePlatform)target;
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
    }
}
#endif