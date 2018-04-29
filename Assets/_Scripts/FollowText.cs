using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowText : MonoBehaviour {

    TextMesh txt;
	// Use this for initialization
	void Start () {
        txt = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(GameManager.Instance.player.transform);
        float intensity = (Mathf.Sin(Time.time * 10f) + 1f) / 2f;
        txt.color = new Color(1f, intensity, intensity);
	}

    public void SetText(string s) {
        txt.text = s;
    } 
}
