using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is unique for each level. Have an empty WorldManager gameObject with this script attached on each level.
/// </summary>
public class WorldManager : MonoBehaviour {
    public static WorldManager Instance;

    public Transform[] destinations;
    public Transform[] spawnpoints;

    public GameObject enemyPrefab;

    public List<GameObject> enemies;
    public List<GameObject> props;
    public List<GameObject> env;

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
        enemies = new List<GameObject>();
        props = new List<GameObject>();
        env = new List<GameObject>();

        Debug.Log("WorldManager Started");
        InvokeRepeating("SpawnEnemy", 5f, 5f);
    }

    public void Register(GameObject obj, TrackType type) {
        switch (type) {
            case TrackType.Enemy: enemies.Add(obj); break;
            case TrackType.Prop: props.Add(obj); break;
            case TrackType.Env: env.Add(obj); break;
        }
    }
    public void UnRegister(GameObject obj, TrackType type) {
        switch (type) {
            case TrackType.Enemy: enemies.Remove(obj); break;
            case TrackType.Prop: props.Remove(obj); break;
            case TrackType.Env: env.Remove(obj); break;
        }
    }

    public void SpawnEnemy() {
        Debug.Log("Spawning Enemy");
        if (enemyPrefab != null) Instantiate(enemyPrefab, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
