using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class AnimalAnimation : MonoBehaviour
    {
        private Animation m_Animation;

        [SerializeField] private string m_IdleAnimation;
        [SerializeField] private string m_EatAnimation;
        [SerializeField] private string m_DanceAnimation;
        [SerializeField] private string m_WalkAnimaion;

        [SerializeField] private AudioClip m_DanceAudioClip = null;
        [Range(0.0f,1.0f)] public float DanceAudioVolume = 0.5f;

        [SerializeField] private AudioClip m_EatAudioClip = null;
        [Range(0.0f, 1.0f)] public float EatAudioVolume = 0.5f;
        public enum State
        {
            Idle,
            Eat,
            Dance,
            Walk
        }

        private State m_State = State.Idle;
        private State original_State = State.Idle;

        private AnimalWalk m_AnimalWalk;

        private void Start()
        {
            m_Animation = GetComponent<Animation>();
            m_Animation.wrapMode = WrapMode.Loop;
            m_Animation[m_EatAnimation].wrapMode = WrapMode.Once;
            m_Animation[m_EatAnimation].layer = 1;
            m_Animation[m_DanceAnimation].wrapMode = WrapMode.Once;
            m_Animation[m_DanceAnimation].layer = 2;

            // Walking animation
            if (m_AnimalWalk != null)
            {
                m_AnimalWalk = GetComponent<AnimalWalk>();
                m_AnimalWalk.SetWalking(false);
            }
        }
        private void Update()
        {
            if (m_State == State.Idle)
            {
                m_Animation.CrossFadeQueued(m_IdleAnimation);
                original_State = State.Idle;
                m_State = State.Idle;
            }

            if (m_State == State.Walk)
            {
                m_Animation.CrossFade(m_WalkAnimaion);
                m_AnimalWalk.SetWalking(true);
                m_Animation[m_EatAnimation].blendMode = AnimationBlendMode.Additive;
                original_State = State.Walk;
                m_State = State.Walk;
            }

            if (m_State == State.Eat)
            {           
                if (m_EatAudioClip != null && !m_Animation.IsPlaying(m_EatAnimation))
                {
                    //First, adjust the volume based on global factor
                    float scaledEatVolume = AudioController.Instance.sfxVolumeSetter(EatAudioVolume);
                    AudioManager.Instance.PlayAudioClip(m_EatAudioClip, scaledEatVolume);
                }
                m_Animation.CrossFade(m_EatAnimation);
                m_State = original_State;
            }

            if(m_State == State.Dance)
            {
                m_Animation.CrossFade(m_DanceAnimation);
                if (m_DanceAudioClip != null)
                {
                    float scaledDanceVolume = AudioController.Instance.sfxVolumeSetter(DanceAudioVolume);
                    AudioManager.Instance.PlayAudioClip(m_DanceAudioClip, scaledDanceVolume);
                }
                m_State = original_State;
            }

        }

        public void SetState(State _state)
        {
            if (m_State != _state)
            {
                m_State = _state;
            }
        }
    }   

}
