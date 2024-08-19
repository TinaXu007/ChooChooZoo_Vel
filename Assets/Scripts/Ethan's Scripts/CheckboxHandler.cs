using UnityEngine;
using UnityEngine.UI; // Required for working with UI elements

public class CheckboxHandler : MonoBehaviour
{
    public Toggle checkbox; // Reference to the UI checkbox
    [SerializeField] private GameObject[] gameObjectsManualMode; // disable manual mode objects
    [SerializeField] private GameObject[] gameObjectsAutoMode; // disable auto mode objects

    void Start()
    {
        // Ensure there's a reference to the checkbox
        if (checkbox != null)
        {
            // Add listener for checkbox value changed
            checkbox.onValueChanged.AddListener(HandleCheckboxChange);
        }
    }

    private void HandleCheckboxChange(bool isChecked)
    {
        Debug.Log("on rail mode is " + !isChecked);
        // Loop through each GameObject and set active state based on checkbox
        foreach (var gameObject in gameObjectsManualMode)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(!isChecked); // Disable if checked, enable if not
            }
        }

        foreach (var gameObject in gameObjectsAutoMode)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(isChecked); // enable if checked, disable if not
            }
        }
    }

    void OnDestroy()
    {
        // Remove listener when the script is destroyed
        if (checkbox != null)
        {
            checkbox.onValueChanged.RemoveListener(HandleCheckboxChange);
        }
    }
}
