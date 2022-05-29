using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public GameObject lPanel, rPanel, lbPanel, rbPanel, restartBtn;
    public Image fGraphic, lGraphic, rGraphic;
    public Text lText, rText;
    public Button[] buttons;
    public Text[] btnTexts;
    public Text[] btnFlavTexts;
    public Choice[] choices = new Choice[6];
    public Scene currentScene;
    public float writingSpeed = 50f, fadeSpeed = 50f;
    private FileReader fr = new FileReader();
    private GameController gameController;

    private void Update()
    {
        if (currentScene.prevScene == null)
            GameObject.Find("BtnPrevStory").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("BtnPrevStory").GetComponent<Button>().interactable = true;

        if (currentScene.nextScene == null)
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = true;

        currentScene.completed = false;

        if (!currentScene.sc.lBtns && !currentScene.sc.rBtns)
            currentScene.completed = true;
        if (gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "0S")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "1S")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "2S")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "3S")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "4S")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "5S"))
            currentScene.completed = true;
        if (gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "0F")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "1F")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "2F")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "3F")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "4F")
            || gameController.madeDecisions.Contains(currentScene.sceneName.Substring(2) + "5F"))
            currentScene.completed = true;

        if (!currentScene.completed)
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = false;
        else
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = true;

        if (currentScene.nextScene == null)
        {
            GameObject.Find("BtnNextStory").GetComponent<Button>().interactable = false;
            restartBtn.SetActive(true);
        }
        else 
            restartBtn.SetActive(false);

        if (currentScene.completed)
        {
            foreach (Button btn in buttons)
            {
                btn.interactable = false;
            }
        }
    }

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        for (int i = 0; i < 6; i++)
        {
            choices[i] = new Choice();
        }
        LoadScene(gameController.currentScene);
    }

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="sceneFileName">Scene Name on file</param>
    private void LoadScene(string sceneFileName)
    {
        StopAllCoroutines();
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
        string currentDirectory = Environment.CurrentDirectory;
        try
        {
            List<string> aftermath = fr.ReadFromFile("A" + currentScene.sceneName);
            char[] sceneNameArr = currentScene.sceneName.Substring(2).ToCharArray();
            sceneNameArr[2] = (int.Parse(sceneNameArr[2].ToString()) - 1).ToString().ToCharArray()[0];
            string sceneName = new string(sceneNameArr);
            if (gameController.madeDecisions.Contains(sceneName + "0S")
                    || gameController.madeDecisions.Contains(sceneName + "3S")
                    || (gameController.madeDecisions.Contains("1.90S") && sceneName.Equals("2.-"))
                    || (gameController.madeDecisions.Contains("1.93S") && sceneName.Equals("2.-"))
                    || (gameController.madeDecisions.Contains("3.90S") && sceneName.Equals("4.-"))
                    || (gameController.madeDecisions.Contains("3.93S") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[2], lText));
                if (!bool.Parse(aftermath[1]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[0].Substring(2, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[0].Substring(3, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    
                    int diff = int.Parse(aftermath[0].Substring(0, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[0].Substring(0, 3));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }
            else if (gameController.madeDecisions.Contains(sceneName + "1S")
                || gameController.madeDecisions.Contains(sceneName + "4S")
                || (gameController.madeDecisions.Contains("1.91S") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("1.94S") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("3.91S") && sceneName.Equals("4.-"))
                || (gameController.madeDecisions.Contains("3.94S") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[6], lText));
                if (!bool.Parse(aftermath[5]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[4].Substring(2, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[4].Substring(3, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    int diff = int.Parse(aftermath[4].Substring(0, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[4].Substring(0, 3));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }
            else if (gameController.madeDecisions.Contains(sceneName + "2S")
                || gameController.madeDecisions.Contains(sceneName + "5S")
                || (gameController.madeDecisions.Contains("1.92S") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("1.95S") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("3.92S") && sceneName.Equals("4.-"))
                || (gameController.madeDecisions.Contains("3.95S") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[10], lText));
                if (!bool.Parse(aftermath[9]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[8].Substring(2, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[8].Substring(3, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    int diff = int.Parse(aftermath[8].Substring(0, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[8].Substring(0, 3));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }
            else if (gameController.madeDecisions.Contains(sceneName + "0F")
                || gameController.madeDecisions.Contains(sceneName + "3F")
                || (gameController.madeDecisions.Contains("1.90F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("1.93F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("3.90F") && sceneName.Equals("4.-"))
                || (gameController.madeDecisions.Contains("3.93F") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[3], lText));
                if (!bool.Parse(aftermath[1]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[0].Substring(6, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[0].Substring(7, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    int diff = int.Parse(aftermath[0].Substring(4, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[0].Substring(5, 2));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[1] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }
            else if (gameController.madeDecisions.Contains(sceneName + "1F")
                || gameController.madeDecisions.Contains(sceneName + "4F")
                || (gameController.madeDecisions.Contains("1.91F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("1.94F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("3.91F") && sceneName.Equals("4.-"))
                || (gameController.madeDecisions.Contains("3.94F") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[7], lText));
                if (!bool.Parse(aftermath[5]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[4].Substring(6, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[4].Substring(7, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    int diff = int.Parse(aftermath[4].Substring(4, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[4].Substring(5, 2));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[5] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }
            else if (gameController.madeDecisions.Contains(sceneName + "2F")
                || gameController.madeDecisions.Contains(sceneName + "5F")
                || (gameController.madeDecisions.Contains("1.92F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("1.95F") && sceneName.Equals("2.-"))
                || (gameController.madeDecisions.Contains("3.92F") && sceneName.Equals("4.-"))
                || (gameController.madeDecisions.Contains("3.95F") && sceneName.Equals("4.-")))
            {
                StartCoroutine(Write(aftermath[11], lText));
                if (!bool.Parse(aftermath[9]))
                {
                    string stat;
                    try
                    {
                        stat = aftermath[8].Substring(6, 1);
                        if (currentScene.sceneName.Equals("SC4.0"))
                            stat = aftermath[8].Substring(7, 1);
                    }
                    catch (Exception)
                    {
                        stat = "A";
                    }
                    int diff = int.Parse(aftermath[8].Substring(4, 2));
                    if (currentScene.sceneName.Equals("SC4.0"))
                        diff = int.Parse(aftermath[8].Substring(5, 2));
                    switch (stat)
                    {
                        case "S":
                            gameController.strength += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "I":
                            gameController.intelligence += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "T":
                            gameController.trickery += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "L":
                            gameController.lives += diff;
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        case "A":
                            aftermath[9] = "true";
                            fr.WriteToFile("A" + currentScene.sceneName, aftermath);
                            break;
                        default:
                            Debug.Log("Invalid aftermath file");
                            break;
                    }
                }
            }

            rGraphic.sprite = currentScene.sprite;
            StartCoroutine(FadeImage(rGraphic));
        }
        catch (Exception)
        {
            Environment.CurrentDirectory = currentDirectory;
            if (currentScene.sc.lText && !currentScene.sc.rText)
            {
                lText.text = "";
                List<string> strs = new List<string>();
                foreach (string str in fr.ReadFromFile("T" + currentScene.sceneName))
                {
                    strs.Add(str + "\n");
                }
                string toWrite = "";
                foreach (string str in strs)
                {
                    toWrite += str;
                }
                StartCoroutine(Write(toWrite, lText));
            }
            else if (currentScene.sc.lGraphic)
            {
                lGraphic.sprite = currentScene.sprite;
                StartCoroutine(FadeImage(lGraphic.gameObject.GetComponent<Image>()));
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

            if (currentScene.sc.rText && !currentScene.sc.lText)
            {
                rText.text = "";
                List<string> strs = new List<string>();
                foreach (string str in fr.ReadFromFile("T" + currentScene.sceneName))
                {
                    strs.Add(str + "\n");
                }
                string toWrite = "";
                foreach (string str in strs)
                {
                    toWrite += str;
                }
                StartCoroutine(Write(toWrite, rText));
            }
            else if (currentScene.sc.rGraphic)
            {
                rGraphic.sprite = currentScene.sprite;
                StartCoroutine(FadeImage(rGraphic.gameObject.GetComponent<Image>()));
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
                StartCoroutine(FadeImage(fGraphic.gameObject.GetComponent<Image>()));
            }

            if (currentScene.sc.lText && currentScene.sc.rText && currentScene.nextScene != null)
            {
                lText.text = "";
                List<string> strs = new List<string>();
                foreach (string str in fr.ReadFromFile("T" + currentScene.sceneName))
                {
                    strs.Add(str + "\n");
                }
                string toWrite = "";
                foreach (string str in strs)
                {
                    toWrite += str;
                }
                StartCoroutine(Write(toWrite, lText));

                rText.text = "";
                List<string> strs2 = new List<string>();
                foreach (string str in fr.ReadFromFile("TT" + currentScene.sceneName))
                {
                    strs2.Add(str + "\n");
                }
                string toWrite2 = "";
                foreach (string str in strs2)
                {
                    toWrite2 += str;
                }
                StartCoroutine(Write(toWrite2, rText));
            }
            else if (currentScene.sc.lText && currentScene.sc.rText)
            {
                List<string> lEndings = fr.ReadFromFile("T" + currentScene.sceneName + "E");
                List<string> rEndings = fr.ReadFromFile("TT" + currentScene.sceneName + "E");
                int max = Mathf.Max(gameController.strength, gameController.intelligence, gameController.trickery);
                lText.text = "";
                rText.text = "";
                if (gameController.strength == max)
                {
                    StartCoroutine(Write(lEndings[0], lText));
                    StartCoroutine(Write(rEndings[0], rText));
                }
                else if (gameController.intelligence == max)
                {
                    StartCoroutine(Write(lEndings[1], lText));
                    StartCoroutine(Write(rEndings[1], rText));
                }
                else
                {
                    StartCoroutine(Write(lEndings[2], lText));
                    StartCoroutine(Write(rEndings[2], rText));
                }
            }
        }
    }

    private IEnumerator FadeImage(Image img)
    {
        float t = 0;
        int alpha = 0;
        img.color = new Color32(255, 255, 255, 0);
        if (currentScene.sprite != null)
        {
            while (img.color.a < 1f)
            {
                t += Time.deltaTime * fadeSpeed;
                alpha = Mathf.FloorToInt(t);
                alpha = Mathf.Clamp(alpha, 0, 255);

                img.color = new Color32(255, 255, 255, (byte)alpha);

                yield return null;
            }

            img.color = Color.white;
        }
    }

    private IEnumerator Write(string textToWrite, Text label)
    {
        float t = 0;
        int charIndex = 0;
        label.text = String.Empty;

        while (charIndex < textToWrite.Length)
        {
            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToWrite.Length);

            label.text = textToWrite.Substring(0, charIndex);

            yield return null;
        }

        label.text = textToWrite;
    }

    private IEnumerator Write(string textToWrite, TextMeshPro label)
    {
        float t = 0;
        int charIndex = 0;
        label.text = String.Empty;

        while (charIndex < textToWrite.Length)
        {
            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToWrite.Length);

            label.text = textToWrite.Substring(0, charIndex);

            yield return null;
        }

        label.text = textToWrite;
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
        // btnFlavTexts[buttonIndex].text = choices[buttonIndex].flavourText;
        StartCoroutine(Write(choices[buttonIndex].flavourText, btnFlavTexts[buttonIndex]));
        buttons[buttonIndex].interactable = true;
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
    /// Gets clone of local Choice[].
    /// </summary>
    /// <returns>choices.Clone();</returns>
    public Choice[] GetChoices()
    {
        return (Choice[])choices.Clone();
    }
}
