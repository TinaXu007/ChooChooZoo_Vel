using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class AnimalDance : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Animals = new List<GameObject>();

        public void SetAnimalDance(int[] indices)
        {
            foreach (int i in indices)
            {
                Animals[i].GetComponent<AnimalAnimation>().SetState(AnimalAnimation.State.Dance);
            }
        }
    }

}
