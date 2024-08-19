using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardSystem : MonoBehaviour
{
    public static RewardSystem Instance { get; set; }

    [SerializeField] private TMP_Text rewardText;

    public float fadeTime = 10f;
    public int rewards = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rewardText.text = rewards.ToString();
    }
}
