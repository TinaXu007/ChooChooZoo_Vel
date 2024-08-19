using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class HandAnimation : MonoBehaviour
    {
        private Animation m_Animation;

        [SerializeField] private GameObject m_HandObject;

        [SerializeField] private AnimationClip m_PetAnimation;


        private bool IsHandActive = true;

        public enum State
        {
            Pet
        }
        private void Start()
        {
            m_Animation = GetComponent<Animation>();
        }
        private void Update()
        {
            if (IsHandActive && !m_Animation.isPlaying)
            {
                m_HandObject.SetActive(false);
                IsHandActive = false;
            }
            else if (!IsHandActive && m_Animation.isPlaying)
            {
                m_HandObject.SetActive(true);
                IsHandActive = true;
            }
        }

        public bool PlayAnimation(State _state)
        {
            if (m_Animation.isPlaying) return false;

            if (_state == State.Pet)
            {
                m_Animation.Play(m_PetAnimation.name);
            }
            return true;
        }

    }

}
