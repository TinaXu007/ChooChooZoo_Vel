using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;


public class AudioManager : MonoBehaviour
{ 
    private static AudioManager instance;

    private AudioSource[] audioSources;

    public static AudioManager Instance
    {
        get 
        {
            if (instance == null) Debug.LogError("Audio Manager is null!");
            return instance; 
        }

    }
    
    private int GetIdleAudioSouceIndex()
    {
        for (int i = 0; i < audioSources.Length; i++) 
        {
            if (!audioSources[i].isPlaying) 
            {
                return i;
            }
        }
        return -1;
    }

    private void Awake()
    {
        instance = this;
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayAudioClip(AudioClip _clip,float volume)
    {
        int index = GetIdleAudioSouceIndex();
        if (index != -1)
        {
            audioSources[index].clip = _clip;
            audioSources[index].volume = volume;
            audioSources[index].Play();
        }

    }
}
