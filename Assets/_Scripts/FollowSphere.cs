using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour {

    float xtarg;
    float initx;
    public float juice = 0.3f;
    public Light point;
    public Color hitColor;
    public float hitIntensity;
    float initIntensity;
    float intensity;
    Color lightColor;
    Material initMaterial;
    public Material redMaterial;
    public AudioClip[] screams;

	// Use this for initialization
	void Start () {
        xtarg = transform.position.x;
        initx = xtarg;
        initIntensity = point.intensity;
        intensity = initIntensity;
        lightColor = Color.white;
        initMaterial = GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.player != null) {
            xtarg = GameManager.Instance.player.transform.position.x + initx;
        }
        transform.position = new Vector3(transform.position.x + Time.deltaTime * juice * (xtarg - transform.position.x), transform.position.y, transform.position.z);
        lightColor = Color.Lerp(lightColor, Color.white, Time.deltaTime * 2f);
        intensity = Mathf.Lerp(intensity, initIntensity, Time.deltaTime * 2f);
        point.intensity = intensity;
        point.color = lightColor;
        //GetComponent<MeshRenderer>().material.color = lightColor;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 15) {
            if (other.gameObject.GetComponent<BulletMovement>().player) {
                lightColor = hitColor;
                intensity = hitIntensity;
                GetComponent<MeshRenderer>().material = redMaterial;
                Invoke("ResetMat", 0.75f);
                GetComponent<AudioSource>().PlayOneShot(screams[Random.Range(0, screams.Length)]);
            }
        }
    }

    void ResetMat() {
        GetComponent<MeshRenderer>().material = initMaterial;
    }
}
