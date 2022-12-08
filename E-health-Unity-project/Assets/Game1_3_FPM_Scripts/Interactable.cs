using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private string[] answers = { "high", "low" };   // These are only the answers for the first part: those of the second part are preprocessed by an internal method
    private string chosenAnswer;

    public AudioSource correctAnswerSound;
    public AudioSource wrongAnswerSound;

    public void OnInteraction(bool highChoice)
    {
        chosenAnswer = highChoice ? answers[0] : answers[1];
        string correctAnswer = GetComponent<AlienPlaysClip>().GetCorrectAnswer();
        // Debug.Log("Chosen answer: " + chosenAnswer + "\nCorrect answer: " + correctAnswer);
        if (chosenAnswer == correctAnswer)
        {
            correctAnswerSound.Play();
            FindObjectOfType<SoundsGameScoreManager>().AddScore();
        }
        else
        {
            wrongAnswerSound.Play();
            FindObjectOfType<SoundsGameScoreManager>().AnsweredWrong();
        }
        Destroy(gameObject);
        FindObjectOfType<SoundsGameScoreManager>().AnswerGiven();
    }

    private string ParseAnswer(string answer)
    {
        switch (answer)
        {
            case "PigButton": return "pig";
            case "CowButton": return "cow";
            case "DuckButton": return "duck";
            case "ChickenButton": return "chicken";
            case "SheepButton": return "sheep";
            case "NullButton": return "null";
            default: return "no answer";
        }
    }

    public void OnInteraction(string choice, bool visual)
    {
        chosenAnswer = ParseAnswer(choice);
        string correctAnswer = GetCorrectAnimalAnswer(visual);
        // Debug.Log("Correct answer: " + correctAnswer);
        // You need to know the animals of all buttons appearing: if correctAnswer is none of them, then correctAnswer = "null"
        string[] buttons = FindObjectOfType<PlayerInteractionController>().GetButtons();
        if (correctAnswer != buttons[0] && correctAnswer != buttons[1] && correctAnswer != buttons[2])
            correctAnswer = "null";
        // Debug.Log("Chosen answer: " + chosenAnswer + "\nCorrect answer: " + correctAnswer);
        if (chosenAnswer == correctAnswer)
        {
            correctAnswerSound.Play();
            FindObjectOfType<SoundsGameScoreManager>().AddScore();
        }
        else
        {
            wrongAnswerSound.Play();
            FindObjectOfType<SoundsGameScoreManager>().AnsweredWrong();
        }
        Destroy(gameObject);
        FindObjectOfType<SoundsGameScoreManager>().AnswerGiven();
    }

    // The parameter "visual" indicates whether the answer to guess is visual (game 1.3.3) or not (game 1.3.2)
    public string GetCorrectAnimalAnswer(bool visual)
    {
        return visual ? gameObject.tag : GetComponent<AnimalPlaysClip>().GetCorrectAnswer();
    }
}
