using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

[Serializable]
public class Version {
    public int version;
    public Version() {
        version = 1;
    }
}

[Serializable]
public class SettingsData {
    //public bool setting;

    public SettingsData() {
        //setting = false;
    }

}
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

    public void SaveSettings() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + SETTINGS_PATH);

        bf.Serialize(file, new Version());
        file.Close();
    }

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
