using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public GameObject lPanel, rPanel, lbPanel, rbPanel;
    public Image fGraphic, lGraphic, rGraphic;
    public Text lText, rText;
    public Button[] buttons;
    public Text[] btnTexts;
    public Text[] btnFlavTexts;
    public Choice[] choices = new Choice[6];
    private Scene currentScene;
    private FileReader fr = new FileReader();
    private GameController gameController;

    private void Update()
    {
        if(!currentScene.sc.lBtns && !currentScene.sc.rBtns)
        {
            currentScene.completed = true;
        }

        if (!currentScene.completed)
        {
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = true;
        }
    }

    private void Awake()
    {
        LoadScene("SC1.1");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        for (int i = 0; i < 6; i++)
        {
            choices[i] = new Choice();
        }
    }

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="sceneFileName">Scene Name on file</param>
    private void LoadScene(string sceneFileName)
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

    /// <summary>
    /// Loads the scene after this one.
    /// </summary>
    public void LoadNextScene()
    {
        if (currentScene.nextScene != null)
        {
            currentScene = currentScene.nextScene;
            LoadScene(currentScene.sceneName);
        }
    }

    /// <summary>
    /// Loads the scene before this one.
    /// </summary>
    public void LoadPrevScene()
    {
        if (currentScene.prevScene != null)
        {
            currentScene = currentScene.prevScene;
            LoadScene(currentScene.sceneName);
        }
    }

    /// <summary>
    /// Toggles all relevant GameObjects in scene according to sceneconfig.
    /// </summary>
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

    /// <summary>
    /// Sets values on relevant GameObjects in scene.
    /// </summary>
    /// <param name="strings">List&lt;string&gt; with values.</param>
    private void SetObjectValues(List<string> strings)
    {
        if (currentScene.sc.lText)
        {
            lText.text = "";
            foreach (string str in fr.ReadFromFile("T" + currentScene.sceneName))
            {
                lText.text += str + "\n";
            }
        }
        else if (currentScene.sc.lGraphic)
        {
            lGraphic.sprite = currentScene.sprite;
            lGraphic.gameObject.GetComponent<Image>().color = Color.white;
        }
        else if (currentScene.sc.lBtns)
        {
            try
            {
                LSetChoices();

                SetButton(0);
                SetButton(1);
                SetButton(2);
            }
            catch (FileNotFoundException)
            {
                Debug.Log("B" + currentScene.sceneName + " not found!");
            }
        }

        if (currentScene.sc.rText)
        {
            rText.text = "";
            foreach (string str in fr.ReadFromFile("T" + currentScene.sceneName))
            {
                rText.text += str + "\n";
            }
        }
        else if (currentScene.sc.rGraphic)
        {
            rGraphic.sprite = currentScene.sprite;
            rGraphic.gameObject.GetComponent<Image>().color = Color.white;
        }
        else if (currentScene.sc.rBtns)
        {
            try
            {
                RSetChoices();

                SetButton(3);
                SetButton(4);
                SetButton(5);
            }
            catch (FileNotFoundException)
            {
                Debug.Log("B" + currentScene.sceneName + " not found!");
            }
        }

        if (currentScene.sc.fGraphic)
        {
            fGraphic.sprite = currentScene.sprite;
            fGraphic.gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    /// <summary>
    /// Sets the values of choices for leftButtons.
    /// </summary>
    private void LSetChoices()
    {
        List<string> btnStrings = new List<string>(fr.ReadFromFile("B" + currentScene.sceneName));

        for (int i = 0; i < 3; i++)
        {
            choices[i].btnText = btnStrings[i * 4];
            choices[i].flavourText = btnStrings[i * 4 + 1];
            choices[i].req = (Choice.Req)Enum.Parse(typeof(Choice.Req), btnStrings[i * 4 + 2]);
            choices[i].reqNum = int.Parse(btnStrings[i * 4 + 3]);

            if (!choices[i].CheckValidity())
                Debug.Log(i + "Button set up incorrectly");
        }
    }

    /// <summary>
    /// Sets the values of choices for rightButtons.
    /// </summary>
    private void RSetChoices()
    {
        List<string> btnStrings = new List<string>(fr.ReadFromFile("B" + currentScene.sceneName));
        int j = 0;
        for (int i = 3; i < 6; i++)
        {
            choices[i].btnText = btnStrings[j * 4];
            choices[i].flavourText = btnStrings[j * 4 + 1];
            choices[i].req = (Choice.Req)Enum.Parse(typeof(Choice.Req), btnStrings[j * 4 + 2]);
            choices[i].reqNum = int.Parse(btnStrings[j * 4 + 3]);

            if (!choices[i].CheckValidity())
                Debug.Log(i + "Button set up incorrectly");

            j++;
        }
    }

    /// <summary>
    /// Sets up a button according to corresponding choices[]
    /// </summary>
    /// <param name="buttonIndex">index of button and choice</param>
    private void SetButton(int buttonIndex)
    {
        btnTexts[buttonIndex].text = choices[buttonIndex].btnText;
        btnFlavTexts[buttonIndex].text = choices[buttonIndex].flavourText;

        switch (choices[buttonIndex].req)
        {
            case Choice.Req.STRENGTH:
                if (choices[buttonIndex].reqNum > gameController.strength)
                {
                    buttons[buttonIndex].interactable = false;
                }
                break;

            case Choice.Req.INTELLIGENCE:
                if (choices[buttonIndex].reqNum > gameController.intellect)
                {
                    buttons[buttonIndex].interactable = false;
                }
                break;

            case Choice.Req.TRICKERY:
                if (choices[buttonIndex].reqNum > gameController.trickery)
                {
                    buttons[buttonIndex].interactable = false;
                }
                break;

            case Choice.Req.NONE:
                break;
        }
    }

    /// <summary>
    /// Parses values from List&lt;string&gt; and enters them into currentScene.SceneConfiguration.
    /// </summary>
    /// <param name="strings">List&lt;string&gt; with values</param>
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

    /// <summary>
    /// Choice data container for button.
    /// </summary>
    public class Choice
    {
        public string btnText = "";
        public string flavourText = "";
        public Req req = Req.NONE;
        public int reqNum;

        /// <summary>
        /// Checks that a Choice is correctly set up.
        /// <br></br>
        /// Choice is correctly set up if btnText and flavourText have assigned values and an optional requirement is set with necessary reqValue.
        /// </summary>
        /// <returns>true if correct, else false</returns>
        public bool CheckValidity()
        {
            return !string.IsNullOrEmpty(btnText) && 
                    !string.IsNullOrEmpty(flavourText) && 
                    (req == Req.NONE ? reqNum == 0 : reqNum > 0);
        }

        /// <summary>
        /// Possible requirements for making a choice.
        /// </summary>
        public enum Req { STRENGTH, INTELLIGENCE, TRICKERY, NONE }
    }
}
