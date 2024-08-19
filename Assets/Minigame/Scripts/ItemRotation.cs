using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class ItemRotation : MonoBehaviour
    {
        public float RotationSpeed = 20;
        void Update()
        {
            foreach (Transform child in transform)
            {
                child.Rotate(0,RotationSpeed*Time.deltaTime,0,Space.World);
            }
        }

    }
}

