using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerGauge : MonoBehaviour {

    private float curTimer;
    float maxTimer;

    public Text text;
    public Image fill;

    private void Start() {
        maxTimer = PuppetRegister.duration;
    }

    // Update is called once per frame
    void Update () {
        curTimer = PuppetRegister.timer;

        if (curTimer <= maxTimer - 0.1f) {
            text.text = "" + (int)curTimer;
            fill.fillAmount = curTimer / maxTimer;
        } else {
            text.text = "R";
            fill.fillAmount = 1;
        }
	}
}
