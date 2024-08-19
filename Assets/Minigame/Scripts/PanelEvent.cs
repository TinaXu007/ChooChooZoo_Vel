using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame
{
    public class PanelEvent : MonoBehaviour
    {
        public bool isActive = false;

        void Start()
        {
            gameObject.SetActive(isActive);
        }

        public void OnButtonClick()
        {
            isActive = !isActive;
            gameObject.SetActive(isActive);
        }
    }

}
