using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class QuizManager_GL : MonoBehaviour
{
    public List<QuestionsAndAnswers_GL> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public List<ListOfSmall> Ls;

    public GameObject Quizpanel;
    public GameObject GOPanel;
    public TextMeshProUGUI QuestionTxt;

    public TextMeshProUGUI ScoreTxt;
    private int TotalQuestions = 0;
    public int score;

    private int coord_small = 150;
    private int coord_large = 500;
    private int vel;
    
    public GameObject settingsPopup;
    public Slider clickSlider;
    public Slider musicSlider;
    public AudioMixer audioMixer;
    public AudioSource clickSound;
    
    public float TimeLeft;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerText;

    public GameObject[] lives;
    public int life=3;
    private void Start()
    {
        TotalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        generateQuestion();
        TimerOn = true;
        
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
                GameOver();
            }
        }
    }
    
    
    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
     void GameOver()
    {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreTxt.text = "YOUR SCORE: " + score.ToString() + "/" + TotalQuestions.ToString();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void homescreen(int MainScene)
    {
        SceneManager.LoadScene(MainScene);
    }
    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
        Destroy(lives[life-1].gameObject);
        life--;
        if (life == 0)
        {
            GameOver();
        }
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }
    void SetAnswers()
    {   
        for (int i = 0; i < options.Length; i++)
        {
            if (i == 0)
            {   
                var prob = Random.Range(0, 100);
                if (prob < 50)
                {
                    vel = coord_small;
                }
                else
                {
                    vel = coord_large;
                }
            }
            else
            {
                if (vel == coord_large) vel = coord_small;
                else vel = coord_large;
            }
            
            options[i].transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = new Vector2(vel, vel);
            options[i].GetComponent<AnswerScript_GL>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];
            float b =options[i].transform.GetChild(0).GetComponent<Image>().rectTransform.rect.height;
            
            //if (QnA[currentQuestion].CorrectAnswer == i + 1)
            //{
              //  options[i].GetComponent<AnswerScript>().isCorrect = true;
            //}
            if (b == coord_large)
            {
                options[i].GetComponent<AnswerScript_GL>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
        currentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswers();
        }
        else
        {
        Debug.Log("Out of Questions");
        GameOver();
        }
    }
    
    public void toggleSettingsPopup()
    {
        settingsPopup.SetActive(!settingsPopup.activeSelf);
        TimerOn = false;
    }

    public void closeSettingsPopup()
    {
        settingsPopup.SetActive(false);
        TimerOn = true;
    }


    public void ChangeVolume()
    {
        if( musicSlider.value <= - 48)
        {
            audioMixer.SetFloat("MusicVol", -80);
        }
        else
        {
            audioMixer.SetFloat("MusicVol", musicSlider.value);
        }
    }
    

    public void clickSliderValueChange()
    {
        if (clickSlider.value <= -48)
        {
            audioMixer.SetFloat("SoundEffectsVol", -80);
        }
        else
        {
            audioMixer.SetFloat("SoundEffectsVol", clickSlider.value);
        }
    }
}

