using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Singleton. Access with GameManager.Instance
/// Handles overall game flow such as starting game, and delegating to local managers.
/// </summary>
public class GameManager : MonoBehaviour {

	public static GameManager Instance; ///Singleton Instance
        //GameManager.Instance

	public static int saveVersion;
	public SettingsData settings;

	public GameObject player;

	/// <summary>
	/// EARLY setup that is done before the start function.
	/// </summary>
	private void Awake() {
		if (Instance != null) {
			Destroy(gameObject);
			return;
		}
		Instance = this;
	}

	/// <summary>
	/// Load everything that needs to be done at game load.
	/// </summary>
	void Start () {
		saveVersion = SaveManager.Instance.GetVersion();
		SaveManager.Instance.SaveVersion();
		settings = SaveManager.Instance.LoadSettings();
	}


	/// <summary>
	/// Global update. Run on every frame. Try to put things in more local update loops, but some things like global counters can go here.
	/// </summary>
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1)) SceneManager.LoadScene("Title");
        if (Input.GetKeyDown(KeyCode.Alpha2)) SceneManager.LoadScene("Tutorial");
        if (Input.GetKeyDown(KeyCode.Alpha3)) SceneManager.LoadScene("blue");
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene("Green");
        if (Input.GetKeyDown(KeyCode.Alpha5)) SceneManager.LoadScene("Red");
    }

}