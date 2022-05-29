using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float volume = 1.0f;
    public float musicVolume = 1.0f;
    public Slider masterSlider, musicSlider;
    public GameObject deathMessage;
    private static float staticVolume = 1.0f;
    private static float staticMusicVolume = 1.0f;
    [NonSerialized]
    public int strength = 0;
    [NonSerialized]
    public int intelligence = 0;
    [NonSerialized]
    public int trickery = 0;
    [NonSerialized]
    public int lives = 3;
    public string currentScene = "SC1.1";
    [NonSerialized]
    public List<string> madeDecisions = new List<string>();

    /// <summary>
    /// Debug.Log stats, currentScene and madeDecisions.
    /// </summary>
    public void DebugLogValues()
    {
        Debug.Log(strength.ToString());
        Debug.Log(intelligence.ToString());
        Debug.Log(trickery.ToString());
        Debug.Log(lives.ToString());
        Debug.Log(currentScene.ToString());
        foreach (string decision in madeDecisions)
        {
            Debug.Log(decision);
        }
    }

    /// <summary>
    /// Creates necessary files on startup.
    /// </summary>
    private void CreateFiles()
    {
        Debug.Log("Creating Files");
        FileReader fr = new FileReader();
        List<string> files = new List<string>();
        string currentDirectory = Environment.CurrentDirectory;

        files.Add("ASC1.5");
        files.Add("ASC1.8");
        files.Add("ASC2.0");
        files.Add("ASC2.2");
        files.Add("ASC2.4");
        files.Add("ASC2.7");
        files.Add("ASC3.2");
        files.Add("ASC3.6");
        files.Add("ASC4.0");
        files.Add("BSC1.4");
        files.Add("BSC1.7");
        files.Add("BSC1.9");
        files.Add("BSC2.1");
        files.Add("BSC2.3");
        files.Add("BSC2.6");
        files.Add("BSC3.1");
        files.Add("BSC3.5");
        files.Add("BSC3.9");
        files.Add("SC1.1");
        files.Add("SC1.2");
        files.Add("SC1.3");
        files.Add("SC1.4");
        files.Add("SC1.5");
        files.Add("SC1.6");
        files.Add("SC1.7");
        files.Add("SC1.8");
        files.Add("SC1.9");
        files.Add("SC2.0");
        files.Add("SC2.1");
        files.Add("SC2.2");
        files.Add("SC2.3");
        files.Add("SC2.4");
        files.Add("SC2.5");
        files.Add("SC2.6");
        files.Add("SC2.7");
        files.Add("SC2.8");
        files.Add("SC2.9");
        files.Add("SC3.1");
        files.Add("SC3.2");
        files.Add("SC3.3");
        files.Add("SC3.4");
        files.Add("SC3.5");
        files.Add("SC3.6");
        files.Add("SC3.7");
        files.Add("SC3.8");
        files.Add("SC3.9");
        files.Add("SC4.0");
        files.Add("SC4.1");
        files.Add("TSC1.1");
        files.Add("TSC1.2");
        files.Add("TSC1.3");
        files.Add("TSC1.4");
        files.Add("TSC1.6");
        files.Add("TSC1.7");
        files.Add("TSC1.9");
        files.Add("TSC2.1");
        files.Add("TSC2.3");
        files.Add("TSC2.5");
        files.Add("TSC2.6");
        files.Add("TSC2.8");
        files.Add("TSC2.9");
        files.Add("TSC3.1");
        files.Add("TSC3.3");
        files.Add("TSC3.4");
        files.Add("TSC3.5");
        files.Add("TSC3.7");
        files.Add("TSC3.8");
        files.Add("TSC3.9");
        files.Add("TSC4.1E");
        files.Add("TTSC3.7");
        files.Add("TTSC4.1E");

        foreach (string file in files)
        {
            fr.WriteToFile(file, TextAssetToList((TextAsset)Resources.Load("TextsToCreate/" + file)));
            Environment.CurrentDirectory = currentDirectory;
        }

        PlayerPrefs.SetString("FilesMade", "True");
    }

    /// <summary>
    /// Converts all lines of text in a textasset to a List&lt;string&gt;
    /// </summary>
    /// <param name="textAsset"></param>
    /// <returns></returns>
    private List<string> TextAssetToList(TextAsset textAsset)
    {
        List<string> text = new List<string>();
        using (StringReader sr = new StringReader(textAsset.text))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                text.Add(line);
            }
        }
        return text;
    }

    private void Update()
    {
        SetVolume();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentScene = GameObject.Find("SceneController").GetComponent<SceneController>().currentScene.sceneName;
            MusicVolume();
            MasterVolume();
        }
        
        if (lives <= 0 && PlayerPrefs.GetString("Dead") == "false")
        {
            PlayerPrefs.SetString("Dead", "true");
            SceneManager.LoadScene(0);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            LoadGame();
        }
        else
        {
            SaveGame();
        }
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Dead"))
        {
            PlayerPrefs.SetString("Dead", "false");
        }
        if (!PlayerPrefs.HasKey("FilesMade") || PlayerPrefs.GetString("FilesMade").Equals("False"))
            CreateFiles();
        LoadGame();

        if (!PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.SetFloat("Volume", 1.0f);
        if (!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetFloat("Music", 1.0f);
        staticVolume = PlayerPrefs.GetFloat("Volume");
        staticMusicVolume = PlayerPrefs.GetFloat("Music");
        volume = staticVolume;
        musicVolume = staticMusicVolume;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            DefaultSliders();

        if (PlayerPrefs.GetString("Dead").Equals("true") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerDeath();
        }
    }

    public void Restart()
    {
        strength = 0;
        intelligence = 0;
        trickery = 0;
        lives = 3;
        currentScene = "SC1.1";
        madeDecisions.Clear();
        SaveGame();
        PlayerPrefs.SetString("FilesMade", "False");
        SceneManager.LoadScene(0);
    }

    private void PlayerDeath()
    {
        deathMessage.SetActive(true);
        PlayerPrefs.SetString("FilesMade", "False");
        StartCoroutine(DestroyMsg());

        strength = 0;
        intelligence = 0;
        trickery = 0;
        lives = 3;
        currentScene = "SC1.1";
        madeDecisions.Clear();
        SaveGame();

        FileReader fr = new FileReader();
        List<string> files = new List<string>();
        string currentDirectory = Environment.CurrentDirectory;

        files.Add("ASC1.5");
        foreach (string file in files)
        {
            List<string> strings = fr.ReadFromFile(file);
            for (int i = 0; i < strings.Count; i++)
            {
                if (strings[i] == "true")
                {
                    strings[i] = "false";
                }
            }
            fr.WriteToFile(file, strings);
            Environment.CurrentDirectory = currentDirectory;
        }

        PlayerPrefs.SetString("Dead", "false");
    }

    IEnumerator DestroyMsg()
    {
        yield return new WaitForSeconds(5);

        deathMessage.SetActive(false);
    }

    /// <summary>
    /// Saves the current game state using FileReader.WriteToFile("SaveGame", this.SaveValues())
    /// </summary>
    public void SaveGame()
    {
        FileReader fr = new FileReader();
        fr.WriteToFile("SaveGame", SaveValues());
    }

    /// <summary>
    /// Sets global volume settings
    /// </summary>
    private void SetVolume()
    {
        staticVolume = volume;
        staticMusicVolume = musicVolume;

        if (staticVolume < 0)
            staticVolume = 0.0f;
        else if (staticVolume > 1)
            staticVolume = 1.0f;

        if (staticMusicVolume < 0)
            staticMusicVolume = 0.0f;
        else if (staticMusicVolume > 1)
            staticMusicVolume = 1.0f;

        AudioListener.volume = staticVolume;
        PlayerPrefs.SetFloat("Volume", staticVolume);
        PlayerPrefs.SetFloat("Music", staticMusicVolume);
    }

    /// <summary>
    /// Loads most recent game state using this.LoadValues(FileReader.ReadFromFile("SaveGame"))
    /// </summary>
    public void LoadGame()
    {
        FileReader fr = new FileReader();
        string currentDirectory = Environment.CurrentDirectory;
        try
        {
            LoadValues(fr.ReadFromFile("SaveGame"));
        }
        catch (FileNotFoundException)
        {
            Environment.CurrentDirectory = currentDirectory;
            SaveGame();
        }
    }

    /// <summary>
    /// Saves stats, currentScene and madeDecisions.
    /// </summary>
    /// <returns>
    /// List&lt;string&gt; with values.
    /// <br></br>
    /// Strength on line 0
    /// <br></br>
    /// Intellect on line 1
    /// <br></br>
    /// Trickery on line 2
    /// <br></br>
    /// CurrentScene on line 3
    /// <br></br>
    /// Rest of lines saves madeDecisions, one decision per line
    /// </returns>
    private List<string> SaveValues()
    {
        List<string> toSave = new List<string>();
        toSave.Add(strength.ToString());
        toSave.Add(intelligence.ToString());
        toSave.Add(trickery.ToString());
        toSave.Add(lives.ToString());
        toSave.Add(currentScene.ToString());
        foreach (string decision in madeDecisions)
        {
            toSave.Add(decision);
        }

        return toSave;
    }

    /// <summary>
    /// Loads stats, currentScene and madeDecisions.
    /// </summary>
    /// <param name="toLoad">
    /// List&lt;string&gt; with values.
    /// <br></br>
    /// Should only load from FileReader.ReadFromFile("SaveGame")
    /// </param>
    private void LoadValues(List<string> toLoad)
    {
        strength = int.Parse(toLoad.ElementAt(0));
        intelligence = int.Parse(toLoad.ElementAt(1));
        trickery = int.Parse(toLoad.ElementAt(2));
        lives = int.Parse(toLoad.ElementAt(3));
        currentScene = toLoad.ElementAt(4);
        
        for (int i = 5; i < toLoad.Count; i++)
        {
            if (!madeDecisions.Contains(toLoad.ElementAt(i)))
                madeDecisions.Add(toLoad.ElementAt(i));
        }
    }

    private void MasterVolume()
    {
        volume = masterSlider.value;
    }

    private void MusicVolume()
    {
        musicVolume = musicSlider.value;
    }

    private void DefaultSliders()
    {
        masterSlider.value = volume;
        musicSlider.value = musicVolume;
    }
}
