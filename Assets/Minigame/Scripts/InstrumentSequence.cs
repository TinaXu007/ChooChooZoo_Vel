using System.Collections;
using System.Collections.Generic;
using Minigame;
using UnityEngine;
using UnityEngine.UI;


namespace Minigmae
{
    public class InstrumentSequence : MonoBehaviour
    {
        [SerializeField] private List<Sprite> instrumentSprites = new List<Sprite>();

        [SerializeField] private List<GameObject> levelSequence = new List<GameObject>();

        [SerializeField] private Sprite CorrectSprite = null;

        [SerializeField] private float delay = 1f;

        [SerializeField] private int threshold = 1;

        [SerializeField] private int levelCount = 4;

        private int _currentlevel = 0;

        private int spriteCnt = 0; // how many instrument sprite

        private int sequenceCnt = 0; // how many item in the sequence. level 1-> 2, level 2 -> 3, level 3-> 4

        private int[] ItemSequence; // Store sequnce of the item

        private int _currentItemIndex = 0; // current item index that hasn't diminished

        private bool hasSequence = false;

        private SimonSays _simonSays = null;

        private int score = 0;

        

        private void Awake()
        {
            // new feature : simon says
            _simonSays = GetComponent<SimonSays>();

            // set all level of sequence not active first
            foreach (GameObject sequence in levelSequence) 
            {
                sequence.SetActive(false);
            }
            // set current level active
            _currentlevel = 0;
            levelSequence[_currentlevel].SetActive(true);

            spriteCnt = instrumentSprites.Count;
            sequenceCnt = GetSequenceCount();
            ItemSequence = new int[sequenceCnt];
            
            // generate random sequence
            GetRandomSequence();

        }

        // Set current level activate if level changes
        public void SetLevel(int level,float _delay)
        {
            if (level != _currentlevel && level < levelCount )
            {
                // set current level to the level specified
                int _previousLevel = _currentlevel;
                _currentlevel = level;
                // get new sequence count
                sequenceCnt = GetSequenceCount();
                ItemSequence = new int[sequenceCnt];
                // generate random sequence
                StartCoroutine(GetRandomSequenceWithDelay(_delay));
                StartCoroutine(ChangeLevelWithDelay(_previousLevel, _currentlevel, _delay));
            }
        }

        IEnumerator ChangeLevelWithDelay(int previousLevel, int currentLevel, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            levelSequence[previousLevel].SetActive(false);
            levelSequence[currentLevel].SetActive(true);
        }
        // Generate random sequence
        public void GetRandomSequence()
        {
            for (int i = 0; i < sequenceCnt; i++)
            {
                int rand = Random.Range(0, spriteCnt ); // random instrument sprite index
                ItemSequence[i] = rand; // assign index to sequence
                SetSequenceItemSprite(i, instrumentSprites[rand]);
            }
            hasSequence = true;

            // new feature: play simon says
             _simonSays.PlaySimonSays();
        }

        // Get Next Item index that are not accomplished yet
        public int GetNextItemInSequence()
        {
            return ItemSequence[_currentItemIndex];
        }

        // Check if selecting correct item and if correct, set the sprite to correct sprite
        public void CheckSelectCorrectItem(string instrumentName)
        {
            if (hasSequence)
            {
                if (instrumentName == instrumentSprites[ItemSequence[_currentItemIndex]].name)
                {
                    SetSequenceItemSprite(_currentItemIndex, CorrectSprite);
                    _currentItemIndex++;
                    if (_currentItemIndex >= sequenceCnt)
                    {
                        _currentItemIndex = 0;
                        score++;
                        // if score is greater than the threshold score, increase Level
                        if (score > threshold)
                        {
                            score = 0;
                            if (_currentlevel + 1 >= levelCount)
                            {
                                levelSequence[_currentlevel].SetActive(false);
                                _simonSays.Finished();
                            }
                            else
                            {
                                SetLevel(_currentlevel + 1,delay);
                            }

                        }
                        else
                        {
                            StartCoroutine(GetRandomSequenceWithDelay(delay));
                        }
                    }
                }
            }
        }

        private IEnumerator GetRandomSequenceWithDelay(float _delay)
        {
            hasSequence = false;
            yield return new WaitForSeconds(_delay);
            GetRandomSequence();
        }

        private int GetSequenceCount()
        {
            int cnt = 0;
            foreach (Transform child in levelSequence[_currentlevel].transform)
            {
                cnt++;
            }
            return cnt;
        }

        private void SetSequenceItemSprite(int itemIndex, Sprite _sprite)
        {
            levelSequence[_currentlevel].transform.GetChild(itemIndex).GetComponent<Image>().sprite = _sprite;
        }

        public int[] GetSequence()
        {
            return ItemSequence;
        }
    }
}
