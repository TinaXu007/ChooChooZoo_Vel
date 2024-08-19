using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTestQuestion 
{
    private string question;
    private string[] answers; // Display names of the answers (could be nothing)
    private string[] answerIDs; // What the answers actually represent (used in logging)
    private int correctAnswer; // Keep in mind that 0 is not the first index for correctAnswers. When setting the buttons, start them at 1. But the internal lists start at 0.
    private int questionFormat; // 0 = String question and string answers; 1 = Picture question and string answers; 2 = Picture question and picture answers
    private Sprite questionPic;
    private Sprite[] answerPics;
    private AudioClip audio;

    public ScreenTestQuestion(string questionText, string[] answersText, string[] actualAnswers, int correct, Sprite questionPicInput, Sprite[] answerPicsInput)
    {
        question = questionText;
        answers = answersText;
        answerIDs = actualAnswers;
        correctAnswer = correct;
        questionPic = questionPicInput;
        answerPics = answerPicsInput;
        //DetermineQuestionFormat();
    }

    /*public ScreenTestQuestion(Sprite questionPicInput, string[] answersText, int correct)
    {
        questionPic = questionPicInput;
        answers = answersText;
        correctAnswer = correct;
        //DetermineQuestionFormat();
    }

    public ScreenTestQuestion(Sprite questionPicInput, Sprite[] answerPicsInput, int correct)
    {
        questionPic = questionPicInput;
        answerPics = answerPicsInput;
        correctAnswer = correct;
        //DetermineQuestionFormat();
    }

    public ScreenTestQuestion(string questionText, Sprite[] answerPicsInput, int correct)
    {
        question = questionText;
        answerPics = answerPicsInput;
        correctAnswer = correct;
        //DetermineQuestionFormat();
    }*/


    public string GetQuestion()
    {
        return question;
    }

    public string[] GetAnswers()
    {
        return answers;
    }

    public string GetCorrectAnswer() // Returns the text
    {
        return answers[correctAnswer - 1];
    }

    public string GetCorrectAnswerValue()
    {
        return answerIDs[correctAnswer - 1];
    }

    public Sprite[] GetPics()
    {
        return answerPics;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public string GetAnswerValue(int index)
    {        
        return answerIDs[index];
    }

    public Sprite GetPic(int index)
    {
        return answerPics[index];
    }

    public bool IsPictureQuestion()
    {
        return (questionPic != null);
    }

    public Sprite GetQuestionPic()
    {
        return questionPic;
    }

    public bool IsPictureAnswers()
    {
        return (answerPics != null);
    }

    public bool IsTextQuestion()
    {
        return (question != null);
    }

    // Compare indices of the answer choices
    public bool IsChoiceCorrect(int input)
    {
        if (input == correctAnswer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetAnswer(int input)
    {
        correctAnswer = input;
    }

    public void SetAudio(AudioClip audio)
    {
        this.audio = audio;
    }

    public AudioClip GetAudio()
    {
        return audio;
    }

    /*public int GetQuestionFormat()
    {
        return questionFormat;
    }*/

    /*private void DetermineQuestionFormat()
    {
        if (answerPics != null)
        {
            questionFormat = 2;
        }
        else if (questionPic != null)
        {
            questionFormat = 1;
        }
        else
        {
            questionFormat = 0;
        }
    }*/
}
