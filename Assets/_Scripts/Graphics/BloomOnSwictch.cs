using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BloomOnSwictch : MonoBehaviour {
    UnityEngine.Rendering.PostProcessing.PostProcessVolume vol;

    float curr;
    public float duration;
    float startTime;
    // Use this for initialization
    void Start () {
        vol = GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        StartCoroutine("Routine");
	}
	
	IEnumerator Routine() {
        startTime = Time.time;
        while (Time.time - startTime < duration) {
            vol.weight = Mathf.Lerp(1.0f, 0, (Time.time - startTime) / duration);
            yield return null;
        }
        vol.weight = 0;
    }
}
