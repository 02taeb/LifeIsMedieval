using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource audioSource;
    private bool paused, play = true;
    private int lastPlayed, toPlay;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = GameObject.Find("GameController").GetComponent<GameController>().musicVolume;

        if (Time.timeScale != 0 && !audioSource.isPlaying && !paused && play)
        {
            StartCoroutine(Wait());
        }
            
            
        
        if (Time.timeScale == 0 && audioSource.isPlaying)
        {
            audioSource.Pause();
            paused = true;
        }   
        else if (Time.timeScale != 0 && !audioSource.isPlaying)
        {
            audioSource.UnPause();
            paused = false;
        }
    }

    private IEnumerator Wait()
    {
        play = false;
        
        yield return new WaitForSeconds(1);

        do
            toPlay = Random.Range(0, tracks.Length);
        while (toPlay == lastPlayed);

        audioSource.PlayOneShot(tracks[toPlay]);
        lastPlayed = toPlay;

        play = true;
    }
}
