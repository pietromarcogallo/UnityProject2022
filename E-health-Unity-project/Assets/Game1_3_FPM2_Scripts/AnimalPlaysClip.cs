using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPlaysClip : MonoBehaviour
{
    // public new AudioSource audio;
    public AudioSource[] audios;
    private int currentSound;
    private string correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        currentSound = Random.Range(0, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audios[currentSound].Play();
            // Debug.Log("Sound " + currentSound + ", correct answer: " + correctAnswer);
        }
    }

    private void Update()
    {
        switch (currentSound)
        {
            case 0:
                correctAnswer = "chicken";
                break;
            case 1:
                correctAnswer = "cow";
                break;
            case 2:
                correctAnswer = "duck";
                break;
            case 3:
                correctAnswer = "pig";
                break;
            case 4:
                correctAnswer = "sheep";
                break;
        }
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer;
    }
}
