using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Minigame;

namespace Minigame
{
    public class ItemSelection : MonoBehaviour
    {
        public bool autoScroll = false;

        [SerializeField] private List<GameObject> ItemList = new List<GameObject>();

        [SerializeField] private float scrollDelay = 3f;

        [SerializeField] private AudioClip m_ButtonClickAudioClip;

        private int itemIndex = 0;

        private int itemCount = 0;

        private IEnumerator AutoCoroutine;

        private Outline outline = null;

        void Start()
        {
            AddOutlineToAll();
            AddOutline(itemIndex);
            OnItemSelect(itemIndex, true);
            itemCount = ItemList.Count;
            outline = GetComponent<Outline>();
            AutoCoroutine = AutoScrollCoroutine(scrollDelay);
        }

        // Add outline scipt to all objects in the list
        private void AddOutlineToAll()
        {
            foreach (GameObject item in ItemList)
            {
                var outline = item.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.white;
                outline.OutlineWidth = 0f;
            }
        }

        private void AddOutline(int index)
        {
            ItemList[index].GetComponent<Outline>().OutlineWidth = 5f;
        }

        private void RemoveOutline(int index)
        {
            ItemList[index].GetComponent<Outline>().OutlineWidth = 0f;
        }

        public void NextItem()
        {   
            float scaledVolume = AudioController.Instance.sfxVolumeSetter(0.3f);
            AudioManager.Instance.PlayAudioClip(m_ButtonClickAudioClip,scaledVolume); // Lulu added scaled volume for global volume control
            RemoveOutline(itemIndex);
            OnItemSelect(itemIndex, false);
            itemIndex = (itemIndex + 1) % itemCount;
            AddOutline(itemIndex);
            OnItemSelect(itemIndex,true);
        }

        public void PreviousItem()
        {
            RemoveOutline(itemIndex);
            OnItemSelect(itemIndex, false);
            itemIndex = ((itemIndex - 1) + itemCount) % itemCount;
            AddOutline(itemIndex);
            OnItemSelect(itemIndex,true);
        }

        public GameObject GetItem()
        {
            return ItemList[itemIndex];
        }

        private void OnItemSelect(int index, bool isSelect)
        {

            Animator animator = ItemList[index].GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsSelect", isSelect);
            }
        }

        public void StartAutoScrolling()
        {
            outline.OutlineColor = Color.red;
            autoScroll = true;
            StartCoroutine(AutoCoroutine);
        }

        private IEnumerator AutoScrollCoroutine(float delay)
        {
            while (autoScroll)
            {
                yield return new WaitForSeconds(delay);
                NextItem();
            }

        }

        public void StopAutoScrolling()
        {
            outline.OutlineColor = Color.black;
            autoScroll = false;
            StopCoroutine(AutoCoroutine);
        }

        public void SetAutoScrollingDelay(float delay)
        {
            StopAutoScrolling();
            AutoCoroutine = AutoScrollCoroutine(delay);
            StartAutoScrolling();
        }
    }

}
