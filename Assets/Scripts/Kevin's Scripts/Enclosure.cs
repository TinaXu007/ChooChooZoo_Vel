using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enclosure : MonoBehaviour
{
    /* Example of how an enclosure object might work in practice. The animal that is part of the enclosure will be activated when the 
     * car enters the "isTrigger" zone of the enclosure. The actual 3D model for the enclosure should be a separate object, aka. the 
     * parent object of this one. */
    public BaseAnimal targetAnimal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        targetAnimal.PlayAnimation(); // Put in more specific methods later
    }

    private IEnumerator DelayedAnimalResponse(float delay) {
        yield return new WaitForSeconds(delay);
        targetAnimal.PlayAnimation();
    }
}
