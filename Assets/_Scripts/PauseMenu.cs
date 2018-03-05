﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject DeathScreen;
	
	// Update is called once per frame
	void Update () {
        if (DeathScreen.activeSelf == true) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false; 
    }

    public void OptionsButton() {
        Debug.Log("Need to make options menu");
    }

    public void QuitButton() {
        Application.Quit();
    }

    void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Title");
        Resume();
    }


}
