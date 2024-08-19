using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefsOnExitGame : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs data cleared on application quit.");
    }
}
