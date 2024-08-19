using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UserInfoTest : MonoBehaviour
{
    public TMP_Text questionLabel;
    public TMP_InputField inputField;
    public Logger logger;

    private string[] questions;
    private int currQuestion = 0; // State

    // Start is called before the first frame update
    void Start()
    {
        inputField.interactable = true;
        Debug.Log("input field " + inputField.text);
        questions = new string[] { "What is your name?", "What is your birthday?"};
        questionLabel.text = questions[currQuestion];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextQuestion()
    {
        if (currQuestion == 3)
        {
            // Load next scene
        }
        else
        {
            logger.LogData(new string[] { questions[currQuestion] }, new string[] { inputField.text }); // Log user answer
            currQuestion++;
            questionLabel.text = questions[currQuestion]; // Set the label for next question
            inputField.text = "";
        }
    }
}