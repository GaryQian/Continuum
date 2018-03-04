using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackType { Enemy, Prop, Env }
public class Tracker : MonoBehaviour {
    public TrackType type;
	// Use this for initialization
	void Start () {
        WorldManager.Instance.Register(gameObject, type);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy() {
        WorldManager.Instance.UnRegister(gameObject, type);
    }
}
