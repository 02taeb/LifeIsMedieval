using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVibrate : MonoBehaviour
{
    public void Vibrate()
    {
        Handheld.Vibrate();
        Debug.Log("Vibrate");
    }
}
