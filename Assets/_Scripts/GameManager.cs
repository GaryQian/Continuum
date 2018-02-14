using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Singleton. Access with GameManager.Instance
/// Handles overall game flow such as starting game, and delegating to local managers.
/// </summary>
public class GameManager : MonoBehaviour {
	public static GameManager Instance; ///Singleton Instance

	public static int saveVersion;
	public SettingsData settings;

	public GameObject player;
	public Transform playerLocation;
	public float px, py, pz;

	/// <summary>
	/// EARLY setup that is done before the start function.
	/// </summary>
	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
		playerLocation = player.transform;
	}

	/// <summary>
	/// Load everything that needs to be done at game load.
	/// </summary>
	void Start () {
		saveVersion = SaveManager.Instance.GetVersion ();
		SaveManager.Instance.SaveVersion ();
	}
	/// <summary>
	/// Global update. Run on every frame. Try to put things in more local update loops, but some things like global counters can go here.
	/// </summary>
	void Update () {
		playerLocation = player.transform;
	}

	public Transform getPlayerLocation() {
		return playerLocation;
	}
}