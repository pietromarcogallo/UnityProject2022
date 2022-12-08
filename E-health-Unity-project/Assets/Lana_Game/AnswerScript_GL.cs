using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript_GL : MonoBehaviour
{
   public bool isCorrect = false;
   public QuizManager_GL quizManager;

   public Color startColor;

  private void Start()
   {
   startColor = GetComponent<Image>().color; 
   }
  
  public void Answer()
   {
      if (isCorrect)
      {
         //GetComponent<Image>().color = Color.green;
         Debug.Log("Correct Answer");
         quizManager.correct();
      }
      else
      {
         //GetComponent<Image>().color = Color.red;
         Debug.Log("Wrong Answer");
         quizManager.wrong();
      }

   }
}
