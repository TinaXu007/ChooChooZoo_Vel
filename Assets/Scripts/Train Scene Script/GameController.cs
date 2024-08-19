using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TrainMode
{
    public class GameController : MonoBehaviour
    {
        private static GameController instance;

        public static GameController Instance { get { return instance; } }  

        private void Awake()
        {
            instance = this;
        }

        [SerializeField] private ZooQuest m_ZooQuest = null;

        public void ZooQuestAction(string Action)
        {
            m_ZooQuest.CheckAccomplishInstruction(Action);
        }
    }
}

