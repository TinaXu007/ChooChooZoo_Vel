using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;

    // Is the title screen active?
    // public bool ShowTitle
    // {
    //     get => TitleScreen.Instance.gameObject.activeSelf;
    // }

    public static TitleScreen Instance
    {
        get;
        private set;
    }

    void Start()
    {
        // titleScreen.SetActive(true); //Show the title screen
        // MenuController.Instance.HideMenu(); // Hide the menu screen
        AudioController.Instance.PlayTitleMusic(); // Play title screen music
    }

    // public void HideTitle()
    // {
    //     showTitle = false;
    //     titleScreen.SetActive(false);
    // }

    private void Awake()
    {
        Instance = this;
    }
}
