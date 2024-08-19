using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storyHandle : MonoBehaviour
{
    public GameObject[] storyPrefabs;

    public int storyAt = -1;

    public KeyCode buttPress; 

    public 


    // Start is called before the first frame update
    void Start()
    {
        storyAdvance();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttPress))
        {
            Debug.Log("Space key was pressed.");

            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }

            storyAdvance();
        }
    }


    public void storyAdvance() {
        storyAt++;
        Instantiate(storyPrefabs[storyAt], this.transform);
    }
}
