using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Animator animator;

    private void Start()
    {
        slider.value = 0f;
        if (SceneManager.GetActiveScene().name == "ScoreScene")
        {
            animator.SetTrigger("GeneralScore");
            animator.SetFloat("Score", 0f);
            StartCoroutine(SetScore());
        }
        else
        {
            animator.SetTrigger("SeparateScores");
            StartCoroutine(SetScores());
        }
        // slider.value = PlayerPrefs.GetFloat("FinalScore");
        // fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private IEnumerator SetScore()
    {
        while (slider.value != PlayerPrefs.GetFloat("FinalScore"))
        {
            if (SceneManager.GetActiveScene().name == "ScoreScene")
                slider.value += 5;
            else
                slider.value++;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            animator.SetFloat("Score", slider.value);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        animator.SetTrigger("NoMoreSlide");
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("Forward");
    }

    private IEnumerator SetScores()
    {
        int bar = gameObject.tag == "bar_1" ? 1 : gameObject.tag == "bar_2" ? 2 : 3;
        while (slider.value != PlayerPrefs.GetFloat("ScoreGame" + bar))
        {
            slider.value++;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        animator.SetTrigger("NoMoreSlide");
        yield return new WaitForSeconds(7f);
        animator.SetTrigger("Forward");
    }
}
