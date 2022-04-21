using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioFitterAdjust : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<AspectRatioFitter>().aspectRatio = (float) Screen.width / (float) Screen.height;
    }
}
