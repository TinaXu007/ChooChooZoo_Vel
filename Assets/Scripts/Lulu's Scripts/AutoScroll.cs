using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Minigame;

public class AutoScroll : MonoBehaviour
{
    public ItemSelection m_ItemSelection;

    public TextMeshProUGUI delayDropdown;

    public static float delaySeconds = 3f;

    public void updateDelay(int index)
    {
        // Debug.Log(index);
        switch(index)
        {
            case 0:
                AutoScroll.delaySeconds = 3f;
                break;
            case 1:
                AutoScroll.delaySeconds = 5f;
                break;
            case 2:
                AutoScroll.delaySeconds = 10f;
                break;
            case 3:
                AutoScroll.delaySeconds = 20f;
                break;
        }
        m_ItemSelection.SetAutoScrollingDelay(delaySeconds);
    }

    public void setDelay(int seconds)
    {
        AutoScroll.delaySeconds = seconds;
    }

    public static AutoScroll Instance
    {
        get;
        private set;
    }

    

}
