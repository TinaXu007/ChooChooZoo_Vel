using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManager", menuName = "Fall2023ECSE390Team4/SceneManager", order = 0)]
public class CustomSceneManager : ScriptableObject    
{
    private Stack<int> loadedScenes;

    [System.NonSerialized]
    private bool initialized;

    private void Init()
    {
        loadedScenes = new Stack<int>();
        initialized = true;
    }

    public UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    public void LoadScene(int buildIndex)
    {
        if (!initialized)
        {
            Init();
        }
        loadedScenes.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        if (!initialized)
        {
            Init();
        }
        loadedScenes.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


    public void LoadPreviousScene()
    {
        if (!initialized)
        {
            Debug.LogError( "You haven't used the LoadScene functions of the scriptable object. Use them instead of the LoadScene functions of Unity's SceneManager." );
        }
        if (loadedScenes.Count > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadedScenes.Pop());
        }
        else 
        {
            Debug.LogError( "No previous scene loaded" );
        }
    }
}
