using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTestSFX : MonoBehaviour
{
    public AudioClip[] audioClips; // Store the needed SFX in here

    private AudioSource audioSource; // The audio source that is in the same object as this one
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // input - index of the clip from the array, that we want to play
    public void PlaySelectedClip(int input)
    {
        audioSource.clip = audioClips[input];
        audioSource.Play();
    }
}