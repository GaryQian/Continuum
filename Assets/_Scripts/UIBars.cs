using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {

    private Slider healthBar;
    private Slider staminaBar;
	// Use this for initialization
	void Start () {
        healthBar = (Slider)GameObject.Find("HealthBar").GetComponent(typeof(Slider));
        staminaBar = (Slider)GameObject.Find("StaminaBar").GetComponent(typeof(Slider));
	}
	
    //sets the bars on the HUD to the desired value
    public void SetHealthBar(float cur, float max) {
        if (healthBar != null) healthBar.value = cur / max;
    }

    public void SetStaminaBar(float cur, float max) {
        if (staminaBar != null) staminaBar.value = cur / max;
    }
}
