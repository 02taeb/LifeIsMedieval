using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float volume = 1.0f;
    private static float staticVolume = 1.0f;
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
        FileReader fr = new FileReader();
        List<string> files = new List<string>();
        string currentDirectory = Environment.CurrentDirectory;

        files.Add("ASC1.5");
        files.Add("BSC1.4");
        files.Add("SC1.1");
        files.Add("SC1.2");
        files.Add("SC1.3");
        files.Add("SC1.4");
        files.Add("SC1.5");
        files.Add("SC2.1");
        files.Add("SC3.1");
        files.Add("TSC1.1");
        files.Add("TSC1.2");
        files.Add("TSC1.3");
        files.Add("TSC1.4");
        files.Add("TSC2.1");
        files.Add("TSC3.1E");
        files.Add("TTSC2.1");
        files.Add("TTSC3.1E");

        foreach (string file in files)
        {
            fr.WriteToFile(file, TextAssetToList((TextAsset)Resources.Load("TextsToCreate/" + file)));
            Environment.CurrentDirectory = currentDirectory;
        }
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
            currentScene = GameObject.Find("SceneController").GetComponent<SceneController>().currentScene.sceneName;
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
        // https://discord.com/channels/961255353008406598/964194314873884672/968813090592419870 
        // PlayerPrefs.SetFloat("Volume", 1.0f);
        CreateFiles();
        LoadGame();
        staticVolume = PlayerPrefs.GetFloat("Volume");
        volume = staticVolume;
    }

    private void PlayerDeath()
    {
        // Will these values be overwritten somewhere else?
        // Return to main menu or force first scene somehow.
        // Maybe have another method which forces the player to main menu and then runs this
        // both to avoid overwriting and to reset progress.
        strength = 0;
        intelligence = 0;
        trickery = 0;
        lives = 3;
        currentScene = "SC1.1";
        madeDecisions.Clear();
        SaveGame();

        // Reset aftermath files
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
        if (staticVolume < 0)
        {
            staticVolume = 0.0f;
        }
        else if (staticVolume > 1)
        {
            staticVolume = 1.0f;
        }
        AudioListener.volume = staticVolume;
        PlayerPrefs.SetFloat("Volume", staticVolume);
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
}
