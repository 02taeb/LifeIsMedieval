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
        if (currentScene.sc.fGraphic)
        {
            fGraphic.sprite = currentScene.sprite;
            fGraphic.gameObject.GetComponent<Image>().color = Color.white;
        }

        // TODO: Add values for buttons
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
        private string btnText = "";
        private string flavourText = "";
        private Req req = Req.NONE;
        private int reqNum;

        /// <summary>
        /// Checks that a Choice is correctly set up.
        /// <br></br>
        /// Choice is correctly set up if btnText and flavourText have assigned values and an optional requirement is set with necessary reqValue.
        /// </summary>
        /// <returns>true if correct, else false</returns>
        public bool CheckValidity()
        {
            return string.IsNullOrEmpty(btnText) && 
                    string.IsNullOrEmpty(flavourText) && 
                    (req == Req.NONE ? reqNum == 0 : reqNum > 0);
        }

        /// <summary>
        /// Possible requirements for making a choice.
        /// </summary>
        enum Req { STRENGTH, INTELLIGENCE, TRICKERY, NONE }
    }
}
