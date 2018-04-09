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
        if (curSound <= MaxSound) {
            Textedit.text = curSound.ToString() + "%";
        } else {
            Textedit.text = "Ready!";
        }

        LoadingBar.fillAmount = curSound / MaxSound;
	}
}
