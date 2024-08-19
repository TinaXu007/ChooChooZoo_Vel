using System.Collections;
using System.Collections.Generic;
using Minigmae;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Minigame
{
    public class SimonSays : MonoBehaviour
    {
        [SerializeField] InstrumentSelection _instrumentSelection;

        [SerializeField] InstrumentSequence _instrumentSequence;

        [SerializeField] GameObject _inputManager;

        [SerializeField] float delayBetweenItem = 5f;

        [SerializeField] TMP_Text WhoseTurn;

        [SerializeField] GameObject FinishAnimation;



        public bool Play = true;



        public void PlaySimonSays()
        {
            int[] _sequence = _instrumentSequence.GetSequence();
            StartCoroutine(SimonSaysCoroutine(_sequence));  
        }

        public void Finished()
        {
            FinishAnimation.SetActive(true);
        }

        IEnumerator SimonSaysCoroutine(int[] _sequence)
        {
            WhoseTurn.text = "My Turn!";
            _inputManager.SetActive(false);
            _instrumentSelection.ShowSelectedInstrument(false);
            for (int i=0; i < _sequence.Length; i++)
            {
                _instrumentSelection.PlayItem(_sequence[i],delayBetweenItem);
                yield return new WaitForSeconds(delayBetweenItem);
            }
            _instrumentSelection.ShowSelectedInstrument(true);
            WhoseTurn.text = "Your Turn!";
            _inputManager.SetActive(true);
        }
    }
}

