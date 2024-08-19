#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class ClearPlayerPrefsOnExitPlayMode : MonoBehaviour
{
    static ClearPlayerPrefsOnExitPlayMode()
    {
        EditorApplication.playModeStateChanged += HandlePlayModeStateChanged;
    }

    private static void HandlePlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            // Clear PlayerPrefs when exiting Play mode
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs cleared on exiting Play mode.");
        }
    }
}
#endif