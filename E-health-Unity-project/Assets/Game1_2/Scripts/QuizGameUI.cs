﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameUI : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private QuizManager_GE quizManager;               
    [SerializeField] private CategoryBtnScript categoryBtnPrefab;
    [SerializeField] private GameObject scrollHolder;
    [SerializeField] private Text scoreText, timerText;
    [SerializeField] private List<Image> lifeImageList;
    [SerializeField] private GameObject gameOverPanel, mainMenu, gamePanel;
    [SerializeField] private Color correctCol, wrongCol, normalCol; 
    [SerializeField] private Image questionImg;                    
    [SerializeField] private Text questionInfoText;                
    [SerializeField] private List<Button> options;                  
                                                                      
#pragma warning restore 649

    private float audioLength;         
    private Question question;          
    private bool answered = false;     

    public Text TimerText { get => timerText; }                     
    public Text ScoreText { get => scoreText; }                    
    public GameObject GameOverPanel { get => gameOverPanel; }                     

    private void Start()
    {
        
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }

        CreateCategoryButtons();

    }
    
    public void SetQuestion(Question question)
    {
        
        this.question = question;
        
        switch (question.questionType)
        {
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);  
                questionImg.transform.gameObject.SetActive(true);           
                questionImg.sprite = question.questionImage;
                break;
        }
        questionInfoText.text = question.questionInfo;                   

        
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);
        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];  
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false;                       

    }

    public void ReduceLife(int remainingLife)
    {
        lifeImageList[remainingLife].color = Color.red;
    }

    
    IEnumerator PlayIMage()
    {
                         
        if (question.questionType == QuestionType.IMAGE)
        {
            yield return new WaitForSeconds(audioLength + 0.5f);
            StartCoroutine(PlayIMage());
        }
        else 
        {
           
            StopCoroutine(PlayIMage());
           
            yield return null;
        }
    }

    /// <summary>
    /// Method assigned to the buttons
    /// </summary>
    /// <param name="btn">ref to the button object</param>
    void OnClick(Button btn)
    {
        if (quizManager.GameStatus == GameStatus.PLAYING)
        {
            //if answered is false
            if (!answered)
            {
                //set answered true
                answered = true;
                //get the bool value
                bool val = quizManager.Answer(btn.name);

                //if its true
                if (val)
                {
                    StartCoroutine(BlinkImg(btn.image));
                }
                else
                {
                                           
                    btn.image.color = wrongCol;
                }
            }
        }
    }

    
    void CreateCategoryButtons()
    {
        
        for (int i = 0; i < quizManager.QuizData.Count; i++)
        {
            
            CategoryBtnScript categoryBtn = Instantiate(categoryBtnPrefab, scrollHolder.transform);
            
            categoryBtn.SetButton(quizManager.QuizData[i].categoryName, quizManager.QuizData[i].questions.Count);
            int index = i;
            
            categoryBtn.Btn.onClick.AddListener(() => CategoryBtn(index, quizManager.QuizData[index].categoryName));
        }
    }

    
    private void CategoryBtn(int index, string category)
    {
        quizManager.StartGame(index, category); 
        mainMenu.SetActive(false);             
        gamePanel.SetActive(true);              
    }

    
    IEnumerator BlinkImg(Image img)
    {
        for (int i = 0; i < 2; i++)
        {
            img.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            img.color = correctCol;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RestryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
