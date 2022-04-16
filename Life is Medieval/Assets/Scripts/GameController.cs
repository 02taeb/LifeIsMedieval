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

    public void DebugValues()
    {
        Debug.Log(strength.ToString());
        Debug.Log(intellect.ToString());
        Debug.Log(trickery.ToString());
        Debug.Log(currentScene.ToString());
        Debug.Log(madeDecisions.ToString());
    }

    public void SetTest()
    {
        SetStatsTest();
        SetSceneTest();
        MakeDecisionsTest();
    }

    public void ResetTest()
    {
        ResetSceneTest();
        ZeroStatsTest();
        ResetDecisionsTest();
    }

    public List<string> SaveTest()
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

    public void LoadTest(List<string> toLoad)
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

    private void MakeDecisionsTest()
    {
        madeDecisions.Add(0.2);
        madeDecisions.Add(1.1);
        madeDecisions.Add(2.0);
        madeDecisions.Add(3.2);
    }

    private void ZeroStatsTest()
    {
        strength = 0;
        intellect = 0;
        trickery = 0;
    }

    private void SetStatsTest()
    {
        strength = 10;
        intellect = 3;
        trickery = 5;
    }

    private void ResetSceneTest()
    {
        currentScene = 0;
    }

    private void SetSceneTest()
    {
        currentScene = 4.5;
    }

    private void ResetDecisionsTest()
    {
        madeDecisions.Clear();
    }
}
