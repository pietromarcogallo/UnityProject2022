using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPlaysClip : MonoBehaviour
{
    // public new AudioSource audio;
    public AudioSource[] audios;
    private int currentSound;
    private string correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        currentSound = Random.Range(0, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audios[currentSound].Play();
        }
    }

    private void Update()
    {
        correctAnswer = currentSound == 0 || currentSound == 3 ? "high" : "low";
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer;
    }
}
