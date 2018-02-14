using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    //public static int saveVersion;
    //public SettingsData settings;
//	public GameObject FPSController;
	public GameObject player;
	public Transform playerLocation;
	public float px, py, pz;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
		playerLocation = player.transform;
    }

    // Use this for initialization
    void Start () {
        //saveVersion = SaveManager.Instance.GetVersion();
        //SaveManager.Instance.SaveVersion();

        //settings = SaveManager.Instance.LoadSettings();
        
	}
	
	// Update is called once per frame
	void Update () {
		playerLocation = player.transform;
	}

	public Transform getPlayerLocation() {
		return playerLocation;
	}
}
