using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class CustomButton : MonoBehaviour
    {
        [SerializeField] private AudioClip InstrumentSoundAudioClip;

        [SerializeField] private AudioClip InstrumentNameAudioClip;

        private Animator m_Animator;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
        }

        public void OnPressed()
        {
            float scaledVolume = AudioController.Instance.sfxVolumeSetter(0.6f);
            m_Animator.SetTrigger("Pressed");          
            AudioManager.Instance.PlayAudioClip(InstrumentSoundAudioClip, scaledVolume); // Lulu added scaled volume for global volume control

        }

        public void OnSelected(bool isSelected, bool audio)
        {
            if (isSelected)
            {
                float scaledVolume = AudioController.Instance.sfxVolumeSetter(0.6f);
                if (audio) 
                {
                    AudioManager.Instance.PlayAudioClip(InstrumentNameAudioClip, scaledVolume);
                }

            }
            m_Animator.SetBool("Selected", isSelected);
        }
    }
}

