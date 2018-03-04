using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{

    // Use this for initialization
    public GameObject deathUI;
    public Camera deathCamera;

    void Start()
    {
        GetComponent<Health>().OnDie += TriggerMenu;
    }



    public void TriggerMenu()
    {
        deathUI.SetActive(true);
        Vector3 vec = new Vector3(0, 24, 0);
        Quaternion rotation = new Quaternion((float)64.80, 0, 0, 0);
        Camera cam = Instantiate(deathCamera, vec, rotation);
        cam.enabled = true;
        Time.timeScale = 0f;
    }


    public void RestartButton() {
        SceneManager.LoadScene("Title");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

