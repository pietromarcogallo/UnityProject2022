using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SoundsGameScoreManager : MonoBehaviour
{
    private static float score, firstScore, secondScore, thirdScore;

    public TextMeshProUGUI scoreText;

    public GameObject player;   // Reference necessary to update their health condition (hearts)

    private string sceneName;

    private int numInteractables = 10;

    public TransitionManager[] transitions;


    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        score = sceneName == "Game1_3_FPM" ? 0 : PlayerPrefs.GetFloat("FinalScore");
        firstScore = sceneName == "Game1_3_FPM" ? 0 : PlayerPrefs.GetFloat("ScoreGame1");
        secondScore = sceneName == "Game1_3_FPM" || sceneName == "Game1_3_FPM2" ? 0 : PlayerPrefs.GetFloat("ScoreGame2");
        thirdScore = sceneName == "Game1_3_FPM" || sceneName == "Game1_3_FPM2" || sceneName == "Game1_3_FPM3" ? 0 : PlayerPrefs.GetFloat("ScoreGame3");
        PlayerPrefs.SetFloat("FinalScore", score);
        PlayerPrefs.SetFloat("ScoreGame1", firstScore);
        PlayerPrefs.SetFloat("ScoreGame2", secondScore);
        PlayerPrefs.SetFloat("ScoreGame3", thirdScore);
        Debug.Log("Score: " + PlayerPrefs.GetFloat("FinalScore"));
        Debug.Log("Score 1: " + PlayerPrefs.GetFloat("ScoreGame1"));
        Debug.Log("Score 2: " + PlayerPrefs.GetFloat("ScoreGame2"));
        Debug.Log("Score 3: " + PlayerPrefs.GetFloat("ScoreGame3"));
    }

    public void AddScore()
    {
        if (sceneName == "Game1_3_FPM")
        {
            score += 10;
            firstScore += 10;
        }
        else
        {
            score += 20;
            if (sceneName == "Game1_3_FPM2")
                secondScore += 20;
            else
                thirdScore += 20;
        }
    }

    public void AnsweredWrong()
    {
        player.GetComponent<Health>().AffectHealth();
        // Debug.Log("Remaining lives: " + player.GetComponent<Health>().GetHealth());
        if (player.GetComponent<Health>().GetHealth() == 0)
        {
            PlayerPrefs.SetFloat("FinalScore", score);
            PlayerPrefs.SetFloat("ScoreGame1", firstScore);
            PlayerPrefs.SetFloat("ScoreGame2", secondScore);
            PlayerPrefs.SetFloat("ScoreGame3", thirdScore);
            // SoundsGameSceneManager.LoadGameScene(SceneManager.GetActiveScene().buildIndex + 1);
            // FindObjectOfType<TransitionManager>().StartTransition();
            for (int i = 0; i < transitions.Length; i++)
            {
                transitions[i].StartTransition();
            }
            return;
        }
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AnswerGiven()
    {
        numInteractables--;
        if (numInteractables == 0)
        {
            PlayerPrefs.SetFloat("FinalScore", score);
            PlayerPrefs.SetFloat("ScoreGame1", firstScore);
            PlayerPrefs.SetFloat("ScoreGame2", secondScore);
            PlayerPrefs.SetFloat("ScoreGame3", thirdScore);
            // SoundsGameSceneManager.LoadGameScene(SceneManager.GetActiveScene().buildIndex + 1);
            // FindObjectOfType<TransitionManager>().StartTransition();
            for (int i = 0; i < transitions.Length; i++)
                transitions[i].StartTransition();
        }
    }

    public static void SaveScore()
    {
        PlayerPrefs.SetFloat("FinalScore", score);
        PlayerPrefs.SetFloat("ScoreGame1", firstScore);
        PlayerPrefs.SetFloat("ScoreGame2", secondScore);
        PlayerPrefs.SetFloat("ScoreGame3", thirdScore);
    }
}
