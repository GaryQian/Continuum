using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBars : MonoBehaviour {

    private Image healthBar;
    private Image staminaBar;
	// Use this for initialization
	void Start () {
        healthBar = (Image)GameObject.Find("HealthBar2").GetComponent(typeof(Image));
        staminaBar = (Image)GameObject.Find("StaminaBar2").GetComponent(typeof(Image));
	}
	
    //sets the bars on the HUD to the desired value
    public void SetHealthBar(float cur, float max) {
        if (healthBar != null) healthBar.fillAmount = cur / max;
    }

    public void SetStaminaBar(float cur, float max) {
        if (staminaBar != null) staminaBar.fillAmount = cur / max;
    }
}
