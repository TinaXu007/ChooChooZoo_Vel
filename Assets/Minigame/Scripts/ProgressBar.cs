using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Minigame
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image ProgressBarFill;

        private float progress_min = 0;
        private float progress_max = 100;

        private float progress = 0;

        //Yizhou add
        private int Score = 0;
        [SerializeField] private TMP_Text Star_score;
        private bool increased = false;

        void Start()
        {
            if (PlayerPrefs.HasKey("Score"))
            {
                Debug.Log("HAS score data");
                Score = PlayerPrefs.GetInt("Score");
                Star_score.SetText(Score.ToString());
            }
            else
            {
                Debug.Log("no score data");
            }
        }

        public float Progress // from 0 to 100
        {
            get { return progress; }
            set
            {
                if (value > progress_max)
                {
                    progress = progress_max;
                    IncrementScore(); // Increment score when progress bar is full
                }
                else if (value < progress_min)
                {
                    progress = progress_min;
                }
                else
                {
                    progress = value;
                }

                ProgressBarFill.fillAmount = progress / 100;
            }
        }


        public bool HasFullProgress()
        {
            return progress >= progress_max;
        }

        private void IncrementScore()
        {
            if(increased == false)
            {
                Score++;
                PlayerPrefs.SetInt("Score", Score);
                Star_score.SetText(Score.ToString());
                PlayerPrefs.Save();
                Debug.Log("Score: " + Score); // For demonstration, you can replace this with your own score handling logic
            }
            increased = true;
        }
    }

}
