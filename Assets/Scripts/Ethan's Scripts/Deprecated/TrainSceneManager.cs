using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static TrainSceneManager Instance; // Singleton instance

    public string PreviousScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void EnterMiniGame()
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("MiniGameScene");
    }

    public void ExitMiniGame()
    {
        SceneManager.LoadScene(PreviousScene);
    }
}
