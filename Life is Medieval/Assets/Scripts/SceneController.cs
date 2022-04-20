using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject lPanel, rPanel, lbPanel, rbPanel;
    public Image fGraphic, lGraphic, rGraphic;
    public Text lText, rText;
    public GameObject[] buttons;
    public Choice[] choices = new Choice[3];
    private Scene currentScene;
    private FileReader fr = new FileReader();

    private void Awake()
    {
        LoadScene("SC1.1");
    }

    public void LoadScene(string sceneFileName)
    {
        currentScene = ((GameObject)Resources.Load("StoryScenes/" + sceneFileName)).GetComponent<Scene>();

        List<string> strings = fr.ReadFromFile(sceneFileName);
        ParseBools(strings);
        if (!currentScene.sc.CheckSetup())
        {
            Debug.Log("Scene Configuration invalid");
        }

        ToggleObjects();
        SetObjectValues(strings);
    }

    public void LoadNextScene()
    {
        if (currentScene.nextScene != null)
        {
            currentScene = currentScene.nextScene;
            LoadScene(currentScene.sceneName);
        }
    }

    public void LoadPrevScene()
    {
        if (currentScene.prevScene != null)
        {
            currentScene = currentScene.prevScene;
            LoadScene(currentScene.sceneName);
        }
    }

    private void ToggleObjects()
    {
        if (!currentScene.sc.lBtns)
        {
            lbPanel.SetActive(false);
        }
        else
        {
            lbPanel.SetActive(true);
        }
        if (!currentScene.sc.rBtns)
        {
            rbPanel.SetActive(false);
        }
        else
        {
            rbPanel.SetActive(true);
        }
        if (!currentScene.sc.lText)
        {
            lText.gameObject.SetActive(false);
        }
        else
        {
            lText.gameObject.SetActive(true);
        }
        if (!currentScene.sc.rText)
        {
            rText.gameObject.SetActive(false);
        }
        else
        {
            rText.gameObject.SetActive(true);
        }
        if (!currentScene.sc.lGraphic)
        {
            lGraphic.gameObject.SetActive(false);
        }
        else
        {
            lGraphic.gameObject.SetActive(true);
        }
        if (!currentScene.sc.rGraphic)
        {
            rGraphic.gameObject.SetActive(false);
        }
        else
        {
            rGraphic.gameObject.SetActive(true);
        }
        if (!currentScene.sc.fGraphic)
        {
            fGraphic.gameObject.SetActive(false);
        }
        else
        {
            fGraphic.gameObject.SetActive(true);
        }
    }

    private void SetObjectValues(List<string> strings)
    {
        if (currentScene.sc.lText)
        {
            lText.text = strings[9];
        }
        else if (currentScene.sc.lGraphic)
        {
            lGraphic.sprite = currentScene.sprite;
        }
        if (currentScene.sc.rText)
        {
            rText.text = strings[9];
        }
        else if (currentScene.sc.rGraphic)
        {
            rGraphic.sprite = currentScene.sprite;
        }
        if (currentScene.sc.fGraphic)
        {
            fGraphic.sprite = currentScene.sprite;
        }
    }

    private void ParseBools(List<string> strings)
    {
        currentScene.sc.lBtns = bool.Parse(strings[0]);
        currentScene.sc.rBtns = bool.Parse(strings[1]);
        currentScene.sc.lText = bool.Parse(strings[2]);
        currentScene.sc.rText = bool.Parse(strings[3]);
        currentScene.sc.lGraphic = bool.Parse(strings[4]);
        currentScene.sc.rGraphic = bool.Parse(strings[5]);
        currentScene.sc.lPanel = bool.Parse(strings[6]);
        currentScene.sc.rPanel = bool.Parse(strings[7]);
        currentScene.sc.fGraphic = bool.Parse(strings[8]);
    }

    public class Choice
    {
        private string btnText = "";
        private string flavourText = "";
        private Req req = Req.NONE;
        private int reqNum;

        public bool CheckValidity()
        {
            return string.IsNullOrEmpty(btnText) && 
                    string.IsNullOrEmpty(flavourText) && 
                    (req == Req.NONE ? reqNum == 0 : reqNum > 0);
        }

        enum Req { STRENGTH, INTELLIGENCE, TRICKERY, NONE }
    }
}
