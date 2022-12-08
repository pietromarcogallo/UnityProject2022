using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public AudioSource correctAnswerSound;
    public AudioSource wrongAnswerSound;
    public ScoreManager scoreManager;
    public QuizManager quizManager;

    public void Answer()
    {
        if (isCorrect)
        {
            correctAnswerSound.Play();
            scoreManager.AddScore();
        } else
        {
            wrongAnswerSound.Play();
        }
        quizManager.OntoTheNextSound();
    }
}
