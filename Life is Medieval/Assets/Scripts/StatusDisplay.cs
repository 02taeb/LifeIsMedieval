using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    public Text STR, INT, TRK, HP;
    public GameController gc;

    void Update()
    {
        STR.text = $"STR: {gc.strength}";
        INT.text = $"INT: {gc.intelligence}";
        TRK.text = $"TRK: {gc.trickery}";
        HP.text = $"HP: {gc.lives}";
    }
}
