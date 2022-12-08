using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public QuizManager quizManager;

    public void AddScore()
    {
        if (quizManager.numSoundsHeard < 3)
            score += 5;
        else
            score += 10;
        // When numSoundsHeard == 10, switch to next sound variant game
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
