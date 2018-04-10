using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGauge : MonoBehaviour {

    private float curTimer;
    public float maxTimer = 10f;

    public Text text;
    public Image fill;

	// Update is called once per frame
	void Update () {
        curTimer = PuppetRegister.timer;

        if (curTimer <= maxTimer) {
            text.text = (int)curTimer + "%";
            fill.fillAmount = curTimer / maxTimer;
        } else {
            text.text = "Ready!";
            fill.fillAmount = 1;
        }
	}
}
