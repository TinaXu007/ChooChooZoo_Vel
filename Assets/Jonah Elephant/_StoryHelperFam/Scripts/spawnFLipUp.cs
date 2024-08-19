using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFLipUp : MonoBehaviour { 

    public float speedGo = 180;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation.x > 0)
        {
            transform.Rotate(new Vector3(-speedGo, 0f, 0f) * 1 * Time.deltaTime);
        }
        
    }
}
