using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil.Cil;
using System;
using UnityEngine.UI;

public class ZooQuest : MonoBehaviour
{
    [Serializable]
    public class Instruction
    {
        public string Task;
        public string Action;
    }

    [SerializeField] private List<Instruction> instructions = new List<Instruction>();


    [SerializeField] private TMP_Text m_InstructionText = null;

    [SerializeField] private string CompleteAllText = "";


    [SerializeField] private Animator CorrectAnimator;

    [SerializeField] private float delay = 2.0f;

    private int index = 0;

    //Yizhou add
    [SerializeField] private TMP_Text Star_score;

    private int Score = 0;
    //end


    private void Start()
    {
        //Debug.Log("load score");
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
        index = ZooQuestStatus.QuestIndex;
        Score = ZooQuestStatus.QuestScore;
        m_InstructionText.text = instructions[index].Task;
    }

    private void GetNextInstruction()
    {
        index++;
        if (index < instructions.Count)
        {
            m_InstructionText.text = instructions[index].Task;
        }
        else
        {
            m_InstructionText.text = CompleteAllText;
        }
    }

    private void OnDestroy()
    {
        // store index
        ZooQuestStatus.QuestIndex = index;
        ZooQuestStatus.QuestScore = Score;
    }

    private IEnumerator GetNextInstructionCoroutine()
    {
        CorrectAnimator.SetTrigger("Complete");
        yield return new WaitForSeconds(delay);
        GetNextInstruction();
        //Yizhou add the following code
        Debug.Log("get one star!");
        Score = PlayerPrefs.GetInt("Score");
        Score++;
        PlayerPrefs.SetInt("Score", Score);
        Star_score.SetText(Score.ToString());
        PlayerPrefs.Save();
        //end
    }

    public bool CheckAccomplishInstruction(string action)
    {
        Debug.Log(action);
        if (action != instructions[index].Action) { return false; }

        StartCoroutine(GetNextInstructionCoroutine());
        return true;
    }
}
