using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{

    public GameObject LoadingScreen;

    public Image ProgressBarFill;

    public TMP_Text ProgressText;

    public void LoadScene(string sceneName)
    {
        Debug.Log("Start Loading Train Scene!");
        LoadingScreen.SetActive(true);
        ProgressBarFill.fillAmount = 0;
        ProgressText.text = "0 %";
        StartCoroutine(CustomLoadSceneAsync(sceneName));
    }

    IEnumerator CustomLoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            ProgressText.text = Mathf.Ceil(progressValue * 100).ToString() + " %";
            ProgressBarFill.fillAmount = progressValue;

            yield return null; 
        }
    }
    
    public static LoadingScene Instance
    {
        get;
        private set;
    }
    
    void Awake()
    {
        Instance = this;
    }
}
