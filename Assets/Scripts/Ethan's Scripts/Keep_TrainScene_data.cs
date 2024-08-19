using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keep_TrainScene_data : MonoBehaviour
{
    [SerializeField] private GameObject Train;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("load train position");
        if (PlayerPrefs.GetString("EnterMiniGame") == "true")
        {
            Debug.Log("load train position");
            float train_x = PlayerPrefs.GetFloat("train_x");
            float train_y = PlayerPrefs.GetFloat("train_y");
            float train_z = PlayerPrefs.GetFloat("train_z");
            Train.transform.position = new Vector3(train_x, train_y, train_z);         

            float trainYaw = PlayerPrefs.GetFloat("train_yaw");
            float trainPitch = PlayerPrefs.GetFloat("train_pitch");
            float trainRoll = PlayerPrefs.GetFloat("train_roll");
            Quaternion newRotation = Quaternion.Euler(trainPitch, trainYaw, trainRoll);

            Train.transform.rotation = newRotation;
            string EnterMiniGame = "false";
            PlayerPrefs.SetString("EnterMiniGame", EnterMiniGame);

        }
        else
        {
            Debug.Log("no train position data");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
