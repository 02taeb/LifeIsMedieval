using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeDecision : MonoBehaviour
{
    private GameController gameController;
    private Scene currentScene;
    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        currentScene = GameObject.Find("SceneController").GetComponent<SceneController>().currentScene;
        if (gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + gameObject.name.Substring(gameObject.name.IndexOf(' ') + 1))) 
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
    }
    public void MakeDecisionOnButton()
    {
        if (!gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + gameObject.name.Substring(gameObject.name.IndexOf(' ') + 1)))
        {
            gameController.madeDecisions.Add(currentScene.sceneName.Substring(2) + gameObject.name.Substring(gameObject.name.IndexOf(' ') + 1));
            gameObject.GetComponent<Image>().color = Color.green;
        }
    }
}
