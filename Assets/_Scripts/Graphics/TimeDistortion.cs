using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDistortion : MonoBehaviour {
    UnityEngine.Rendering.PostProcessing.PostProcessVolume vol;
    public static float targ = 0;
    // Use this for initialization
    void Start () {
        vol = GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        StartCoroutine("Routine");
    }

    IEnumerator Routine() {
        while (true) {
            vol.weight = Mathf.Lerp(vol.weight, targ, Time.deltaTime);
            yield return null;
        }
    }
}
