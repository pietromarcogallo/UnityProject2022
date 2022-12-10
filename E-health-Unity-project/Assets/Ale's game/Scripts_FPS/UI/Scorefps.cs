using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scorefps : MonoBehaviour
{
    public int scoreAmountOnKill;
    public int scorePenalty;
    public int currentScore;
    
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        currentScore = 0;
    }

    public void AddToScore()
    {
        currentScore += scoreAmountOnKill;
        scoreText.text = currentScore.ToString();
    }

    public void SubtractFromScore()
    {
        currentScore -= scorePenalty;
        scoreText.text = currentScore.ToString();
    }
}
