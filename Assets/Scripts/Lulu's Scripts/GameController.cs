using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public bool gamePaused;

    private float trialVolume = 0.1f;

    public static GameController Instance
    {
        get;
        private set;
    }

    public float getTrialVolume()
    {
        return trialVolume;
    }

    public void setTrialVolume(float volume)
    {
        trialVolume = volume;
        Debug.Log("Set volume to "+ volume);
    }

    private void Awake()
    {
        Instance = this;
        Debug.Log(trialVolume);
    }
    
    private void Update()
    {
        EscapeUpdate();        
    }


    private void EscapeUpdate()
    {
        if (SceneManager.GetActiveScene().name == "01Title")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                PauseController.Instance.ResumeGame();
            }
            else
            {
                PauseController.Instance.PauseGame();
            }

            gamePaused = !gamePaused;
        }

        // Debugging code for animal collection
        // Debug.Log(AnimalCollection.collectedAnimals);
        
        // foreach (int ele in AnimalCollection.collectedAnimals)
        // {
        //     Debug.Log(ele);
    //     }
    }
}

            // if (TitleScreen.Instance.gameObject.activeSelf)
            // {
            //     return;
            // }