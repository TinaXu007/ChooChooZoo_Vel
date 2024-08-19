using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AnimalCollection : MonoBehaviour
{
    [SerializeField] GameObject allAnimals;

    public static int totalAnimals;

    public static HashSet<int> collectedAnimals; 

    public List<GameObject> animalsChildren = new List<GameObject>();
    
    // Random rnd = new Random();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        // if (collectedAnimals != null)
        // {
        //     Transform animalsParent = allAnimals.transform;

        //     foreach (Transform childTransform in animalsParent)
        //     {
        //         animalsChildren.Add(childTransform.Find("Block").gameObject);
        //         // Debug.Log(childTransform.Find("Block").gameObject);
        //         // Debug.Log(animalsChildren.Count);
        //     }
        // }

        AnimalCollection.totalAnimals = animalsChildren.Count;
        
        if (collectedAnimals != null)
        {
            updateAnimals(AnimalCollection.collectedAnimals.ToArray());
        }
        
        //Debugging:
        // int index = rnd.Next(0,5);
        // Debug.Log(index);
        // collectAnimal(index);
        // collectAnimal(3);
        // int[] array = {0,3,5};
        // updateAnimals(array);
        // Debug.Log(collectedAnimals);

        // foreach (int ele in AnimalCollection.collectedAnimals)
        // {
        //     Debug.Log(ele);
        // }
    }

    public void collectAnimal(int index)
    {
        // Input: index of the animal collected.
        // Indices of animals:
        // Elephant: 0
        // Donkey: 1
        // Monkey: 2
        // Penguin: 3
        // Seal: 4
        // Dolphin: 5
        if (index >= AnimalCollection.totalAnimals)
        {
            Debug.Log("Index is out of range.");
        }

        animalsChildren[index].SetActive(false);
        AnimalCollection.collectedAnimals.Add(index);
    }

    public void collectAnimal(string animalName)
    {
        int index = -1;
        switch (animalName)
        {
            case "Elephant":
                index = 0;
                break;

            case "Donkey":
                index = 1;
                break;

            case "Monkey":
                index = 2;
                break;

            case "Penguin":
                index = 3;
                break;

            case "Seal":
                index = 4;
                break;

            case "Dolphin":
                index = 5;
                break;

            default:
                Debug.Log("Animal not in the list!");
                break;
        }

        animalsChildren[index].SetActive(false);
        AnimalCollection.collectedAnimals.Add(index);

    }

    public void updateAnimals(int[] indices)
    {
        // Input: Array of integer indices of animals collected
        // Indices of animals:
        // Elephant: 0
        // Donkey: 1
        // Monkey: 2
        // Penguin: 3
        // Seal: 4
        // Dolphin: 5

        foreach (int i in indices)
        {
            if (i >= AnimalCollection.totalAnimals)
            {
                Debug.Log("Index is out of range");
                continue;
            }
            animalsChildren[i].SetActive(false);
            AnimalCollection.collectedAnimals.Add(i);
        }
    }

    private static AnimalCollection instance;

    public static AnimalCollection Instance { get { return instance; } }

    void Awake()
    {
        instance = this;

        Debug.Log("Animal Collection awake");
        
        if (collectedAnimals == null)
        {
            collectedAnimals = new HashSet<int>(); 
        }

        if (collectedAnimals != null)
        {
            Transform animalsParent = allAnimals.transform;

            foreach (Transform childTransform in animalsParent)
            {
                animalsChildren.Add(childTransform.Find("Block").gameObject);
                // Debug.Log(childTransform.Find("Block").gameObject);
                // Debug.Log(animalsChildren.Count);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
