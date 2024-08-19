using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigame;

namespace Minigame
{
    public class MinigameController : MonoBehaviour
    {
        private static MinigameController instance;

        public static MinigameController Instance { get { return instance; } }

        public string AnimalName;


        [SerializeField] private ProgressBar HappinessProgressBar;
        [SerializeField] private ProgressBar HungrinessProgressBar;

        public float happinessIncrease = 15;
        public float hungrinessIncrease = 15;

        private bool isFullProgress = false;

        [SerializeField] private GameObject AnimalIcon;

        private void Awake()
        {
            instance = this;

            SetHappinessAndHungrinessProgress();

            AnimalIcon.SetActive(false);
        }


        private void OnDestroy()
        {
            UpdateHappinessAndHungrinessProgress();
        }

        public void SetHappinessAndHungrinessProgress()
        {

            AnimalStatus status = MinigameStatus.GetAnimalStatus(AnimalName);
            if (status != null)
            {
                Debug.Log("set progress - Find Progress:" + status.HappinessProgress.ToString() + " " + status.HungrinessProgress.ToString());
                HappinessProgressBar.Progress = status.HappinessProgress;
                HungrinessProgressBar.Progress = status.HungrinessProgress;
                if (!isFullProgress && HappinessProgressBar.HasFullProgress() && HungrinessProgressBar.HasFullProgress())
                {
                    isFullProgress = true;
                }
            }
        }

        public void UpdateHappinessAndHungrinessProgress()
        {
            Debug.Log("Update progress: " + HungrinessProgressBar.Progress.ToString() + " " + HappinessProgressBar.Progress.ToString());
            MinigameStatus.UpdateAnimalStatus(AnimalName, HungrinessProgressBar.Progress, HappinessProgressBar.Progress);
        }

        public void AddHapinessProgress()
        {
            HappinessProgressBar.Progress +=  happinessIncrease;
        }

        public void AddHungrinessProgress()
        {
            HungrinessProgressBar.Progress +=  hungrinessIncrease;
        }

        public void CheckFullProgress()
        {
            if (!isFullProgress && HappinessProgressBar.HasFullProgress() && HungrinessProgressBar.HasFullProgress())
            {
                isFullProgress = true;
                OnFullProgress(); // still in progress
            }
        }

        // in progress
        public void OnFullProgress()
        {
            AnimalIcon.SetActive(true);
            AnimalCollection.Instance.collectAnimal(AnimalName);
        }

    }
}

