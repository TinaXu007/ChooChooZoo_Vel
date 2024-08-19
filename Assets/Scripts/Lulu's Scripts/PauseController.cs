using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioListener audioListener;

    // [SerializeField] GameObject eventSystem;

    [SerializeField] GameObject inputManager;
    [SerializeField] GameObject menu;
    // GameObject inputManager;

    public static PauseController Instance
    {
        get;
        private set;
    }
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void PauseGame ()
    {
        MenuController.Instance.ShowMenu();
        inputManager?.SetActive(false);
        Time.timeScale = 0f;
    }
    public void ResumeGame ()
    {
        menu.SetActive(false);
        inputManager?.SetActive(true);
        Time.timeScale = 1f;
    }

    
    

}
