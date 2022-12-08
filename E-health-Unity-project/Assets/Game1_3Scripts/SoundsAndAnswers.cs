using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsAndAnswers : MonoBehaviour
{
    public AudioSource voice;
    public Button[] answers;
    public string[] answerTexts = { "CongruentStimulusHigh", "IncongruentStimulusHigh", "IncongruentStimulusLow", "CongruentStimulusLow" };
    public int correctAnswer;
}
