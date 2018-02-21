using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamagedFlashRed : MonoBehaviour {

	public Image redPanel;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FlashRedOnDamage() {
		redPanel.enabled = true;
		Invoke ("CancelRedFlash", 0.5f);
	}

	void CancelRedFlash() {
		redPanel.enabled = false;
	}
}
