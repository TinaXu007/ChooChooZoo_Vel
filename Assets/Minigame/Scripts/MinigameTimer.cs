using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Minigame
{
    [Serializable]
    public class TimerEvent : UnityEvent { }
    public class MinigameTimer : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _timerValue = 20f;
        [SerializeField] private bool _timerEnabled = true;
        [SerializeField] private GameObject _WarningImage;

        [SerializeField] private LoadingScene m_LoadingScene;
        [SerializeField] private string NextSceneToLoad;
        
        private float _currentTime;

        private void Awake()
        {
            _currentTime = _timerValue;
            _slider.maxValue = _timerValue;
            _slider.minValue = 0f;
            _WarningImage.SetActive(false);
        }

        private void Update()
        {
            if (_timerEnabled)
            {
                _currentTime -= Time.deltaTime;
                _slider.value = _currentTime;

                if (_currentTime < (_timerValue / 4))
                {
                    ShowWarning();
                }

                if (_currentTime < 0)
                { 
                    m_LoadingScene.LoadScene(NextSceneToLoad);
                    _timerEnabled = false;
                }
            }
        }

        private void ShowWarning()
        {
            _WarningImage.SetActive(true);
        }

    }

}
