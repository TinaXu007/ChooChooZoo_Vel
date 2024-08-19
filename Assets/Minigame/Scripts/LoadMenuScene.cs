using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour
{
    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("Temp_TitleScreen");
    }
}
