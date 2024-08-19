using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conflict_Rails : MonoBehaviour
{
    [SerializeField] List<GameObject> rails_enable_list = new List<GameObject>();

    [SerializeField] List<GameObject> rails_disable_list = new List<GameObject>();

    public GameObject Train;
    private string targetObjectName;
    // Start is called before the first frame update
    void Start()
    {
        if (Train != null)
        {
            targetObjectName = Train.name;
        }
        float distance = Vector3.Distance(transform.position, Train.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the collision involves a specific tag or GameObject
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Locomotive_01_Blue")
        {
            // Handle the collision with the target GameObject
            //Debug.Log("Collision with target!");
            foreach (GameObject obj in rails_disable_list)
            {
                // Deactivate (disable) each GameObject in the list
                obj.SetActive(false);
            }
            foreach (GameObject obj in rails_enable_list)
            {
                // Deactivate (disable) each GameObject in the list
                obj.SetActive(true);
            }
        }
    }
}
