using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Minigame
{
    public class FeedInstruction : MonoBehaviour
    {
        [SerializeField] private GameObject animal;

        [SerializeField] private List<string> foods = new List<string>();

        [SerializeField] private GameObject correct = null;

        [SerializeField] private GameObject incorrect = null;

        [SerializeField] private TMP_Text FeedInstrucText = null;

        [SerializeField] private float delay = 2.0f;

        // Instruction: Feed + foods + to the + animal;
        private string FeedingInstruction_1 = "Feed ";
        private string FeedingInstruction_2 = " to the ";

        private int index = 0;
        private bool hasIntruction = false;

        private int score = 0;

        private void Start()
        {
            if (correct != null)
            {
                correct.SetActive(false);
            }
            if (incorrect != null)
            {
                incorrect.SetActive(false);
            }
            GetInstruction();
        }

        public void GetInstruction()
        {
            hasIntruction = true;
            correct.SetActive(false);
            incorrect.SetActive(false);
            FeedInstrucText.text = FeedingInstruction_1 + GetRandomFood() + FeedingInstruction_2 + animal.name;
        }

        public void CheckSelectCorrectItem(string item)
        {
            if (hasIntruction)
            {
                if (item == foods[index])
                {
                    StartCoroutine(CorrectCoroutine());
                }
                else
                {
                    StartCoroutine(IncorrectCoroutine());
                }
                hasIntruction = false;
            }
        }

        private IEnumerator CorrectCoroutine()
        {
            correct.SetActive(true);
            //score++;
            // if score reach 10, animal start walking.
            if (score == 10)
            {
                animal.GetComponent<AnimalAnimation>().SetState(AnimalAnimation.State.Walk);
            }
            yield return new WaitForSeconds(delay);
            GetInstruction();
        }

        private IEnumerator IncorrectCoroutine()
        {
            incorrect.SetActive(true) ;
            yield return new WaitForSeconds(delay);
            GetInstruction();
        }

        private string GetRandomFood()
        {
            int rand = Random.Range(0, foods.Count);
            index = rand;
            return foods[rand];
        }
    }

}
