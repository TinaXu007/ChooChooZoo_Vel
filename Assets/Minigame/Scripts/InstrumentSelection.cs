using System.Collections;
using System.Collections.Generic;
using Minigame;
using Minigmae;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigame
{
    public class InstrumentSelection : MonoBehaviour
    {
        [SerializeField] private List<GameObject> InstrumentList = new List<GameObject>();

        [SerializeField] private AnimalDance m_AnimalDance = null;

        [SerializeField] private InstrumentSequence m_sequence = null;

        private int index = 0;

        private int count = 0;

        void Start()
        {
            count = InstrumentList.Count;
            InstrumentList[index].GetComponent<CustomButton>().OnSelected(true,true);
        }

        public void NextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InstrumentList[index].GetComponent<CustomButton>().OnSelected(false,true);
                index = (index + 1) % count;
                InstrumentList[index].GetComponent<CustomButton>().OnSelected(true,true);
            }
        }
        
        public void SelectItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InstrumentList[index].GetComponent<CustomButton>().OnPressed();
                if (m_AnimalDance != null)
                {
                    int[] indices = { index * 2, index * 2 + 1 };
                    m_AnimalDance.SetAnimalDance(indices);

                    // Sequence Game
                    m_sequence.CheckSelectCorrectItem(InstrumentList[index].name);
                }
            }

        }

        public void PlayItem(int _index,float delay)
        {
            StartCoroutine(PlayItemWithDelay(_index,delay));
        }

        IEnumerator PlayItemWithDelay(int _index,float delay)
        {
            InstrumentList[_index].GetComponent<CustomButton>().OnSelected(true,false);
            yield return new WaitForSeconds(delay/3);
            InstrumentList[_index].GetComponent<CustomButton>().OnPressed();
            yield return new WaitForSeconds(delay/3);
            InstrumentList[_index].GetComponent<CustomButton>().OnSelected(false,false);
        }

        public void ShowSelectedInstrument(bool show)
        {
            InstrumentList[index].GetComponent<CustomButton>().OnSelected(show, false);
        }
    }
}

