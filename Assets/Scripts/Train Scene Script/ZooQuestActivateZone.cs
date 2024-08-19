using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooQuestActivateZone : MonoBehaviour
{
    [SerializeField] private string ZooQuestActionText;

    private bool isEnter = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train" && !isEnter)
        {
            isEnter = true;
            TrainMode.GameController.Instance.ZooQuestAction(ZooQuestActionText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Train") isEnter = false;
    }
}
