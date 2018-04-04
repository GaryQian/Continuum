using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

	public float originalFOV;
	public float currentFOV;
	public float zoomInterval = 1f;
	public float lerpTime = 1f;
	private float maxZoomFOV = 50f;

	// Use this for initialization
	void Start () {
		originalFOV = Camera.main.fieldOfView;

	}
	
	// Update is called once per frame
	void Update () {
		currentFOV = Camera.main.fieldOfView;
//		Camera.main.fieldOfView = fieldOfVision;
		if (Camera.main.fieldOfView != originalFOV) {
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, originalFOV, lerpTime * Time.deltaTime); 
		}
	}

	public void DashCameraZoom(){
		StartCoroutine ("DashFOV");
	}

	IEnumerator DashFOV() {
		//Zoomin
		for (float f = originalFOV; f > maxZoomFOV; f -= zoomInterval) {
			Camera.main.fieldOfView = f;
			yield return null;
		}
			
		//Zoomout
//		for (float f = maxZoomFOV; f <60f; f += zoomInterval) {
//			Camera.main.fieldOfView = f;
//			yield return null;
//		}

		//Reset to original fov
//		Camera.main.fieldOfView = originalFOV;
//		yield return null;

	}
}
