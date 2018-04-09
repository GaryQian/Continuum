using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindTimer : MonoBehaviour {


    public Image LoadingBar;
    public Text Textedit;

    public float MaxSound = 10f;
    private float curSound = 0f;

	// Update is called once per frame
	void Update () {
        curSound = PuppetRegister.timer;
        if (curSound <= PuppetRegister.duration) {
            Textedit.text = "" + ((int)curSound);
        } else {
            Textedit.text = "R";
        }

        LoadingBar.fillAmount = curSound / MaxSound;
	}
}
