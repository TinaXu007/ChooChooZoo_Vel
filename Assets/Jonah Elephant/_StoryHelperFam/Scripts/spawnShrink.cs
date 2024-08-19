using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnShrink : MonoBehaviour
{

    private float start = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3 (start, start, start);
        start = (start - (2f * Time.deltaTime));

            if (start < 0) {
            Destroy(gameObject);
        }
    }
}
