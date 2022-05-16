using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAudioTimeScale : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0 && audioSource.isPlaying)
            audioSource.Pause();
        else if (Time.timeScale != 0 && !audioSource.isPlaying)
            audioSource.Play();
    }
}
