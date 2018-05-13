using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    public static Ending Instance; ///Singleton Instance

    public int state = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}

    public void Begin() {
        NextState();
    }

    public void NextState() {
        state++;
        switch (state) {
            case 1: {
                    GameObject.Find("Pot").GetComponent<RisePlatform>().Begin();
                    break;
                }
            default: {

                    break;
                }
        }
    }
	
	// Update is called once per frame
	void Update () {

		switch(state) {
            case 1: {
                    break;
                }
            default: {

                    break;
                }
        }

	}


}
