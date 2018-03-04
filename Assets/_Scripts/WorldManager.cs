using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is unique for each level. Have an empty WorldManager gameObject with this script attached on each level.
/// </summary>
public class WorldManager : MonoBehaviour {
    public static WorldManager Instance;

    public Transform[] destinations;

    private void Awake() {
        // Modified singleton pattern. This aggresively takes control of the global instance.
        if (Instance != null) {
            Destroy(Instance.gameObject);
            Destroy(Instance);
        }
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
