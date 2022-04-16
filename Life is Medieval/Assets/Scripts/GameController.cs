using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int strength = 0;
    private int intellect = 0;
    private int trickery = 0;
    private double currentScene = 0;
    private List<double> madeDecisions = new List<double>();

    /// <summary>
    /// Debug.Log stats, currentScene and madeDecisions.
    /// </summary>
    public void DebugLogValues()
    {
        Debug.Log(strength.ToString());
        Debug.Log(intellect.ToString());
        Debug.Log(trickery.ToString());
        Debug.Log(currentScene.ToString());
        foreach (double decision in madeDecisions)
        {
            Debug.Log(decision.ToString());
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
    public List<string> SaveValues()
    {
        List<string> toSave = new List<string>();
        toSave.Add(strength.ToString());
        toSave.Add(intellect.ToString());
        toSave.Add(trickery.ToString());
        toSave.Add(currentScene.ToString());
        foreach (double decision in madeDecisions)
        {
            toSave.Add(decision.ToString());
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
    public void LoadValues(List<string> toLoad)
    {
        strength = int.Parse(toLoad.ElementAt(0));
        intellect = int.Parse(toLoad.ElementAt(1));
        trickery = int.Parse(toLoad.ElementAt(2));
        currentScene = double.Parse(toLoad.ElementAt(3));
        
        for (int i = 4; i < toLoad.Count; i++)
        {
            madeDecisions.Add(double.Parse(toLoad.ElementAt(i)));
        }
    }
}
