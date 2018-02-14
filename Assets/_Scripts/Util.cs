using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Static utility class with Instance references to key Gameobjects in the scene
/// 
/// Most methods and members here should be marked public static, but if nessecary instance member variables can be used too.
/// </summary>
public class Util : MonoBehaviour {

    public static Util Instance;

    public static GameObject canvas;
    public static float fontScale; //Same as avgScale (for now). Use to scale fonts that are not using 'bestfit'
    public static float avgScale; //Average of xScale and yScale

    public static float xScale; //screen x scale relative to a 1920x1080 base resolution
    public static float yScale; //screen y scale relative to a 1920x1080 base resolution

    public static float aspectRatio; // screen width divided by height

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Calculate and initialize global statistics that are universally useful.
    /// </summary>
    void Start() {
        aspectRatio = (float)Screen.width / Screen.height;
        xScale = Screen.width / 1920f;
        yScale = Screen.height / 1080f;
        fontScale = (xScale + yScale) / 2f;
        avgScale = fontScale;

        canvas = GameObject.Find("Canvas");
    }

}
