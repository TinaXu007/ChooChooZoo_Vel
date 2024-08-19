using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCollection : MonoBehaviour
{
    [SerializeField] GameObject greyStars;
    [SerializeField] GameObject yellowStars;

    [SerializeField] GameObject getStarObject;

    public static int totalStars;
    public static int starsCollected;

    public List<GameObject> yellowStarChildren = new List<GameObject>();
    public List<GameObject> greyStarChildren = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
        StarCollection.totalStars = greyStarChildren.Count;

        if (starsCollected != null)
        {
            updateUserStars(starsCollected);
        }

        // Debugging code:
        // collectStar(0);
        // collectStar(6);
        // collectStar(10);

        // updateUserStars(7);
        // updateUserStars(9);
    }

    IEnumerator hideStarObjectWait()
    {
        AudioController.Instance.PlayGetStar();
        yield return new WaitForSecondsRealtime(0.8f);
        AudioController.Instance.PlayCheering();
        yield return new WaitForSecondsRealtime(2.2f);
        HideStarObject();
    }

// Method for collecting a star - turn it from grey to yellow
    public void collectStar(int index)
    // Input: Integer as index of which star you want to enable. Index begins with 0
    {
        // Check to see if your index has gone out of range
        // Debug.Log(StarCollection.totalStars);
        // Debug.Log(index);

        if (index > StarCollection.totalStars-1)
        {
            Debug.Log("Out of range or already maxed out star rewards.");
            return;
        }
        // Check to see if the current (valid) index is already activated
        else if (yellowStarChildren[index].activeSelf)
        {
            Debug.Log("Already collected this star.");
            return;
        }
        // Activate the yellow star Game Object to show it was collected.
        else
        {
            yellowStarChildren[index].SetActive(true);
            StarCollection.starsCollected += 1;
            ShowStarObject();
            StartCoroutine("hideStarObjectWait");            
            // Debug.Log(StarCollection.starsCollected);
        }
    }
    
    public void HideStarObject()
    {
        getStarObject.SetActive(false);
    }

    public void ShowStarObject()
    {
        getStarObject.SetActive(true);
    }


// Method for updating the stars collected based on user data
    public void updateUserStars(int count)
    // Input: An integer count for number of stars they have collected. Count starts from 1. 
    {
        // Check for valid input
        if (count > StarCollection.totalStars - 1)
        {
            Debug.Log("There are not that many stars.");
            return;
        }
        
        // Iterate through and set the stars active.
        for(int i = 0; i < count; i++)
        {
            yellowStarChildren[i].SetActive(true);
        }
    }
    
    public static StarCollection Instance
    {
        get;
        private set;
    }
    
    void Awake()
    {
        Instance = this;

        if (greyStars != null)
        {
            Transform greyStarsParent = greyStars.transform;

            foreach (Transform childTransform in greyStarsParent)
            {
                greyStarChildren.Add(childTransform.gameObject);
                // Debug.Log(childTransform.gameObject);
            }
        }

        if (yellowStars != null)
        {
            Transform yellowStarsParent = yellowStars.transform;

            foreach (Transform childTransform in yellowStarsParent)
            {
                yellowStarChildren.Add(childTransform.gameObject);
                // Debug.Log(childTransform.gameObject);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
