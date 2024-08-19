using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class AnimalStatus
    {
        public string AnimalName { get; set; }
        public float HungrinessProgress { get; set; }
        public float HappinessProgress { get; set; }
    }
    public static class MinigameStatus
    {
        public static List<AnimalStatus> AnimalStatuses = new List<AnimalStatus>();

        public static AnimalStatus GetAnimalStatus(string animalName)
        {
            return AnimalStatuses.Find((s) => s.AnimalName == animalName);
        }

        public static void AppendAnimalStatus (string _AnimalName , float _hungrinessProgress, float _happinessProgress)
        {
            AnimalStatus animalStatus = new AnimalStatus();
            animalStatus.AnimalName = _AnimalName;
            animalStatus.HungrinessProgress = _hungrinessProgress;
            animalStatus.HappinessProgress = _happinessProgress;
            AnimalStatuses.Add(animalStatus);
        }

        public static void UpdateAnimalStatus(string _AnimalName, float _hungrinessProgress, float _happinessProgress)
        {
            AnimalStatus animalStatus = AnimalStatuses.Find((s) => s.AnimalName == _AnimalName);
            if (animalStatus != null)
            {
                animalStatus.HungrinessProgress = _hungrinessProgress;
                animalStatus.HappinessProgress = _happinessProgress;
            }
            else
            {
                AppendAnimalStatus(_AnimalName, _hungrinessProgress, _happinessProgress);
            }
        }

    }

}
