using UnityEngine;
using System.Collections.Generic;

public class AutoRailSelector : MonoBehaviour
{
    public List<Collider> rails; // List of rails to choose from
    public GameObject train; // Reference to the train

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == train)
        {
            SelectRandomRail();
        }
    }

    private void SelectRandomRail()
    {
        if (rails.Count == 0)
        {
            Debug.LogError("No rails assigned in the list.");
            return;
        }
        
        foreach (Collider rail in rails)
        {
            rail.enabled = false;
        }

        int randomIndex = Random.Range(0, rails.Count); // Get random index
        Collider selectedRail = rails[randomIndex]; // Select rail at random index

        // Activate or do something with the selected rail
        // For example, just logging its name here
        selectedRail.enabled = true;
        Debug.Log("Selected Rail: " + selectedRail.name);

        // Implement your logic to handle the rail selection
        // This could be anything depending on your game mechanics
    }
}