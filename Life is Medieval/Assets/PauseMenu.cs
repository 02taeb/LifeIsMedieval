using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenu;

    public GameObject settingsMenu;

    public bool settingsActive = false;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsActive)
                return;

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                
                    Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;


    }


    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void loadSetting()
    {
        settingsActive = true;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void goBack()
    {
        settingsActive = false;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void quitGame()
    {
        Debug.Log("Quiting game........");
    }

    private void changeColour()
    {
        
    }



}
