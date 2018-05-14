using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteScreen : MonoBehaviour {

    public Image img;
    float startTime;
    public float duration;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        StartCoroutine(Fade());
	}
    
    IEnumerator Fade() {
        while (Time.time - startTime < duration) {

            img.color = new Color(1f, 1f, 1f, Mathf.Lerp(0, 1f, (Time.time - startTime) / duration));
            yield return null;
        }
    }
}
