using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject minigameMenu;
    [SerializeField] GameObject animalsMenu;
    [SerializeField] GameObject starsMenu;
    [SerializeField] GameObject exitCheck;
    
    // All the buttons in the menu overlay
    public List<GameObject> buttons;
    public List <GameObject> menuButtons;
    // public EventSystem eventSystem;

    // Selections are based on indexing
    private int selectedIndex = 0;
    Selectable currButton;
    Selectable prevButton;


    // Show menu. Used by PauseController
    public void ShowMenu()
    {        
        menu.SetActive(true);  
        ShowPause();
        HideSettings();
        HideMinigames();
        HideAnimals();
        HideStars();
        HideExit();
        
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Animator>().Play("Normal");  
        }      
    }

    public void ShowExit()
    {
        exitCheck.SetActive(true);
    }

    public void HideExit()
    {
        exitCheck.SetActive(false);
    }

    public void ShowAnimalsMenu()
    {
        HidePause();
        HideSettings();
        HideStars();
        ShowAnimals();
    }

    public void ShowStarsMenu()
    {
        HidePause();
        HideSettings();
        HideAnimals();
        ShowStars();
    }

    // Hide menu. Used by PauseController
    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void HidePause()
    {
        hideObject(pauseMenu);
    }

    public void ShowPause()
    {
        showObject(pauseMenu);
    }
    
    public void HideSettings()
    {
        hideObject(settingsMenu);
    }

    public void ShowSettings()
    {
        showObject(settingsMenu);
    }

    public void HideMinigames()
    {
        hideObject(minigameMenu);
    }

    public void ShowMinigames()
    {
        showObject(minigameMenu);
    }

    public void ShowAnimals()
    {
        showObject(animalsMenu);
    }

    public void HideAnimals()
    {
        hideObject(animalsMenu);
    }

    public void ShowStars()
    {
        showObject(starsMenu);
    }

    public void HideStars()
    {
        hideObject(starsMenu);
    }

    private void showObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void hideObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    
    
    public static MenuController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {        
        Instance = this;
        currButton = buttons[selectedIndex].GetComponent<Button>();
        prevButton = buttons[(selectedIndex + buttons.Count-1) % buttons.Count].GetComponent<Button>();
    }


    private void Update()
    {
        HandleUpdate();
    }
    
    private void HandleUpdate()
    {
        // Update play and normal toggle for current button & previous button

        // if (menu.activeSelf)
        // {
        //     currButton.GetComponent<Animator>().Play("Highlighted");
        //     prevButton.GetComponent<Animator>().Play("Normal");

        //     if (Input.GetKeyDown(KeyCode.DownArrow))
        //     {
        //         // Move to next button
        //         selectedIndex = (selectedIndex + 1) % buttons.Count;

        //         // Play sound when moving to next button
        //         AudioController.Instance.PlayButtonHover();

        //         // Update state of the buttons
        //         currButton = buttons[selectedIndex].GetComponent<Button>();
        //         prevButton = buttons[(selectedIndex + (buttons.Count-1)) % buttons.Count].GetComponent<Button>();

        //         // Debug.Log(currButton.SelectionState);
        //     }

        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         buttons[selectedIndex].GetComponent<Button>().onClick.Invoke();
        //     }
        // }
    }
}