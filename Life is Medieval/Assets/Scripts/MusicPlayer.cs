using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource audioSource;
    private bool paused;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = GameObject.Find("GameController").GetComponent<GameController>().musicVolume;

        if (Time.timeScale != 0 && !audioSource.isPlaying && !paused)
            StartCoroutine(Wait());
            
        
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
        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(tracks[Random.Range(0, tracks.Length)]);
    }
}
