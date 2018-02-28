using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

/// <summary>
/// TO SAVE/EDIT SETTINGS:
///     GameManager.Instance.settings.<property> = value
///     SaveManager.SaveSettings();
///     
///     
/// GameManager.Instance.saveVersion is the id for the save format (useful when new updates have differing save formats)
/// 
/// </summary>



/// <summary>
/// Save version. This represents the save data format version
/// </summary>
[Serializable]
public class Version {
    public int version;
    public Version() {
        version = 1;
    }
}

/// <summary>
/// Settings. Global settings like brightness, difficulty, etc.
/// </summary>
[Serializable]
public class SettingsData {
    //public bool setting;
    public bool soundOn;
    public bool musicOn;
    public SettingsData() {
        //setting = false;
    }

}


/// <summary>
/// Singleton. Use to load saved data and flush to disk.
/// </summary>
public class SaveManager : MonoBehaviour {

    public static SaveManager Instance;

    public static readonly string VERSION_PATH = "/version.dat";
    public static readonly string SETTINGS_PATH = "/settings.dat";

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SaveVersion() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + VERSION_PATH);

        bf.Serialize(file, new Version());
        file.Close();
    }

    public int GetVersion() {
        if (File.Exists(Application.persistentDataPath + VERSION_PATH)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + VERSION_PATH, FileMode.Open);
            Version data = null;
            try {
                data = (Version)bf.Deserialize(file);
            }
            catch (Exception e) {
                Debug.LogError("Failed to load Version: " + e.Message);
                return new Version().version;
            }
            file.Close();

            return data.version;
        }
        else {
            SaveVersion();
            return new Version().version;
        }
    }


    /// <summary>
    /// Saves main settings
    /// </summary>
    public void SaveSettings() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + SETTINGS_PATH);

        bf.Serialize(file, GameManager.Instance.settings);
        file.Close();
    }
    /// <summary>
    /// Loads Main settings
    /// </summary>
    /// <returns>Settings. This is cached at GameManager.Instance.settings</returns>
    public SettingsData LoadSettings() {
        if (File.Exists(Application.persistentDataPath + SETTINGS_PATH)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SETTINGS_PATH, FileMode.Open);
            SettingsData data = null;
            try {
                data = (SettingsData)bf.Deserialize(file);
            }
            catch (Exception e) {
                Debug.LogError("Failed to load Settings: " + e.Message);
                data = new SettingsData();
            }
            file.Close();

            return data;
        }
        else {
            SaveVersion();
            return new SettingsData();
        }
    }


}
