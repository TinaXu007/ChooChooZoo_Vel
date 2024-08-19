using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPoint : MonoBehaviour
{
    private Vector3 coords;

    private void Awake()
    {
        coords = this.gameObject.transform.position; 
    
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetLocation()
    {
        return new Vector2(coords.x, coords.z);
    }
}
