using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    // Fields for music and sfx audio sources. Should be the essential Objects prefab
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;
    [SerializeField] AudioSource minigameBgmPlayer;

    // Place to store all the music
    [SerializeField] AudioClip titleMusic;
    [SerializeField] AudioClip zooMusic;
    [SerializeField] AudioClip minigameMusic;
    [SerializeField] AudioClip babyShark;

    // Place to store all the sfx
    [SerializeField] AudioClip buttonPop;
    [SerializeField] AudioClip getStar;
    [SerializeField] AudioClip cheering;

    [SerializeField] AudioClip pressLeft;
    [SerializeField] AudioClip pressRight;

    // Slider for volume control
    public Slider musicSlider;
    public Slider sfxSlider;

    // For global volume control
    public static float sfxFactor = 1f;
    public static float musicFactor = 1f;
    
    public static AudioController Instance
    {
        get;
        private set;
    }

    public void getMusicVolume()
    {
        AudioController.musicFactor = musicSlider.value;
        
        musicPlayer.volume = AudioController.musicFactor;

        if (minigameBgmPlayer != null)
        {
            minigameBgmPlayer.volume = AudioController.musicFactor;
        }
    }

    public void getSfxVolume()
    {
        AudioController.sfxFactor = sfxSlider.value;
    }

    public float sfxVolumeSetter(float sourceV)
    {
        return (sourceV * Mathf.Log10(AudioController.sfxFactor+1)*20);
    }

    public float musicVolumeSetter(float sourceV)
    {
        return (sourceV * Mathf.Log10(AudioController.musicFactor+1)*20);
    }


    private void Awake()
    {
        Instance = this;
        
        musicSlider.value = AudioController.musicFactor;
        sfxSlider.value = AudioController.sfxFactor;
        // musicSlider.value = 0.5f;
        // AudioController.sfxFactor = 0.5f;
        // AudioController.musicFactor = 0.5f;
        
        // Debug.Log(musicPlayer.volume);
        // if (minigameBgmPlayer != null)
        // {
        //     minigameBgmPlayer.volume = 0.5f;
        // }
    }

    public void PlayPressLeft()
    {
        PlaySFX(pressLeft, AudioController.sfxFactor);
        Debug.Log("we playin");
    }

    public void PlayPressRight()
    {
        PlaySFX(pressRight, AudioController.sfxFactor);
    }

    public void PlayButtonHover()
    {
        PlaySFX(buttonPop,AudioController.sfxFactor);
    }

    public void PlayGetStar()
    {
        PlaySFX(getStar, AudioController.sfxFactor);
    }

    public void PlayCheering()
    {
        PlaySFX(cheering, AudioController.sfxFactor);
    }

    public void PlayTitleMusic()
    {
        PlayMusic(titleMusic, 0.5f);
    }

    public void PlayMinigameMusic()
    {
        PlayMusic(minigameMusic, AudioController.musicFactor);
    }

    public void PlayBabyShark()
    {
        PlayMusic(babyShark, AudioController.musicFactor);
    }

    public void PlayMusic(AudioClip clip, float volume, bool loop = true)
    {
        if (clip == null)
        {
            return;
        }
         
        // Player settings
        musicPlayer.clip = clip;
        musicPlayer.loop = loop;
        musicPlayer.volume = volume;

        musicPlayer.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            return;
        }

        sfxPlayer.clip = clip;
        sfxPlayer.volume = volume;

        sfxPlayer.Play();
    }
    

}
