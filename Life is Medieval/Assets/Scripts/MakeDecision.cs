using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeDecision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip success, fail;
    private GameController gameController;
    private Scene currentScene;
    private Choice[] choices;
    private Image btnImage;
    private string buttonIndexString;
    private int buttonIndex;
    private List<string> madeDecisions;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        madeDecisions = gameController.madeDecisions;
        currentScene = GameObject.Find("SceneController").GetComponent<SceneController>().currentScene;
        buttonIndexString = gameObject.name.Substring(gameObject.name.IndexOf(' ') + 1);
        buttonIndex = int.Parse(buttonIndexString);
        btnImage = gameObject.GetComponent<Image>();

        if (madeDecisions.Contains(currentScene.sceneName.Substring(2) + buttonIndexString + "S")) 
        {
            btnImage.color = Color.green;
        }
        else if (madeDecisions.Contains(currentScene.sceneName.Substring(2) + buttonIndexString + "F"))
        {
            btnImage.color = Color.red;
        }
        else
        {
            choices = GameObject.Find("SceneController").GetComponent<SceneController>().GetChoices();
            switch (choices[buttonIndex].req)
            {
                case Choice.Req.STRENGTH:
                    btnImage.color = new Color32(250, 120, 0, 255);
                    break;

                case Choice.Req.INTELLIGENCE:
                    btnImage.color = new Color32(0, 235, 255, 255);
                    break;

                case Choice.Req.TRICKERY:
                    btnImage.color = new Color32(0, 255, 46, 255);
                    break;

                case Choice.Req.NONE:
                default:
                    btnImage.color = Color.white;
                    break;
            }
        }
    }

    public void MakeDecisionOnButton()
    {
        if (!madeDecisions.Contains(currentScene.sceneName.Substring(2) + gameObject.name.Substring(gameObject.name.IndexOf(' ') + 1)))
        {
            choices = GameObject.Find("SceneController").GetComponent<SceneController>().GetChoices();
            switch (choices[buttonIndex].req)
            {
                case Choice.Req.STRENGTH:
                    if (Random.Range(gameController.strength * 10, (choices[buttonIndex].reqNum + gameController.strength) * 10 + 1) >= choices[buttonIndex].reqNum * 10 + 1)
                    {
                        SucceedChoice();
                    }
                    else
                    {
                        FailChoice();
                    }
                    break;
                case Choice.Req.INTELLIGENCE:
                    if (Random.Range(gameController.intelligence * 10, (choices[buttonIndex].reqNum + gameController.intelligence) * 10 + 1) >= choices[buttonIndex].reqNum * 10 + 1)
                    {
                        SucceedChoice();
                    }
                    else
                    {
                        FailChoice();
                    }
                    break;
                case Choice.Req.TRICKERY:
                    if (Random.Range(gameController.trickery * 10, (choices[buttonIndex].reqNum + gameController.trickery) * 10 + 1) >= choices[buttonIndex].reqNum * 10 + 1)
                    {
                        SucceedChoice();
                    }
                    else
                    {
                        FailChoice();
                    }
                    break;
                case Choice.Req.NONE:
                    SucceedChoice();
                    break;
            }
            
        }
    }

    private void SucceedChoice()
    {
        madeDecisions.Add(currentScene.sceneName.Substring(2) + name.Substring(name.IndexOf(' ') + 1) + "S");
        btnImage.color = Color.green;
        audioSource.PlayOneShot(success);
    }

    private void FailChoice()
    {
        madeDecisions.Add(currentScene.sceneName.Substring(2) + name.Substring(name.IndexOf(' ') + 1) + "F");
        btnImage.color = Color.red;
        audioSource.PlayOneShot(fail);
    }
}
