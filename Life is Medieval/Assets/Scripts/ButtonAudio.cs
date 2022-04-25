using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;


    public void ButtonSound()
    {
        source.PlayOneShot(clip);
    }

}
