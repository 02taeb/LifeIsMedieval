using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
