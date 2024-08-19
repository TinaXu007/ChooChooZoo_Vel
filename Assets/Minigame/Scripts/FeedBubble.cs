using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


namespace Minigame
{
    public class FeedBubble : MonoBehaviour
    {
        [SerializeField] private GameObject _CorrectImage;
        [SerializeField] private GameObject _IncorrectImage;
        [SerializeField] private Image _FoodToFeedImage;

        [SerializeField] private List<Sprite> _foodSprites = new List<Sprite>();

        [SerializeField] private float _nextFoodDelay = 2.0f;

        private int index = 0;
        private bool hasInstruction = false;
        private void Awake()
        {
            _CorrectImage.SetActive(false);
            _IncorrectImage.SetActive(false);
            NextFood();
        }
        public bool CheckFeedingCorrectFood(string foodName)
        {
            if (hasInstruction) 
            {
                hasInstruction = false;
                if (foodName == _foodSprites[index].name)
                {
                    StartCoroutine(CorrectCoroutine());
                    return true;
                }
                else
                {
                    StartCoroutine(IncorrectCoroutine());
                    return false;
                }
            }
            return false;

        }
        public void NextFood()
        {
            hasInstruction = true;
            _FoodToFeedImage.sprite = GetRandomFood();
        }

        private Sprite GetRandomFood()
        {
            int rand = UnityEngine.Random.Range(0, _foodSprites.Count);
            index = rand;
            return _foodSprites[rand];
        }

        private IEnumerator CorrectCoroutine()
        {
            _CorrectImage.SetActive(true);
            yield return new WaitForSeconds(_nextFoodDelay);
            _CorrectImage.SetActive(false);
            NextFood();
        }

        private IEnumerator IncorrectCoroutine()
        {
            _IncorrectImage.SetActive(true);
            yield return new WaitForSeconds(_nextFoodDelay);
            _IncorrectImage.SetActive(false);
            NextFood();
        }
    }
}

