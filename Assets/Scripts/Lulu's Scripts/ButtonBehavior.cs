using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{

    // Function for pressing the Play button
    public void PlayTitle()
    {
        Debug.Log("Button pressed");
        MenuController.Instance.ShowMenu();
        TitleScreen.Instance.gameObject.SetActive(false);
        // MenuController.Instance.ShowMenu();
        // TitleScreen.Instance.HideTitle();
    }
    // MENU BUTTON FUNCTIONS

    public void selectMinigames()
    {
        MenuController.Instance.ShowMinigames();
        MenuController.Instance.HidePause();
        MenuController.Instance.HideSettings();
    }

    public void showSettings()
    {
        MenuController.Instance.ShowSettings();
        MenuController.Instance.HidePause();
        MenuController.Instance.HideMinigames();
        // StarCollection.Instance.collectStar(0);
    }

    public void showAnimalsMenu()
    {
        MenuController.Instance.ShowAnimalsMenu();
    }

    public void showStarsMenu()
    {
        MenuController.Instance.ShowStarsMenu();
    }

    // SCENE SWITCHING FUNCTIONS

    public void ExitCheck()
    {
        MenuController.Instance.ShowExit();
    }

    public void ShowLogin()
    {
        PlayScene("New Login");
    }
    
    public void PlayTrainMode()
    {
        PlayScene("Train_Scene");
    }

    public void PlayStory()
    {
        PlayScene("StoryScene");
    }

    public void PlayDanceParty()
    {
        PlayScene("Dance Party Minigame");
    }

    public void PlayScreenTest()
    {
        PlayScene("ScreenTestQuestions");
    }

    // Function for pressing the play minigame button
    public void PlayElephant()
    {
        PlayScene("Minigame Scene Elephant");
    }

    public void PlayPenguin()
    {
        PlayScene("Minigame Scene Penguin");
    }
    public void PlayDonkey()
    {
        PlayScene("Minigame Scene Donkey");
    }


    public void PlayScene(string sceneName)
    {
        MenuController.Instance?.HideMenu();
        SceneManager.LoadScene(sceneName);
        PauseController.Instance.ResumeGame();
    }

    // Function for the quit button. Debug.log for unity testing
    public void Quit()
    {
        Application.Quit();
        // SceneManager.LoadScene("Train_Scene");
        Debug.Log("Player has quit the game.");
    }
}
