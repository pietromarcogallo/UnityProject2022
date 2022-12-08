using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    // List with audios of congruent auditory stimuli
    public List<SoundsAndAnswers> soundsAndAnswersCS;
    // List with audios of incongruent auditory stimuli
    public List<SoundsAndAnswers> soundsAndAnswersICS;
    //public GameObject[] options;
    public AnswerScript[] options;
    public int currentSound;
    public int numSoundsHeard;

    private void Start()
    {
        numSoundsHeard = 0;
        GenerateSound();
    }

    public void OntoTheNextSound()
    {
        numSoundsHeard++;
        GenerateSound();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].isCorrect = false;
            // options[i].transform.GetChild(0).GetComponent<Text>().text = soundsAndAnswers[currentSound].answerTexts[i];

            if (numSoundsHeard < 3)
                if (soundsAndAnswersCS[currentSound].correctAnswer == i+1)
                    options[i].isCorrect = true;
            else
                if (soundsAndAnswersICS[currentSound].correctAnswer == i+1)
                    options[i].isCorrect = true;
        }
    }

    void GenerateSound()
    {
        /*
         * In the first 3 trial questions, I will take the first two questions from the poll, the congruent stimuli.
         * Since the poll of questions is made of 4 possible questions (see permutations of pitch and "high"/"low" combinations),
         * when I remove the first two, the other two will switch indexes from [3,4] to [1,2], so the function Random.Range(0,1) can remain the same.
         */
        currentSound = Random.Range(0, 2);
        if (numSoundsHeard < 3)
            soundsAndAnswersCS[currentSound].voice.Play();
        else
        {
            int j = Random.Range(0, 2);
            if (j == 0)
                soundsAndAnswersICS[currentSound].voice.Play();
            else
                soundsAndAnswersCS[currentSound].voice.Play();
        }
        SetAnswers();
    }
}
