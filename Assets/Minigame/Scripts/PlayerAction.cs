using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class PlayerAction : MonoBehaviour
    {
        public GameObject TargetAnimal = null;

        public FoodSpawner m_FoodSpawner = null;

        public ItemSelection m_ItemSelection = null;

        public HandAnimation m_HandAnimation = null;


        [SerializeField] private float FoodThrowingSpeedRangeMin = 0.4f;
        [SerializeField] private float FoodThrowingSpeedRangeMax = 0.9f;

        [SerializeField] private float FoodThrowingHeightRangeMin = 2.4f;
        [SerializeField] private float FoodThrowingHeightRangeMax = 3.0f;

        void Start()
        {
            Debug.Log(m_ItemSelection.GetItem().name);
        }


        public void ChooseNextItem(InputAction.CallbackContext context)
        {  
            if (context.performed && !m_ItemSelection.autoScroll)
            {
                m_ItemSelection.NextItem();
                Debug.Log(m_ItemSelection.GetItem().name);
            }  
        }

        public void ChoosePreviousItem(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                m_ItemSelection.PreviousItem();
                Debug.Log(m_ItemSelection.GetItem().name);
            }
        }

        public void SelectItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                GameObject choosedItem = m_ItemSelection.GetItem();
                if (choosedItem != null)
                {
                    if (choosedItem.tag == "Food")
                    {

                        choosedItem.GetComponent<FoodSFX>()?.PlayAudioClip();
                        float throwingSpeed = Random.Range(FoodThrowingSpeedRangeMin, FoodThrowingSpeedRangeMax);
                        float throwingHeight = Random.Range(FoodThrowingHeightRangeMin, FoodThrowingHeightRangeMax);
                        GameObject ThrowedFood = m_FoodSpawner.SpawnFood(choosedItem.name,throwingSpeed,throwingHeight);
                        // target is the feeding zone tailored to different animal - penguin has feeding zone for a better throwing animation
                        Transform target = TargetAnimal.transform.Find("FeedingZone");
                        if (target != null)
                        {
                            ThrowedFood.GetComponent<Food>().FeedToAnimal(target.gameObject);  
                        }
                        else
                        {
                            ThrowedFood.GetComponent<Food>().FeedToAnimal(TargetAnimal);
                        }
                      

                    }
                    else if (choosedItem.tag == "Hand")
                    {
                        if (m_HandAnimation.PlayAnimation(HandAnimation.State.Pet))
                        {
                            TargetAnimal.GetComponent<AnimalAction>().TriggerPettingAction();
                        }
                    }
                }
            }

        }
    }

}
