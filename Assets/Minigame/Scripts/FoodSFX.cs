using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Minigame
{
    public class FoodSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip FoodAudioClip;
        [Range(0f, 1f)] public float FoodAudioVolume = 1;

        [SerializeField] private float delay = 3.0f;

        private bool isPlaying = false;

        public void PlayAudioClip()
        {
            if (!isPlaying)
            {
                StartCoroutine(PlayAudioClipEnumerator());
            }
        }

        private IEnumerator PlayAudioClipEnumerator()
        {
            isPlaying = true;
            float scaledVolume = AudioController.Instance.sfxVolumeSetter(FoodAudioVolume);
            AudioManager.Instance.PlayAudioClip(FoodAudioClip,scaledVolume); // Lulu added scaled volume for global volume control
            yield return new WaitForSeconds(delay);
            isPlaying = false;
        }
    }
}

