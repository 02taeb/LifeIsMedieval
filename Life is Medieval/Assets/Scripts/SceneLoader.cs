using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{ 
    public void loadScene ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.Find("GameController").GetComponent<GameController>().SaveGame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex == 0 ? 1 : 0);



    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.Find("GameController").GetComponent<GameController>().LoadGame();
        }
    }

    public void quitGame ()
    {
        Application.Quit();
    }
}
