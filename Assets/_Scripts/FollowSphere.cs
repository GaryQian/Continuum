using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour {

    float xtarg;
    float initx;
    public float juice = 0.3f;
	// Use this for initialization
	void Start () {
        xtarg = transform.position.x;
        initx = xtarg;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.player != null) {
            xtarg = GameManager.Instance.player.transform.position.x + initx;
        }
        transform.position = new Vector3(transform.position.x + Time.deltaTime * juice * (xtarg - transform.position.x), transform.position.y, transform.position.z);
	}
}
