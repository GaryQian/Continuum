using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitle : MonoBehaviour {

    public  Text subtitleHolder;

    private Queue timer;
    private bool hasInput = false;
    private Queue text;
    private string curText;
    private float curTimer = 0;

	// Use this for initialization
	private void Start()
	{
        text = new Queue();
        timer = new Queue();
	}
	// Update is called once per frame
	void FixedUpdate () {
        if (hasInput) {
            
            curTimer -= Time.deltaTime;
            if (curTimer <= 0 && (text.Count != 0)) {
                curText = (string) text.Dequeue();
                curTimer = (float) timer.Dequeue();
                subtitleHolder.gameObject.SetActive(true);
            } else if (curTimer <= 0 && (text.Count <= 0)) {
                subtitleHolder.gameObject.SetActive(false);
                hasInput = false;
                subtitleHolder.text = "";
                return;
            }

            subtitleHolder.text = curText;
        }
	}

    public void setInput(string input, float time) {
        timer.Enqueue(time);
        text.Enqueue(input);
        hasInput = true;
    }

    public void stopText() {
        text.Clear();
        timer.Clear();
        curTimer = 0;
    }
}
