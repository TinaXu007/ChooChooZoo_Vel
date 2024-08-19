using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Minigame
{
    public class AnimalAction : MonoBehaviour
    {
        [SerializeField] private FeedBubble _FeedBubble;

        private AnimalParticle _animalParticle;
        private AnimalAnimation _animalAnimation;

        private void Awake()
        {
            _animalAnimation = GetComponent<AnimalAnimation>();
            _animalParticle = GetComponent<AnimalParticle>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Food")
            {

                // check if feeding correct food in the thought bubble
                bool correctFeed = _FeedBubble.CheckFeedingCorrectFood(other.name);

                // new feature : hungriness and happiness
                if (correctFeed)
                {
                    Debug.Log("Correct Feed!");
                    MinigameController.Instance.AddHapinessProgress();
                    MinigameController.Instance.AddHungrinessProgress();
                    MinigameController.Instance.CheckFullProgress();
                }
            }
        }

        public void TriggerPettingAction()
        {
            _animalParticle.PlayParticle(3f);
            _animalAnimation.SetState(AnimalAnimation.State.Dance);
            MinigameController.Instance.AddHapinessProgress();
            MinigameController.Instance.CheckFullProgress();
        }
        
    }
}

