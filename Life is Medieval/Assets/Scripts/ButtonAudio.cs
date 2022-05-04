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

    /*void BasicVariationTechniques()
    {


        source.volume = Random.Range(minVolume, maxVolume);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(sound_clip);
    }*/

}
